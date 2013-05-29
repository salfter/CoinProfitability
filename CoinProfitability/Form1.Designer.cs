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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHashrate = new System.Windows.Forms.TextBox();
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
            this.cbInterval = new System.Windows.Forms.ComboBox();
            this.cbHashrateUnit = new System.Windows.Forms.ComboBox();
            this.btnTips = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCoinType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUpdating = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Interval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hashrate";
            // 
            // tbHashrate
            // 
            this.tbHashrate.Location = new System.Drawing.Point(100, 78);
            this.tbHashrate.Name = "tbHashrate";
            this.tbHashrate.Size = new System.Drawing.Size(100, 20);
            this.tbHashrate.TabIndex = 1;
            this.tbHashrate.TextChanged += new System.EventHandler(this.tbHashrate_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "C";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Reward";
            // 
            // tbReward
            // 
            this.tbReward.Location = new System.Drawing.Point(100, 104);
            this.tbReward.Name = "tbReward";
            this.tbReward.Size = new System.Drawing.Size(100, 20);
            this.tbReward.TabIndex = 3;
            this.tbReward.TextChanged += new System.EventHandler(this.tbReward_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Difficulty";
            // 
            // tbDifficulty
            // 
            this.tbDifficulty.Location = new System.Drawing.Point(100, 130);
            this.tbDifficulty.Name = "tbDifficulty";
            this.tbDifficulty.Size = new System.Drawing.Size(100, 20);
            this.tbDifficulty.TabIndex = 4;
            this.tbDifficulty.TextChanged += new System.EventHandler(this.tbDifficulty_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(206, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "BTC/C";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Exchange Rate";
            // 
            // tbExchangeRate
            // 
            this.tbExchangeRate.Location = new System.Drawing.Point(100, 156);
            this.tbExchangeRate.Name = "tbExchangeRate";
            this.tbExchangeRate.Size = new System.Drawing.Size(100, 20);
            this.tbExchangeRate.TabIndex = 5;
            this.tbExchangeRate.TextChanged += new System.EventHandler(this.tbExchangeRate_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 200);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Expected Income Per Interval:";
            // 
            // tbIncome
            // 
            this.tbIncome.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbIncome.Location = new System.Drawing.Point(15, 216);
            this.tbIncome.Name = "tbIncome";
            this.tbIncome.ReadOnly = true;
            this.tbIncome.Size = new System.Drawing.Size(100, 13);
            this.tbIncome.TabIndex = 15;
            this.tbIncome.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(121, 216);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "C";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(121, 229);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "BTC";
            // 
            // tbIncomeBTC
            // 
            this.tbIncomeBTC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbIncomeBTC.Location = new System.Drawing.Point(15, 229);
            this.tbIncomeBTC.Name = "tbIncomeBTC";
            this.tbIncomeBTC.ReadOnly = true;
            this.tbIncomeBTC.Size = new System.Drawing.Size(100, 13);
            this.tbIncomeBTC.TabIndex = 17;
            this.tbIncomeBTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbInterval
            // 
            this.cbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterval.FormattingEnabled = true;
            this.cbInterval.Location = new System.Drawing.Point(100, 51);
            this.cbInterval.Name = "cbInterval";
            this.cbInterval.Size = new System.Drawing.Size(100, 21);
            this.cbInterval.TabIndex = 0;
            this.cbInterval.SelectedIndexChanged += new System.EventHandler(this.cbInterval_SelectedIndexChanged);
            // 
            // cbHashrateUnit
            // 
            this.cbHashrateUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHashrateUnit.FormattingEnabled = true;
            this.cbHashrateUnit.Location = new System.Drawing.Point(206, 78);
            this.cbHashrateUnit.Name = "cbHashrateUnit";
            this.cbHashrateUnit.Size = new System.Drawing.Size(53, 21);
            this.cbHashrateUnit.TabIndex = 2;
            this.cbHashrateUnit.SelectedIndexChanged += new System.EventHandler(this.cbHashrateUnit_SelectedIndexChanged);
            // 
            // btnTips
            // 
            this.btnTips.Image = global::CoinProfitability.Properties.Resources.tipjar;
            this.btnTips.Location = new System.Drawing.Point(71, 19);
            this.btnTips.Name = "btnTips";
            this.btnTips.Size = new System.Drawing.Size(117, 114);
            this.btnTips.TabIndex = 19;
            this.btnTips.UseVisualStyleBackColor = true;
            this.btnTips.Click += new System.EventHandler(this.btnTips_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnTips);
            this.groupBox1.Location = new System.Drawing.Point(12, 245);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 155);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hit the Tipjar!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Click QR code to launch wallet";
            // 
            // cbCoinType
            // 
            this.cbCoinType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCoinType.FormattingEnabled = true;
            this.cbCoinType.Location = new System.Drawing.Point(100, 12);
            this.cbCoinType.Name = "cbCoinType";
            this.cbCoinType.Size = new System.Drawing.Size(100, 21);
            this.cbCoinType.TabIndex = 21;
            this.cbCoinType.SelectedIndexChanged += new System.EventHandler(this.cbCoinType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Coin Type";
            // 
            // lblUpdating
            // 
            this.lblUpdating.AutoSize = true;
            this.lblUpdating.Location = new System.Drawing.Point(206, 15);
            this.lblUpdating.Name = "lblUpdating";
            this.lblUpdating.Size = new System.Drawing.Size(59, 13);
            this.lblUpdating.TabIndex = 23;
            this.lblUpdating.Text = "Updating...";
            this.lblUpdating.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 415);
            this.Controls.Add(this.lblUpdating);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbCoinType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbHashrateUnit);
            this.Controls.Add(this.cbInterval);
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
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbHashrate);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Coin Profitability";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHashrate;
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
        private System.Windows.Forms.ComboBox cbInterval;
        private System.Windows.Forms.ComboBox cbHashrateUnit;
        private System.Windows.Forms.Button btnTips;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCoinType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblUpdating;
    }
}

