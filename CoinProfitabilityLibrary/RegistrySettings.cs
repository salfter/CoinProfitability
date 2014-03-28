using System;
using System.Collections.Generic;
using Microsoft.Win32;

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
    public struct CoinInfo
    {
        public string ExplorerBaseURL;
        public string ExplorerChain;
        public string ExplorerType;
        public string DefaultHashRateUnit;
        public string Exchange;
        public string ExchangeExtraData;
        public string Abbreviation;
        public string DefaultHashRate;
    }

    public class RegistrySettings
    {
        // registry settings are read into these

        public SortedDictionary<string, CoinInfo> Coins=new SortedDictionary<string,CoinInfo>();

        public RegistrySettings()
        {
            try
            {
                using (RegistryKey rkcu = Registry.CurrentUser)
                using (RegistryKey rkSettings = rkcu.OpenSubKey("Software\\Scott Alfter\\CoinProfitability"))
                {
                    foreach (string i in rkSettings.GetSubKeyNames())
                    {
                        RegistryKey k = rkSettings.OpenSubKey(i);
                        CoinInfo t = new CoinInfo();
                        t.ExplorerBaseURL = (string)k.GetValue("ExplorerBaseURL");
                        t.ExplorerType = (string)k.GetValue("ExplorerType");
                        t.ExplorerChain = (string)k.GetValue("ExplorerChain");
                        t.DefaultHashRateUnit = (string)k.GetValue("DefaultHashRateUnit");
                        t.Exchange = (string)k.GetValue("Exchange");
                        t.ExchangeExtraData = (string)k.GetValue("ExchangeExtraData");
                        t.Abbreviation = (string)k.GetValue("Abbreviation");
                        t.DefaultHashRate = (string)k.GetValue("DefaultHashRate");
                        Coins.Add(i, t);
                    }
                }
            }
            catch { }
        }

        public List<string> GetCoinTypes()
        {
            List<string> rtnval=new List<string>();
            foreach (string i in Coins.Keys)
                rtnval.Add(i);
            return rtnval;
        }
    }
}
