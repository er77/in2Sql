namespace SqlEngine
{
    sealed partial class Wf01Login
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
            this.WF01TBLogin = new System.Windows.Forms.TextBox();
            this.WF01BTTest = new System.Windows.Forms.Button();
            this.WF01BTCancel = new System.Windows.Forms.Button();
            this.WF01BTOk = new System.Windows.Forms.Button();
            this.WF01TBPassword = new System.Windows.Forms.TextBox();
            this.WF01LblPassword = new System.Windows.Forms.Label();
            this.WF01LblODBC = new System.Windows.Forms.Label();
            this.WF01LblLogin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WF01TBLogin
            // 
            this.WF01TBLogin.Location = new System.Drawing.Point(94, 36);
            this.WF01TBLogin.Name = "WF01TBLogin";
            this.WF01TBLogin.Size = new System.Drawing.Size(156, 20);
            this.WF01TBLogin.TabIndex = 3;
            this.WF01TBLogin.Click += new System.EventHandler(this.WF01LblLogin_Click);
            // 
            // WF01BTTest
            // 
            this.WF01BTTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.WF01BTTest.Location = new System.Drawing.Point(12, 112);
            this.WF01BTTest.Name = "WF01BTTest";
            this.WF01BTTest.Size = new System.Drawing.Size(75, 23);
            this.WF01BTTest.TabIndex = 8;
            this.WF01BTTest.Text = "Test";
            this.WF01BTTest.UseVisualStyleBackColor = true;
            this.WF01BTTest.Click += new System.EventHandler(this.WF01BTTest_Click);
            // 
            // WF01BTCancel
            // 
            this.WF01BTCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.WF01BTCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.WF01BTCancel.Location = new System.Drawing.Point(175, 112);
            this.WF01BTCancel.Name = "WF01BTCancel";
            this.WF01BTCancel.Size = new System.Drawing.Size(75, 23);
            this.WF01BTCancel.TabIndex = 9;
            this.WF01BTCancel.Text = "Cancel";
            this.WF01BTCancel.UseVisualStyleBackColor = true;
            this.WF01BTCancel.Click += new System.EventHandler(this.WF01BTCancel_Click);
            // 
            // WF01BTOk
            // 
            this.WF01BTOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.WF01BTOk.Location = new System.Drawing.Point(94, 112);
            this.WF01BTOk.Name = "WF01BTOk";
            this.WF01BTOk.Size = new System.Drawing.Size(75, 23);
            this.WF01BTOk.TabIndex = 10;
            this.WF01BTOk.Text = "OK";
            this.WF01BTOk.UseVisualStyleBackColor = true;
            this.WF01BTOk.Click += new System.EventHandler(this.WF01BTOk_Click);
            // 
            // WF01TBPassword
            // 
            this.WF01TBPassword.Location = new System.Drawing.Point(94, 75);
            this.WF01TBPassword.Name = "WF01TBPassword";
            this.WF01TBPassword.PasswordChar = '*';
            this.WF01TBPassword.Size = new System.Drawing.Size(156, 20);
            this.WF01TBPassword.TabIndex = 7;
            this.WF01TBPassword.Click += new System.EventHandler(this.WF01LblPassword_Click);
            // 
            // WF01LblPassword
            // 
            this.WF01LblPassword.AutoSize = true;
            this.WF01LblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.WF01LblPassword.Location = new System.Drawing.Point(20, 82);
            this.WF01LblPassword.Name = "WF01LblPassword";
            this.WF01LblPassword.Size = new System.Drawing.Size(53, 13);
            this.WF01LblPassword.TabIndex = 6;
            this.WF01LblPassword.Text = "Password";
            // 
            // WF01LblODBC
            // 
            this.WF01LblODBC.AutoSize = true;
            this.WF01LblODBC.BackColor = System.Drawing.SystemColors.MenuBar;
            this.WF01LblODBC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.WF01LblODBC.Location = new System.Drawing.Point(110, 9);
            this.WF01LblODBC.Name = "WF01LblODBC";
            this.WF01LblODBC.Size = new System.Drawing.Size(37, 13);
            this.WF01LblODBC.TabIndex = 4;
            this.WF01LblODBC.Text = "ODBC";
            // 
            // WF01LblLogin
            // 
            this.WF01LblLogin.AutoSize = true;
            this.WF01LblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.WF01LblLogin.Location = new System.Drawing.Point(40, 43);
            this.WF01LblLogin.Name = "WF01LblLogin";
            this.WF01LblLogin.Size = new System.Drawing.Size(33, 13);
            this.WF01LblLogin.TabIndex = 5;
            this.WF01LblLogin.Text = "Login";
            // 
            // in2SqlWF01Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 144);
            this.Controls.Add(this.WF01BTTest);
            this.Controls.Add(this.WF01BTCancel);
            this.Controls.Add(this.WF01BTOk);
            this.Controls.Add(this.WF01TBPassword);
            this.Controls.Add(this.WF01LblPassword);
            this.Controls.Add(this.WF01LblODBC);
            this.Controls.Add(this.WF01LblLogin);
            this.Controls.Add(this.WF01TBLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "in2SqlWF01Login";
            this.Text = "in2SqlWF01Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox WF01TBLogin;
        private System.Windows.Forms.Button WF01BTTest;
        private System.Windows.Forms.Button WF01BTCancel;
        private System.Windows.Forms.Button WF01BTOk;
        private System.Windows.Forms.TextBox WF01TBPassword;
        private System.Windows.Forms.Label WF01LblPassword;
        private System.Windows.Forms.Label WF01LblODBC;
        private System.Windows.Forms.Label WF01LblLogin;
    }
}