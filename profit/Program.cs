using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottAlfter.CoinProfitabilityLibrary;
using Gnu.Getopt;

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
            Console.WriteLine("  -b|--bitcoin   expected income in BTC");
            Console.WriteLine("  -r|--hashrate  override hashrate (provide in H/s)");
        }

        static void Main(string[] args)
        {
            RegistrySettings rs = new RegistrySettings();
            Profitability p = new Profitability();
            CoinInformation ci = new CoinInformation();
            ExchangeInformation ei = new ExchangeInformation();
            Getopt go = new Getopt("profit", args, "hlc:br:",
                new LongOpt[] 
                { 
                    new LongOpt("help", Argument.No, null, 'h'),
                    new LongOpt("list", Argument.No, null, 'l'),
                    new LongOpt("coin", Argument.Required, null, 'c'),
                    new LongOpt("bitcoin", Argument.No, null, 'b'),
                    new LongOpt("hashrate", Argument.Required, null, 'r')
                });
            int opt=-1;
            bool bIncomeInBTC = false;
            string szSelectedCoin = "Bitcoin";

            while ((opt=go.getopt())!=-1)
                switch (opt)
                {
                    case 'h':
                        Help();
                        return;
                    case 'l':
                        foreach (string name in rs.GetCoinTypes())
                            Console.WriteLine(name);
                        return;
                    case 'c':
                        szSelectedCoin = go.Optarg;
                        break;
                    case 'b':
                        bIncomeInBTC = true;
                        break;
                    default:
                        Help();
                        return;
                }
            CoinInfo inf = rs.Coins[szSelectedCoin];
            double diff = ci.GetDifficulty(inf.ExplorerType, inf.ExplorerBaseURL, inf.ExplorerChain);
            decimal reward = ci.GetReward(inf.ExplorerType, inf.ExplorerBaseURL, inf.ExplorerChain);
            decimal hashrate = 0;
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
            Console.WriteLine(p.ProfitOnInterval(86400, hashrate, reward, (decimal)diff));
        }
    }
}
