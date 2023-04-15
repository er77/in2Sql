namespace SqlEngine
{
    sealed partial class Wf09CloudCloudConnectionEditor
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
            this.WF09BTTest = new System.Windows.Forms.Button();
            this.WF09BTCancel = new System.Windows.Forms.Button();
            this.WF09BTOk = new System.Windows.Forms.Button();
            this.lbPassword = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lbUrl = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.rbClickHouse = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSQL = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // WF09BTTest
            // 
            this.WF09BTTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.WF09BTTest.Location = new System.Drawing.Point(22, 248);
            this.WF09BTTest.Name = "WF09BTTest";
            this.WF09BTTest.Size = new System.Drawing.Size(75, 23);
            this.WF09BTTest.TabIndex = 23;
            this.WF09BTTest.Text = "Test";
            this.WF09BTTest.UseVisualStyleBackColor = true;
            this.WF09BTTest.Click += new System.EventHandler(this.WF09BTTest_Click);
            // 
            // WF09BTCancel
            // 
            this.WF09BTCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.WF09BTCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.WF09BTCancel.Location = new System.Drawing.Point(437, 248);
            this.WF09BTCancel.Name = "WF09BTCancel";
            this.WF09BTCancel.Size = new System.Drawing.Size(75, 23);
            this.WF09BTCancel.TabIndex = 24;
            this.WF09BTCancel.Text = "Cancel";
            this.WF09BTCancel.UseVisualStyleBackColor = true;
            this.WF09BTCancel.Click += new System.EventHandler(this.WF09BTCancel_Click);
            // 
            // WF09BTOk
            // 
            this.WF09BTOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.WF09BTOk.Location = new System.Drawing.Point(241, 248);
            this.WF09BTOk.Name = "WF09BTOk";
            this.WF09BTOk.Size = new System.Drawing.Size(75, 23);
            this.WF09BTOk.TabIndex = 25;
            this.WF09BTOk.Text = "OK";
            this.WF09BTOk.UseVisualStyleBackColor = true;
            this.WF09BTOk.Click += new System.EventHandler(this.WF09BTOk_Click);
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbPassword.Location = new System.Drawing.Point(19, 192);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(92, 17);
            this.lbPassword.TabIndex = 18;
            this.lbPassword.Text = "%password%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Connection Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(18, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "%login%";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(22, 213);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(240, 20);
            this.tbPassword.TabIndex = 14;
            this.tbPassword.Text = "clickhouse";
            // 
            // lbUrl
            // 
            this.lbUrl.AutoSize = true;
            this.lbUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbUrl.Location = new System.Drawing.Point(18, 92);
            this.lbUrl.Name = "lbUrl";
            this.lbUrl.Size = new System.Drawing.Size(24, 17);
            this.lbUrl.TabIndex = 21;
            this.lbUrl.Text = "url";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(22, 65);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(240, 20);
            this.tbName.TabIndex = 15;
            this.tbName.Text = "CHDemo";
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(22, 163);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(240, 20);
            this.tbLogin.TabIndex = 16;
            this.tbLogin.Text = "playground";
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(22, 115);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(490, 20);
            this.tbURL.TabIndex = 17;
            this.tbURL.Text = "https://play-api.clickhouse.tech:8443/?query=%SQL% ;&user=%LOGIN%&password=%PASSW" +
    "ORD%";
            // 
            // rbClickHouse
            // 
            this.rbClickHouse.AutoSize = true;
            this.rbClickHouse.Location = new System.Drawing.Point(22, 19);
            this.rbClickHouse.Name = "rbClickHouse";
            this.rbClickHouse.Size = new System.Drawing.Size(79, 17);
            this.rbClickHouse.TabIndex = 22;
            this.rbClickHouse.TabStop = true;
            this.rbClickHouse.Text = "ClickHouse";
            this.rbClickHouse.UseVisualStyleBackColor = true;
            this.rbClickHouse.CheckedChanged += new System.EventHandler(this.rbClickHouse_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(277, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "test %sql%";
            // 
            // tbSQL
            // 
            this.tbSQL.Location = new System.Drawing.Point(280, 163);
            this.tbSQL.Multiline = true;
            this.tbSQL.Name = "tbSQL";
            this.tbSQL.Size = new System.Drawing.Size(232, 70);
            this.tbSQL.TabIndex = 26;
            this.tbSQL.Text = "select * from system.databases ";
            // 
            // in2SqlWF09CloudConnectionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 293);
            this.Controls.Add(this.tbSQL);
            this.Controls.Add(this.WF09BTTest);
            this.Controls.Add(this.WF09BTCancel);
            this.Controls.Add(this.WF09BTOk);
            this.Controls.Add(this.rbClickHouse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lbUrl);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.tbURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "in2SqlWF09CloudConnectionEditor";
            this.Text = "in2SqlWF09CloudConnectionEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button WF09BTTest;
        private System.Windows.Forms.Button WF09BTCancel;
        private System.Windows.Forms.Button WF09BTOk;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lbUrl;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.RadioButton rbClickHouse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSQL;
    }
}