namespace SqlEngine
{
    partial class wf10CsvEdit
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
            this.CMBoxConnection = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.wf10Create = new System.Windows.Forms.Button();
            this.wf10Browse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CsvPath = new System.Windows.Forms.TextBox();
            this.CsvName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CMBoxConnection
            // 
            this.CMBoxConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMBoxConnection.FormattingEnabled = true;
            this.CMBoxConnection.Location = new System.Drawing.Point(30, 35);
            this.CMBoxConnection.Name = "CMBoxConnection";
            this.CMBoxConnection.Size = new System.Drawing.Size(121, 21);
            this.CMBoxConnection.TabIndex = 35;
            this.CMBoxConnection.SelectedIndexChanged += new System.EventHandler(this.CMBoxConnection_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button1.Location = new System.Drawing.Point(30, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 32;
            this.button1.Text = "delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // wf10Create
            // 
            this.wf10Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.wf10Create.Location = new System.Drawing.Point(119, 90);
            this.wf10Create.Name = "wf10Create";
            this.wf10Create.Size = new System.Drawing.Size(75, 25);
            this.wf10Create.TabIndex = 33;
            this.wf10Create.Text = "create";
            this.wf10Create.UseVisualStyleBackColor = true;
            this.wf10Create.Click += new System.EventHandler(this.wf10Create_Click);
            // 
            // wf10Browse
            // 
            this.wf10Browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.wf10Browse.Location = new System.Drawing.Point(316, 88);
            this.wf10Browse.Name = "wf10Browse";
            this.wf10Browse.Size = new System.Drawing.Size(75, 27);
            this.wf10Browse.TabIndex = 34;
            this.wf10Browse.Text = "browse";
            this.wf10Browse.UseVisualStyleBackColor = true;
            this.wf10Browse.Click += new System.EventHandler(this.wf10Browse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Connection name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "folder path  with CSV files";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // CsvPath
            // 
            this.CsvPath.Location = new System.Drawing.Point(224, 62);
            this.CsvPath.Name = "CsvPath";
            this.CsvPath.Size = new System.Drawing.Size(167, 20);
            this.CsvPath.TabIndex = 28;
            this.CsvPath.Text = "C:\\Temp";
            // 
            // CsvName
            // 
            this.CsvName.Location = new System.Drawing.Point(30, 62);
            this.CsvName.Name = "CsvName";
            this.CsvName.Size = new System.Drawing.Size(164, 20);
            this.CsvName.TabIndex = 29;
            this.CsvName.Text = "Temp";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button2.Location = new System.Drawing.Point(316, 159);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 27);
            this.button2.TabIndex = 34;
            this.button2.Text = "close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // in2SqlWF10CsvEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 198);
            this.Controls.Add(this.CMBoxConnection);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.wf10Create);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.wf10Browse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CsvPath);
            this.Controls.Add(this.CsvName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "in2SqlWF10CsvEdit";
            this.Text = "in2SqlWF10CsvEdit";
            this.Load += new System.EventHandler(this.in2SqlWF10CsvEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBoxConnection;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button wf10Create;
        private System.Windows.Forms.Button wf10Browse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CsvPath;
        private System.Windows.Forms.TextBox CsvName;
        private System.Windows.Forms.Button button2;
    }
}