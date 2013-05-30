using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Numerics;
using System.Net;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;

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

namespace CoinProfitability
{
    public partial class Form1 : Form
    {
        // exchange rate wrapper

        private decimal GetExchangeRate(string exchange, string abbrev)
        {
            switch (exchange)
            {
                case "Bter":
                    return GetExchangeRateJSON("https://bter.com/api/1/ticker/"+abbrev.ToLower()+"_btc", "buy");
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
                    link = "https://www.cryptsy.com"+link.Replace("/markets/view", "/orders/ajaxbuyorderslist");
                    using (StringReader sr=new StringReader(wc.DownloadString(link)))
                    using (JsonTextReader r = new JsonTextReader(sr))
                    {
                        while (r.Read())
                        {
                            if (r.TokenType == JsonToken.String) // first string returned is highest buy price
                                return Convert.ToDecimal(r.Value);
                        }
                    }
                }
            return 0;
        }

        // parse JSON data returned by exchange to select desired rate

        private decimal GetExchangeRateJSON(string url, string var)
        {
            WebClient wc = new WebClient();
            using (StringReader sr=new StringReader(wc.DownloadString(url)))
            using (JsonTextReader r = new JsonTextReader(sr))
            {
                bool bReadNext = false;
                while (r.Read())
                {
                    if (bReadNext == true)
                        return Convert.ToDecimal(r.Value);
                    if (r.Value != null)
                        if (r.Value.ToString() == var)
                            bReadNext = true;
                }
            }
            return 0;
        }

        // wrapper to get current block reward

        private decimal GetReward(string chain_type, string url_prefix, string chain_name)
        {
            switch (chain_type)
            {
                case "Abe":
                    return GetRewardAbe(url_prefix, chain_name);
                case "BlockEx":
                    return GetRewardBlockEx(url_prefix);
                case "Tyrion":
                    return GetRewardTyrion(url_prefix, chain_name);
                default:
                    throw new ArgumentException("Block explorer type \"" + chain_type + "\" unknown");
            }
        }

        // wrapper to get current difficulty

        private double GetDifficulty(string chain_type, string url_prefix, string chain_name)
        {
            switch (chain_type)
            {
                case "Abe":
                case "Tyrion": // similar enough to Abe
                    double difficulty = -1;
                    try { difficulty = GetDifficultyAbe(url_prefix, chain_name); }
                    catch { }
                    if (difficulty == -1)
                    {
                        try { difficulty = GetDifficultyAbeAlt(url_prefix, chain_name); }
                        catch { }
                    }
                    if (difficulty != -1)
                        return difficulty;
                    else
                        throw new InvalidDataException("Can't get difficulty from " + url_prefix);
                case "BlockEx":
                    return GetDifficultyBlockEx(url_prefix);
                default:
                    throw new ArgumentException("Block explorer type \"" + chain_type + "\" unknown");
            }
        }

        // workaround for explorer.litecoin.net's outdated Abe installation:
        // get difficulty from most recent block

        private double GetDifficultyAbeAlt(string url_prefix, string chain_name)
        {
            WebClient wc = new WebClient();
            int blockcount = Convert.ToInt32(wc.DownloadString(url_prefix + "/chain/" + chain_name + "/q/getblockcount"));
            string blockinfo = wc.DownloadString(url_prefix + "/search?q=" + blockcount.ToString());
            double difficulty = 0;
            foreach (string line in blockinfo.Split(new char[] { '\n' }))
                if (line.Contains("Difficulty") && !line.Contains("Cumulative"))
                    difficulty = Convert.ToDouble(line.Split(new char[] { ' ' })[1]);
            return difficulty;
        }

        // get block reward from most recent block on a Tyrion blockchain explorer

        private decimal GetRewardTyrion(string url_prefix, string chain_name)
        {
            WebClient wc = new WebClient();
            string homepage=wc.DownloadString(url_prefix + "/chain/" + chain_name);
            string link = "";
            foreach (string line in homepage.Split(new char[] { '\n' }))
                if (line.Contains("<tr>") && !line.Contains("<table"))
                {
                    string[] fields = line.Split(new string[] { "<td>", "</td>", "<tr>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
                    link = fields[0].Substring(11);
                    link = url_prefix + link.Substring(0, link.IndexOf("\""));
                    break;
                }
            string blockinfo = wc.DownloadString(link);
            int tx_index = 0;
            decimal reward = 0;
            foreach (string line in blockinfo.Split(new char[] { '\n' }))
                if (line.Contains("<tr>") && !line.Contains("<table"))
                {
                    string[] fields = line.Split(new string[] { "<td>", "</td>", "<tr>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
                    if (tx_index == 0)
                    {
                        reward = Convert.ToDecimal(fields[3].Split(new char[] { ' ' })[1]);
                        if (fields[3].Contains("+"))
                            break;
                    }
                    else
                        reward -= Convert.ToDecimal(fields[1]);
                    tx_index++;
                }
            return reward * (decimal)100000000;
        }
         
        // get block reward from most recent block on an Abe blockchain explorer

        private decimal GetRewardAbe(string url_prefix, string chain_name)
        {
            WebClient wc = new WebClient();
            int blockcount = Convert.ToInt32(wc.DownloadString(url_prefix + "/chain/" + chain_name + "/q/getblockcount"));
            string blockinfo = wc.DownloadString(url_prefix + "/search?q=" + blockcount.ToString());
            int tx_index = 0;
            decimal reward = 0;
            foreach (string line in blockinfo.Split(new char[] { '\n' }))
                if (line.Contains("<tr>") && !line.Contains("<table>"))
                {
                    string[] fields = line.Split(new string[] { "<td>", "</td>", "<tr>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
                    if (tx_index == 0)
                    {
                        reward = Convert.ToDecimal(fields[3].Split(new char[] { ' ' })[1]);
                        if (fields[3].Contains("+"))
                            break;
                    }
                    else
                        reward -= Convert.ToDecimal(fields[1]);
                    tx_index++;
                }
            return reward * (decimal)100000000;
        }

        // get difficulty (only works with newer Abe servers)

        private double GetDifficultyAbe(string url_prefix, string chain_name)
        {
            WebClient wc = new WebClient();
            return Convert.ToDouble(wc.DownloadString(url_prefix + "/chain/" + chain_name + "/q/getdifficulty"));
        }

        // get block reward from a blockexplorer.com-compatible server

        private decimal GetRewardBlockEx(string url_prefix)
        {
            WebClient wc = new WebClient();
            return Convert.ToDecimal(wc.DownloadString(url_prefix+"/q/bcperblock"));
        }

        // get difficulty from a blockexplorer.com-compatible server

        private double GetDifficultyBlockEx(string url_prefix)
        {
            WebClient wc = new WebClient();
            return Convert.ToDouble(wc.DownloadString(url_prefix+"/q/getdifficulty"));
        }

        // registry settings are read into these

        private struct CoinInfo
        {
            public string ExplorerBaseURL;
            public string ExplorerChain;
            public string ExplorerType;
            public string DefaultHashRateUnit;
            //public string ExchangeURL;
            //public string ExchangeJSONKey;
            public string Exchange;
            public string Abbreviation;
            public string DefaultHashRate;
        }

        SortedDictionary<string, CoinInfo> coins = new SortedDictionary<string, CoinInfo>();

        // drop-down lists are populated with these

        private class Item
        {
            public string Name;
            public string Value;
            public Item(string szName, string szValue)
            {
                Name=szName;
                Value = szValue;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void tbHashrate_TextChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void tbReward_TextChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void tbDifficulty_TextChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void tbExchangeRate_TextChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        // estimate revenue over an interval

        private void OnChanged()
        {
            try
            {
                BigInteger interval = new BigInteger(Convert.ToInt64(((Item)(cbInterval.SelectedItem)).Value));
                BigInteger hashrate = new BigInteger(Convert.ToDecimal(tbHashrate.Text)) * Convert.ToInt64(((Item)(cbHashrateUnit.SelectedItem)).Value);
                BigInteger reward = new BigInteger(Convert.ToDecimal(tbReward.Text)) * 100000000;
                decimal difficulty = Convert.ToDecimal(tbDifficulty.Text);

                BigInteger target = ((new BigInteger(65535) << 208) * 100000000000) / new BigInteger(difficulty * 100000000000);
                BigInteger revenue = interval * target * hashrate * reward / (new BigInteger(1) << 256);

                tbIncome.Text = ((decimal)revenue / (decimal)100000000).ToString("F8");

                if (tbExchangeRate.Text != "")
                    tbIncomeBTC.Text = (Convert.ToDecimal(tbExchangeRate.Text) * (decimal)revenue / (decimal)100000000).ToString("F8");
                else
                    tbIncomeBTC.Text = "---";
            }
            catch 
            {
                tbIncome.Text = tbIncomeBTC.Text = "---";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // populate drop-down lists

            cbInterval.Items.Add(new Item("Week", "604800"));
            cbInterval.Items.Add(new Item("Day", "86400"));
            cbInterval.Items.Add(new Item("Hour", "3600"));
            cbInterval.SelectedIndex = 1;

            cbHashrateUnit.Items.Add(new Item("GH/s", "1000000000"));
            cbHashrateUnit.Items.Add(new Item("MH/s", "1000000"));
            cbHashrateUnit.Items.Add(new Item("kH/s", "1000"));
            cbHashrateUnit.Items.Add(new Item("H/s", "1"));
            cbHashrateUnit.SelectedIndex = 1;

            // read registry settings

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
                        t.Abbreviation = (string)k.GetValue("Abbreviation");
                        t.DefaultHashRate = (string)k.GetValue("DefaultHashRate");
                        coins.Add(i, t);
                        cbCoinType.Items.Add(i);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void cbHashrateUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        // someone wants to donate...w00t!

        private void btnTips_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://alfter.us/donate.php");
        }

        // load data into form

        private void cbCoinType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblUpdating.Visible = true;
            this.Refresh();
            string szCoinType=cbCoinType.Items[cbCoinType.SelectedIndex].ToString();
            CoinInfo i = coins[szCoinType];
            try { tbDifficulty.Text = GetDifficulty(i.ExplorerType, i.ExplorerBaseURL, i.ExplorerChain).ToString(); }
            catch { tbDifficulty.Text = "Unavailable"; }
            try { tbReward.Text = (GetReward(i.ExplorerType, i.ExplorerBaseURL, i.ExplorerChain) / (decimal)100000000).ToString(); }
            catch { tbReward.Text = "Unavailable"; }
            for (int c = 0; c < cbHashrateUnit.Items.Count; c++)
                if (((Item)cbHashrateUnit.Items[c]).Name == i.DefaultHashRateUnit)
                    cbHashrateUnit.SelectedIndex = c;
            try
            {
                if (i.Exchange != null)
                    tbExchangeRate.Text = GetExchangeRate(i.Exchange, i.Abbreviation).ToString();
                else
                    tbExchangeRate.Text = "";
            }
            catch { tbExchangeRate.Text = "Unavailable"; }
            if (cbCoinType.Items[cbCoinType.SelectedIndex].ToString().ToLower() == "bitcoin") // case-insensitive for Mono compatibility
                tbExchangeRate.Text = "1";
            tbHashrate.Text = i.DefaultHashRate;
            lblUpdating.Visible = false;
            this.Refresh();
            OnChanged();
        }
    }
}
