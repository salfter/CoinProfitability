namespace CoinProfitability
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHashrate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbReward = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDifficulty = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbExchangeRate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbIncome = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbIncomeBTC = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbInterval
            // 
            this.tbInterval.Location = new System.Drawing.Point(100, 13);
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(100, 20);
            this.tbInterval.TabIndex = 0;
            this.tbInterval.TextChanged += new System.EventHandler(this.tbInterval_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Interval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hashrate";
            // 
            // tbHashrate
            // 
            this.tbHashrate.Location = new System.Drawing.Point(100, 39);
            this.tbHashrate.Name = "tbHashrate";
            this.tbHashrate.Size = new System.Drawing.Size(100, 20);
            this.tbHashrate.TabIndex = 2;
            this.tbHashrate.TextChanged += new System.EventHandler(this.tbHashrate_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "s";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "H/s";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "C";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Reward";
            // 
            // tbReward
            // 
            this.tbReward.Location = new System.Drawing.Point(100, 65);
            this.tbReward.Name = "tbReward";
            this.tbReward.Size = new System.Drawing.Size(100, 20);
            this.tbReward.TabIndex = 6;
            this.tbReward.TextChanged += new System.EventHandler(this.tbReward_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Difficulty";
            // 
            // tbDifficulty
            // 
            this.tbDifficulty.Location = new System.Drawing.Point(100, 91);
            this.tbDifficulty.Name = "tbDifficulty";
            this.tbDifficulty.Size = new System.Drawing.Size(100, 20);
            this.tbDifficulty.TabIndex = 9;
            this.tbDifficulty.TextChanged += new System.EventHandler(this.tbDifficulty_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(206, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "BTC/C";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Exchange Rate";
            // 
            // tbExchangeRate
            // 
            this.tbExchangeRate.Location = new System.Drawing.Point(100, 117);
            this.tbExchangeRate.Name = "tbExchangeRate";
            this.tbExchangeRate.Size = new System.Drawing.Size(100, 20);
            this.tbExchangeRate.TabIndex = 11;
            this.tbExchangeRate.TextChanged += new System.EventHandler(this.tbExchangeRate_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Expected Income Per Interval:";
            // 
            // tbIncome
            // 
            this.tbIncome.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbIncome.Location = new System.Drawing.Point(15, 177);
            this.tbIncome.Name = "tbIncome";
            this.tbIncome.ReadOnly = true;
            this.tbIncome.Size = new System.Drawing.Size(100, 13);
            this.tbIncome.TabIndex = 15;
            this.tbIncome.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(121, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "C";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(121, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "BTC";
            // 
            // tbIncomeBTC
            // 
            this.tbIncomeBTC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbIncomeBTC.Location = new System.Drawing.Point(15, 190);
            this.tbIncomeBTC.Name = "tbIncomeBTC";
            this.tbIncomeBTC.ReadOnly = true;
            this.tbIncomeBTC.Size = new System.Drawing.Size(100, 13);
            this.tbIncomeBTC.TabIndex = 17;
            this.tbIncomeBTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 213);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbIncomeBTC);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbIncome);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbExchangeRate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDifficulty);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbReward);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbHashrate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbInterval);
            this.Name = "Form1";
            this.Text = "Coin Profitability";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHashrate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbReward;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDifficulty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbExchangeRate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbIncome;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbIncomeBTC;
    }
}

