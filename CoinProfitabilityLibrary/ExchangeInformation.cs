using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Net;

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

namespace ScottAlfter.CoinProfitabilityLibrary
{
    public class ExchangeInformation
    {
        // exchange rate wrapper

        public decimal GetExchangeRate(string exchange, string abbrev)
        {
            switch (exchange)
            {
                case "Bter":
                    return GetExchangeRateJSON("https://bter.com/api/1/ticker/" + abbrev.ToLower() + "_btc", "buy");
                case "Cryptsy":
                    return GetExchangeRateCryptsy(abbrev.ToUpper());
                default:
                    throw new ArgumentException(exchange + " exchange not supported");
            }
        }

        // scrape HTML to get exchange data from Cryptsy

        private decimal GetExchangeRateCryptsy(string abbrev)
        {
            WebClient wc = new WebClient();
            string page = wc.DownloadString("https://www.cryptsy.com/");
            foreach (string line in page.Split(new char[] { '\n' }))
                if (line.Contains(abbrev + "/BTC") && line.Contains("leftmarketinfo"))
                {
                    string link = line.Substring(line.IndexOf("href") + 6);
                    link = link.Substring(0, link.IndexOf("\""));
                    link = "https://www.cryptsy.com" + link.Replace("/markets/view", "/orders/ajaxbuyorderslist");
                    string data = wc.DownloadString(link);
                    var jss = new JavaScriptSerializer();
                    var table = jss.Deserialize<dynamic>(data);
                    try { return Convert.ToDecimal(table["aaData"][0][0]); }
                    catch
                    {
                        return Convert.ToDecimal(table["aaData"][0][0].Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                    }
                }
            return 0;
        }

        // parse JSON data returned by exchange to select desired rate

        private decimal GetExchangeRateJSON(string url, string item)
        {
            WebClient wc = new WebClient();
            string data = wc.DownloadString(url);
            var jss = new JavaScriptSerializer();
            var table = jss.Deserialize<dynamic>(data);
            return Convert.ToDecimal(table[item]);
        }
    }
}
