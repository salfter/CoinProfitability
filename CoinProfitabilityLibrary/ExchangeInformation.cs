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
        private string CryptsyOrderData = "";

        // exchange rate wrapper

        public decimal GetExchangeRate(string exchange, string abbrev)
        {
            switch (exchange)
            {
                case "Bter":
                    return GetExchangeRateBter(abbrev.ToUpper());
                case "Cryptsy":
                    return GetExchangeRateCryptsy(abbrev.ToUpper());
                default:
                    throw new ArgumentException(exchange + " exchange not supported");
            }
        }

        // get exchange rate from Bter

        private decimal GetExchangeRateBter(string abbrev)
        {
            WebClient wc = new WebClient();
            string data = wc.DownloadString("https://bter.com/api/1/ticker/" + abbrev.ToLower() + "_btc");
            var jss = new JavaScriptSerializer();
            var table = jss.Deserialize<dynamic>(data);
            return Convert.ToDecimal(table["buy"]);
        }


        // Cryptsy has an API now...w00t!

        private decimal GetExchangeRateCryptsy(string abbrev)
        {
            RegistrySettings rs = new RegistrySettings();
            WebClient wc = new WebClient();
            //string extra = null;
            //foreach (KeyValuePair<string, CoinInfo> i in rs.Coins)
            //    if (i.Value.Abbreviation == abbrev)
            //        extra = i.Value.ExchangeExtraData;
            //if (extra == "")
            //    extra = null;
            string url="http://pubapi.cryptsy.com/api.php?method=orderdata";
            //if (extra != null)
            //    url = "http://pubapi.cryptsy.com/api.php?method=singleorderdata&marketid=" + extra;
            if (CryptsyOrderData == "")
                CryptsyOrderData = wc.DownloadString(url);
            string data = CryptsyOrderData;
            var jss = new JavaScriptSerializer();
            var table = jss.Deserialize<dynamic>(data);
            return Convert.ToDecimal(table["return"][abbrev]["buyorders"][0]["price"]);
        }
    }
}
