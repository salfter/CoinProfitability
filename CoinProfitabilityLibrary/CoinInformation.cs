using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

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
    public class CoinInformation
    {
        private string CoinChooseJSONData = "";

        // wrapper to get current block reward

        public decimal GetReward(string chain_type, string url_prefix, string chain_name, string chain_abbrev)
        {
            switch (chain_type)
            {
                case "CoinChoose":
                    return GetRewardCoinChoose(chain_name);
                case "Abe":
                    return GetRewardAbe(url_prefix, chain_name);
                case "BlockEx":
                    return GetRewardBlockEx(url_prefix);
                case "Coinplorer":
                    return GetRewardCoinplorer(chain_abbrev);
                default:
                    throw new ArgumentException("Block explorer type \"" + chain_type + "\" unknown");
            }
        }

        // get block reward from CoinChoose

        private decimal GetRewardCoinplorer(string chain_abbrev)
        {
            WebClient wc = new WebClient();
            string data = wc.DownloadString("https://coinplorer.com/" + chain_abbrev.ToUpper());
            bool bTable = false;
            bool bStatistics = false;
            bool bReward = false;
            decimal dReward = (decimal)0.0;
            foreach (string line in data.Split(new char[] { '\n' }))
            {
                if (bReward)
                {
                    dReward = Convert.ToDecimal(line.Split(new string[] { "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                    break;
                }
                if (line.Contains("<table"))
                    bTable = true;
                if (line.Contains("</table"))
                    bTable = bStatistics = false;
                if (line.Contains("<th") && line.Contains("Statistics") && bTable)
                    bStatistics = true;
                if (line.Contains("<td") && line.Contains("Block reward") && bStatistics)
                    bReward = true;
            }
            return dReward;
        }
        
        // get block reward from CoinChoose

        private decimal GetRewardCoinChoose(string chain_name)
        {
            if (CoinChooseJSONData == "")
            {
                WebClient wc = new WebClient();
                CoinChooseJSONData = wc.DownloadString("http://www.coinchoose.com/api.php?base=BTC");
            }
            string jsondata = CoinChooseJSONData;
            var jss = new JavaScriptSerializer();
            var table = jss.Deserialize<dynamic>(jsondata);
            foreach (var row in table)
                if (row["name"].ToLower() == chain_name.ToLower())
                    return Convert.ToDecimal(row["reward"]);
            return 0;
        }

        // get block reward from most recent block on an Abe blockchain explorer

        private decimal GetRewardAbe(string url_prefix, string chain_name)
        {
            WebClient wc = new WebClient();
            string homepage = wc.DownloadString(url_prefix + "/chain/" + chain_name+"?count=200");
            string link = "";
            double diff_pow = 0;
            double diff_pos = 0;
            double diff = 0;
            foreach (string line in homepage.Split(new char[] { '\n' }))
                if (line.Contains("<tr>") && !line.Contains("<table"))
                {
                    string[] fields = line.Split(new string[] { "<td>", "</td>", "<tr>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
                    //link = fields[0].Substring(11);
                    //link = url_prefix + link.Substring(0, link.IndexOf("\""));
                    try { diff = Convert.ToDouble(fields[4]); }
                    catch { }
                    if (diff_pow == 0) // first?
                        diff_pow = diff;
                    if (diff_pow * 100.0 < diff && diff_pos == 0) // current is at least 2 orders of magnitude greater than previous?
                    {
                        diff_pos = diff_pow;
                        diff_pow = diff;
                        break;
                    }
                    if (diff < diff_pow / 100.0 && diff_pos == 0) // current is at least 2 orders of magnitude less than previous?
                    {
                        diff_pos = diff;
                        break;
                    }
                }
            foreach (string line in homepage.Split(new char[] { '\n' })) // now find most recent proof-of-work block
                if (line.Contains("<tr>") && !line.Contains("<table"))
                {
                    string[] fields = line.Split(new string[] { "<td>", "</td>", "<tr>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        if (Convert.ToDouble(fields[4]) == diff_pow)
                        {
                            link = fields[0].Substring(11);
                            link = url_prefix + link.Substring(0, link.IndexOf("\""));
                            break;
                        }
                    }
                    catch { }
                }
            string blockinfo = wc.DownloadString(link);
            int tx_index = 0;
            decimal reward = 0;
            bool bFoundTX = false;
            foreach (string line in blockinfo.Split(new char[] { '\n' }))
            {
                if (line.Contains("Transactions") && !line.Contains("<tr>")) // ftc.cryptocoinexplorer.com puts block info into a table
                    bFoundTX = true;
                if (line.Contains("<tr>") && !line.Contains("<table") && bFoundTX)
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
            }
            return reward;
        }

        // get block reward from a blockexplorer.com-compatible server

        private decimal GetRewardBlockEx(string url_prefix)
        {
            WebClient wc = new WebClient();
            return Convert.ToDecimal(wc.DownloadString(url_prefix + "/q/bcperblock")) / 100000000;
        }

        // wrapper to get current difficulty

        public double GetDifficulty(string chain_type, string url_prefix, string chain_name, string chain_abbrev)
        {
            switch (chain_type)
            {
                case "CoinChoose":
                    return GetDifficultyCoinChoose(chain_name);
                case "Abe":
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
                case "Coinplorer":
                    return GetDifficultyCoinplorer(chain_abbrev);
                default:
                    throw new ArgumentException("Block explorer type \"" + chain_type + "\" unknown");
            }
        }

        // get difficulty from Coinplorer

        private double GetDifficultyCoinplorer(string chain_abbrev)
        {
            WebClient wc = new WebClient();
            string data = wc.DownloadString("https://coinplorer.com/" + chain_abbrev.ToUpper());
            bool bTable = false;
            bool bStatistics = false;
            bool bDifficulty = false;
            double dDifficulty = 0.0;
            foreach (string line in data.Split(new char[] { '\n' }))
            {
                if (bDifficulty)
                {
                    dDifficulty = Convert.ToDouble(line.Split(new string[] { "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                    break;
                }
                if (line.Contains("<table"))
                    bTable = true;
                if (line.Contains("</table"))
                    bTable = bStatistics= false;
                if (line.Contains("<th") && line.Contains("Statistics") && bTable)
                    bStatistics = true;
                if (line.Contains("<td") && line.Contains("Difficulty") && bStatistics)
                    bDifficulty = true;
            }
            return dDifficulty;
        }

        // get difficulty from CoinChoose

        private double GetDifficultyCoinChoose(string chain_name)
        {
            if (CoinChooseJSONData == "")
            {
                WebClient wc = new WebClient();
                CoinChooseJSONData = wc.DownloadString("http://www.coinchoose.com/api.php?base=BTC");
            }
            string jsondata = CoinChooseJSONData;
            var jss = new JavaScriptSerializer();
            var table = jss.Deserialize<dynamic>(jsondata);
            foreach (var row in table)
                if (row["name"].ToLower() == chain_name.ToLower())
                    return Convert.ToDouble(row["difficulty"]);
            return 0.0;
        }

        // get difficulty (only works with newer Abe servers)

        private double GetDifficultyAbe(string url_prefix, string chain_name)
        {
            // saa 28 Mar 14: revised to properly work with proof-of-stake coins
            WebClient wc = new WebClient();
            string homepage = wc.DownloadString(url_prefix + "/chain/" + chain_name + "?count=200");
            double diff_pow = 0;
            double diff_pos = 0;
            double diff = 0;
            foreach (string line in homepage.Split(new char[] { '\n' }))
                if (line.Contains("<tr>") && !line.Contains("<table"))
                {
                    string[] fields = line.Split(new string[] { "<td>", "</td>", "<tr>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
                    try { diff = Convert.ToDouble(fields[4]); }
                    catch { }
                    if (diff_pow == 0) // first?
                        diff_pow = diff;
                    if (diff_pow * 100.0 < diff && diff_pos == 0) // current is at least 2 orders of magnitude greater than previous?
                    {
                        diff_pos = diff_pow;
                        diff_pow = diff;
                        break;
                    }
                    if (diff < diff_pow / 100.0 && diff_pos == 0) // current is at least 2 orders of magnitude less than previous?
                    {
                        diff_pos = diff;
                        break;
                    }
                }
            return diff_pow;
        }

        // workaround for explorer.litecoin.net's outdated Abe installation:
        // get difficulty from most recent block

        private double GetDifficultyAbeAlt(string url_prefix, string chain_name)
        {
            WebClient wc = new WebClient();
            string homepage = wc.DownloadString(url_prefix + "/chain/" + chain_name);
            double difficulty = 0;
            foreach (string line in homepage.Split(new char[] { '\n' }))
                if (line.Contains("<tr>") && !line.Contains("<table"))
                {
                    string[] fields = line.Split(new string[] { "<td>", "</td>", "<tr>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
                    try { difficulty = Convert.ToDouble(fields[4]); }
                    catch { }
                    break;
                }
            return difficulty;
        }

        // get difficulty from a blockexplorer.com-compatible server

        private double GetDifficultyBlockEx(string url_prefix)
        {
            WebClient wc = new WebClient();
            return Convert.ToDouble(wc.DownloadString(url_prefix + "/q/getdifficulty"));
        }
    }
}
