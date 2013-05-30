﻿using System;
using System.Collections.Generic;
using System.Net;
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

namespace ScottAlfter.CoinProfitabilityLibrary
{
    public class CoinInformation
    {
        // wrapper to get current block reward

        public decimal GetReward(string chain_type, string url_prefix, string chain_name)
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

        // get block reward from most recent block on a Tyrion blockchain explorer

        private decimal GetRewardTyrion(string url_prefix, string chain_name)
        {
            WebClient wc = new WebClient();
            string homepage = wc.DownloadString(url_prefix + "/chain/" + chain_name);
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

        // get block reward from a blockexplorer.com-compatible server

        private decimal GetRewardBlockEx(string url_prefix)
        {
            WebClient wc = new WebClient();
            return Convert.ToDecimal(wc.DownloadString(url_prefix + "/q/bcperblock"));
        }

        // wrapper to get current difficulty

        public double GetDifficulty(string chain_type, string url_prefix, string chain_name)
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

        // get difficulty (only works with newer Abe servers)

        private double GetDifficultyAbe(string url_prefix, string chain_name)
        {
            WebClient wc = new WebClient();
            return Convert.ToDouble(wc.DownloadString(url_prefix + "/chain/" + chain_name + "/q/getdifficulty"));
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

        // get difficulty from a blockexplorer.com-compatible server

        private double GetDifficultyBlockEx(string url_prefix)
        {
            WebClient wc = new WebClient();
            return Convert.ToDouble(wc.DownloadString(url_prefix + "/q/getdifficulty"));
        }
    }
}