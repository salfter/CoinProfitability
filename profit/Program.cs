using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottAlfter.CoinProfitabilityLibrary;
using Gnu.Getopt;

// Coin Profitability Calculator
//
// Copyright © 2013 Scott Alfter
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace profit
{
    class Program
    {
        static void Help()
        {
            Console.WriteLine("Usage: profit [options]");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("  -h|--help      this message");
            Console.WriteLine("  -l|--list      list coins configured in registry");
            Console.WriteLine("  -c|--coin      select coin to look up");
            Console.WriteLine("  -b|--bitcoin   report expected income in BTC");
            Console.WriteLine("  -r|--hashrate  override hashrate (provide in H/s)");
            Console.WriteLine("  -x|--exchange  override exchange (Bter or Cryptsy)");
            Console.WriteLine("  -i|--interval  select interval (provide in seconds, default=86400)");
        }

        static int Main(string[] args)
        {
            RegistrySettings rs = new RegistrySettings();
            Profitability p = new Profitability();
            CoinInformation ci = new CoinInformation();
            ExchangeInformation ei = new ExchangeInformation();
            Getopt go = new Getopt("profit", args, "hlc:br:x:i:",
                new LongOpt[] 
                { 
                    new LongOpt("help", Argument.No, null, 'h'),
                    new LongOpt("list", Argument.No, null, 'l'),
                    new LongOpt("coin", Argument.Required, null, 'c'),
                    new LongOpt("bitcoin", Argument.No, null, 'b'),
                    new LongOpt("hashrate", Argument.Required, null, 'r'),
                    new LongOpt("exchange", Argument.Required, null, 'x'),
                    new LongOpt("interval", Argument.Required, null, 'i')
                });
            int opt=-1;
            bool bIncomeInBTC = false;
            string szSelectedCoin = "";
            foreach (string name in rs.GetCoinTypes())
                if (name.ToLower()=="bitcoin")
                    szSelectedCoin = name;
            decimal hashrate = 0;
            string exchange = "";
            long interval = 86400;

            while ((opt=go.getopt())!=-1)
                switch (opt)
                {
                    case 'h':
                        Help();
                        return 0;
                    case 'l':
                        foreach (string name in rs.GetCoinTypes())
                            Console.WriteLine(name);
                        return 0;
                    case 'c':
                        szSelectedCoin = go.Optarg;
                        break;
                    case 'b':
                        bIncomeInBTC = true;
                        break;
                    case 'r':
                        hashrate = Convert.ToDecimal(go.Optarg);
                        break;
                    case 'x':
                        exchange = go.Optarg;
                        break;
                    case 'i':
                        interval = Convert.ToInt64(go.Optarg);
                        break;
                    default:
                        Help();
                        return -1;
                }
            if (szSelectedCoin == "")
            {
                Console.WriteLine("No coin selected");
                return -1;
            }
            CoinInfo inf = rs.Coins[szSelectedCoin];
            double diff = 0;
            try { diff = ci.GetDifficulty(inf.ExplorerType, inf.ExplorerBaseURL, inf.ExplorerChain); }
            catch { Console.WriteLine("Unable to fetch difficulty"); return -1; }
            decimal reward = 0;
            try { reward = ci.GetReward(inf.ExplorerType, inf.ExplorerBaseURL, inf.ExplorerChain); }
            catch { Console.WriteLine("Unable to fetch reward"); return -1; }
            if (hashrate == 0)
                switch (inf.DefaultHashRateUnit)
                {
                    case "H/s":
                        hashrate = Convert.ToDecimal(inf.DefaultHashRate);
                        break;
                    case "kH/s":
                        hashrate = Convert.ToDecimal(inf.DefaultHashRate) * 1000;
                        break;
                    case "MH/s":
                        hashrate = Convert.ToDecimal(inf.DefaultHashRate) * 1000000;
                        break;
                    case "GH/s":
                        hashrate = Convert.ToDecimal(inf.DefaultHashRate) * 100000000;
                        break;
                    default:
                        throw new ArgumentException("invalid hashrate unit");
                }
            if (!bIncomeInBTC)
                Console.WriteLine(p.ProfitOnInterval(interval, hashrate, reward, (decimal)diff).ToString("F8"));
            else
            {
                decimal exchrate = 0;
                try { exchrate = (szSelectedCoin.ToLower() != "bitcoin") ? ei.GetExchangeRate((exchange != "") ? exchange : inf.Exchange, inf.Abbreviation) : 1; }
                catch { Console.WriteLine("Unable to fetch exchange rate"); return -1; }
                Console.WriteLine(p.ProfitOnIntervalBTC(interval, hashrate, reward, (decimal)diff, exchrate).ToString("F8"));
            }
            return 0;
        }
    }
}
