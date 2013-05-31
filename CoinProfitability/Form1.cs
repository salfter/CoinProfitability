using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Numerics;
using ScottAlfter.CoinProfitabilityLibrary;

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
        // this reads in registry settings

        RegistrySettings rs = new RegistrySettings();

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
            Profitability p = new Profitability();
            try
            {
                tbIncome.Text = p.ProfitOnInterval(Convert.ToInt64(((Item)(cbInterval.SelectedItem)).Value),
                    Convert.ToDecimal(tbHashrate.Text) * Convert.ToInt64(((Item)(cbHashrateUnit.SelectedItem)).Value),
                    Convert.ToDecimal(tbReward.Text),
                    Convert.ToDecimal(tbDifficulty.Text)).ToString("F8");

                if (tbExchangeRate.Text != "")
                    tbIncomeBTC.Text = p.ProfitOnIntervalBTC(Convert.ToInt64(((Item)(cbInterval.SelectedItem)).Value),
                    Convert.ToDecimal(tbHashrate.Text) * Convert.ToInt64(((Item)(cbHashrateUnit.SelectedItem)).Value),
                    Convert.ToDecimal(tbReward.Text),
                    Convert.ToDecimal(tbDifficulty.Text),
                    Convert.ToDecimal(tbExchangeRate.Text)).ToString("F8");
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

            foreach (string i in rs.GetCoinTypes())
                cbCoinType.Items.Add(i);
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
            ExchangeInformation ei = new ExchangeInformation();
            CoinInformation ci = new CoinInformation();
            lblUpdating.Visible = true;
            this.Refresh();
            string szCoinType=cbCoinType.Items[cbCoinType.SelectedIndex].ToString();
            CoinInfo i = rs.Coins[szCoinType];
            lblAbbrev.Text = lblRewardCurrency.Text = i.Abbreviation;
            lblExchangeRateCurrency.Text = "BTC/" + i.Abbreviation;
            try { tbDifficulty.Text = ci.GetDifficulty(i.ExplorerType, i.ExplorerBaseURL, i.ExplorerChain).ToString(); }
            catch { tbDifficulty.Text = "Unavailable"; }
            try { tbReward.Text = ci.GetReward(i.ExplorerType, i.ExplorerBaseURL, i.ExplorerChain).ToString(); }
            catch { tbReward.Text = "Unavailable"; }
            for (int c = 0; c < cbHashrateUnit.Items.Count; c++)
                if (((Item)cbHashrateUnit.Items[c]).Name == i.DefaultHashRateUnit)
                    cbHashrateUnit.SelectedIndex = c;
            try
            {
                if (i.Exchange != null)
                    tbExchangeRate.Text = ei.GetExchangeRate(i.Exchange, i.Abbreviation).ToString();
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
