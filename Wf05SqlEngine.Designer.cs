namespace SqlEngine
{
    partial class Wf05SqlEngine
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wf05SqlEngine));
            this.contextMenuSqlConnections = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SqlConnectionsToolStripDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SqlDocument = new FastColoredTextBoxNS.FastColoredTextBox();
            this.ConnName = new System.Windows.Forms.Label();
            this.toolSqlConnections = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSqlRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveToExTable = new System.Windows.Forms.ToolStripSplitButton();
            this.SqlResult = new System.Windows.Forms.TabControl();
            this.SQLDataGrid = new System.Windows.Forms.TabPage();
            this.SqlDataResult = new System.Windows.Forms.DataGridView();
            this.SQLHistory = new System.Windows.Forms.TabPage();
            this.SqlHistoryLog = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SqlDocument)).BeginInit();
            this.toolSqlConnections.SuspendLayout();
            this.SqlResult.SuspendLayout();
            this.SQLDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SqlDataResult)).BeginInit();
            this.SQLHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuSqlConnections
            // 
            this.contextMenuSqlConnections.Name = "contextMenuSqlConnections";
            this.contextMenuSqlConnections.OwnerItem = this.SqlConnectionsToolStripDropDown;
            this.contextMenuSqlConnections.Size = new System.Drawing.Size(61, 4);
            this.contextMenuSqlConnections.Click += new System.EventHandler(this.Connection_Click);
            // 
            // SqlConnectionsToolStripDropDown
            // 
            this.SqlConnectionsToolStripDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SqlConnectionsToolStripDropDown.DropDown = this.contextMenuSqlConnections;
            this.SqlConnectionsToolStripDropDown.Image = ((System.Drawing.Image)(resources.GetObject("SqlConnectionsToolStripDropDown.Image")));
            this.SqlConnectionsToolStripDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SqlConnectionsToolStripDropDown.Name = "SqlConnectionsToolStripDropDown";
            this.SqlConnectionsToolStripDropDown.Size = new System.Drawing.Size(29, 22);
            this.SqlConnectionsToolStripDropDown.Text = "SqlConnections";
            this.SqlConnectionsToolStripDropDown.ToolTipText = "SqlConnections";
            this.SqlConnectionsToolStripDropDown.Click += new System.EventHandler(this.SqlConnectionsToolStripDropDown_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SqlDocument);
            this.splitContainer1.Panel1.Controls.Add(this.ConnName);
            this.splitContainer1.Panel1.Controls.Add(this.toolSqlConnections);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SqlResult);
            this.splitContainer1.Size = new System.Drawing.Size(591, 797);
            this.splitContainer1.SplitterDistance = 480;
            this.splitContainer1.TabIndex = 7;
            // 
            // SqlDocument
            // 
            this.SqlDocument.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.SqlDocument.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.SqlDocument.AutoSize = true;
            this.SqlDocument.BackBrush = null;
            this.SqlDocument.CharHeight = 14;
            this.SqlDocument.CharWidth = 8;
            this.SqlDocument.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SqlDocument.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.SqlDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SqlDocument.IsReplaceMode = false;
            this.SqlDocument.Location = new System.Drawing.Point(0, 25);
            this.SqlDocument.Name = "SqlDocument";
            this.SqlDocument.Paddings = new System.Windows.Forms.Padding(0);
            this.SqlDocument.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.SqlDocument.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("SqlDocument.ServiceColors")));
            this.SqlDocument.Size = new System.Drawing.Size(591, 455);
            this.SqlDocument.TabIndex = 9;
            this.SqlDocument.Text = "fastColoredTextBox1";
            this.SqlDocument.Zoom = 100;
            this.SqlDocument.Load += new System.EventHandler(this.SqlDocument_Load);
            // 
            // ConnName
            // 
            this.ConnName.AutoSize = true;
            this.ConnName.Location = new System.Drawing.Point(262, 5);
            this.ConnName.Name = "ConnName";
            this.ConnName.Size = new System.Drawing.Size(0, 13);
            this.ConnName.TabIndex = 8;
            // 
            // toolSqlConnections
            // 
            this.toolSqlConnections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator2,
            this.toolSqlRun,
            this.toolStripSeparator3,
            this.SaveToExTable,
            this.SqlConnectionsToolStripDropDown});
            this.toolSqlConnections.Location = new System.Drawing.Point(0, 0);
            this.toolSqlConnections.Name = "toolSqlConnections";
            this.toolSqlConnections.Size = new System.Drawing.Size(591, 25);
            this.toolSqlConnections.TabIndex = 6;
            this.toolSqlConnections.Text = "SqlConnections";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "New";
            this.newToolStripButton.Click += new System.EventHandler(this.EditTollMenu_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.Click += new System.EventHandler(this.EditTollMenu_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.EditTollMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "Cut";
            this.cutToolStripButton.Click += new System.EventHandler(this.EditTollMenu_Click);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "Copy";
            this.copyToolStripButton.Click += new System.EventHandler(this.EditTollMenu_Click);
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "Paste";
            this.pasteToolStripButton.Click += new System.EventHandler(this.EditTollMenu_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolSqlRun
            // 
            this.toolSqlRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSqlRun.Image = ((System.Drawing.Image)(resources.GetObject("toolSqlRun.Image")));
            this.toolSqlRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSqlRun.Name = "toolSqlRun";
            this.toolSqlRun.Size = new System.Drawing.Size(23, 22);
            this.toolSqlRun.Text = "sqlRun";
            this.toolSqlRun.Click += new System.EventHandler(this.EditTollMenu_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveToExTable
            // 
            this.SaveToExTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToExTable.Image = ((System.Drawing.Image)(resources.GetObject("SaveToExTable.Image")));
            this.SaveToExTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToExTable.Name = "SaveToExTable";
            this.SaveToExTable.Size = new System.Drawing.Size(32, 22);
            this.SaveToExTable.Text = "Excel Table";
            this.SaveToExTable.ButtonClick += new System.EventHandler(this.SaveToExTable_ButtonClick);
            this.SaveToExTable.Click += new System.EventHandler(this.EditTollMenu_Click);
            // 
            // SqlResult
            // 
            this.SqlResult.Controls.Add(this.SQLDataGrid);
            this.SqlResult.Controls.Add(this.SQLHistory);
            this.SqlResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.SqlResult.Location = new System.Drawing.Point(0, 0);
            this.SqlResult.Name = "SqlResult";
            this.SqlResult.SelectedIndex = 0;
            this.SqlResult.Size = new System.Drawing.Size(591, 516);
            this.SqlResult.TabIndex = 1;
            // 
            // SQLDataGrid
            // 
            this.SQLDataGrid.Controls.Add(this.SqlDataResult);
            this.SQLDataGrid.Location = new System.Drawing.Point(4, 22);
            this.SQLDataGrid.Name = "SQLDataGrid";
            this.SQLDataGrid.Padding = new System.Windows.Forms.Padding(3);
            this.SQLDataGrid.Size = new System.Drawing.Size(583, 490);
            this.SQLDataGrid.TabIndex = 0;
            this.SQLDataGrid.Text = "data";
            this.SQLDataGrid.UseVisualStyleBackColor = true;
            // 
            // SqlDataResult
            // 
            this.SqlDataResult.AllowUserToAddRows = false;
            this.SqlDataResult.AllowUserToDeleteRows = false;
            this.SqlDataResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.SqlDataResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.SqlDataResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SqlDataResult.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.SqlDataResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SqlDataResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SqlDataResult.Location = new System.Drawing.Point(3, 3);
            this.SqlDataResult.Name = "SqlDataResult";
            this.SqlDataResult.ReadOnly = true;
            this.SqlDataResult.Size = new System.Drawing.Size(577, 484);
            this.SqlDataResult.TabIndex = 0;
            // 
            // SQLHistory
            // 
            this.SQLHistory.Controls.Add(this.SqlHistoryLog);
            this.SQLHistory.Location = new System.Drawing.Point(4, 22);
            this.SQLHistory.Name = "SQLHistory";
            this.SQLHistory.Padding = new System.Windows.Forms.Padding(3);
            this.SQLHistory.Size = new System.Drawing.Size(583, 490);
            this.SQLHistory.TabIndex = 2;
            this.SQLHistory.Text = "history";
            this.SQLHistory.UseVisualStyleBackColor = true;
            // 
            // SqlHistoryLog
            // 
            this.SqlHistoryLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SqlHistoryLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SqlHistoryLog.Location = new System.Drawing.Point(3, 3);
            this.SqlHistoryLog.Name = "SqlHistoryLog";
            this.SqlHistoryLog.ReadOnly = true;
            this.SqlHistoryLog.Size = new System.Drawing.Size(577, 484);
            this.SqlHistoryLog.TabIndex = 1;
            this.SqlHistoryLog.Text = "";
            // 
            // in2SqlWF05SqlEngine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 797);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "in2SqlWF05SqlEngine";
            this.Text = "in2SqlWF05SqlEngine";
            this.Load += new System.EventHandler(this.in2SqlWF05SqlEngine_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SqlDocument)).EndInit();
            this.toolSqlConnections.ResumeLayout(false);
            this.toolSqlConnections.PerformLayout();
            this.SqlResult.ResumeLayout(false);
            this.SQLDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SqlDataResult)).EndInit();
            this.SQLHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuSqlConnections;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl SqlResult;
        private System.Windows.Forms.TabPage SQLDataGrid;
        private System.Windows.Forms.DataGridView SqlDataResult;
        private System.Windows.Forms.TabPage SQLHistory;
        private System.Windows.Forms.RichTextBox SqlHistoryLog;
        private System.Windows.Forms.ToolStripDropDownButton SqlConnectionsToolStripDropDown;
        private System.Windows.Forms.ToolStrip toolSqlConnections;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolSqlRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSplitButton SaveToExTable;
        private System.Windows.Forms.Label ConnName;
        private FastColoredTextBoxNS.FastColoredTextBox SqlDocument;
    }
}