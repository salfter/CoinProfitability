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
        public Form1()
        {
            InitializeComponent();
        }

        private void tbInterval_TextChanged(object sender, EventArgs e)
        {
            OnChanged();
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
                BigInteger interval = new BigInteger(Convert.ToInt64(tbInterval.Text));
                BigInteger hashrate = new BigInteger(Convert.ToInt64(tbHashrate.Text));
                BigInteger reward = new BigInteger(Convert.ToInt64(tbReward.Text)) * 100000000;
                decimal difficulty = Convert.ToDecimal(tbDifficulty.Text);

                BigInteger target = ((new BigInteger(65535) << 208) * 100000000000) / new BigInteger(difficulty * 100000000000);
                BigInteger revenue = interval * target * hashrate * reward / (new BigInteger(1) << 256);

                tbIncome.Text = ((decimal)revenue / (decimal)100000000).ToString("F8");

                if (tbExchangeRate.Text != "")
                    tbIncomeBTC.Text = (Convert.ToDecimal(tbExchangeRate.Text) * (decimal)revenue / (decimal)100000000).ToString("F8");
            }
            catch 
            {
                tbIncome.Text = tbIncomeBTC.Text = "---";
            }
        }
    }
}
