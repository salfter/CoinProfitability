using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace CoinProfitability
{
    public partial class Form1 : Form
    {
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

        private void OnChanged()
        {
            try
            {
                BigInteger interval = new BigInteger(Convert.ToInt64(((Item)(cbInterval.SelectedItem)).Value));
                BigInteger hashrate = new BigInteger(Convert.ToInt64(tbHashrate.Text)) * Convert.ToInt64(((Item)(cbHashrateUnit.SelectedItem)).Value);
                BigInteger reward = new BigInteger(Convert.ToInt64(tbReward.Text)) * 100000000;
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
            cbInterval.Items.Add(new Item("Week", "604800"));
            cbInterval.Items.Add(new Item("Day", "86400"));
            cbInterval.Items.Add(new Item("Hour", "3600"));
            cbInterval.SelectedIndex = 1;

            cbHashrateUnit.Items.Add(new Item("GH/s", "1000000000"));
            cbHashrateUnit.Items.Add(new Item("MH/s", "1000000"));
            cbHashrateUnit.Items.Add(new Item("kH/s", "1000"));
            cbHashrateUnit.Items.Add(new Item("H/s", "1"));
            cbHashrateUnit.SelectedIndex = 1;
        }

        private void cbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void cbHashrateUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://alfter.us/donate.php");
        }
    }
}
