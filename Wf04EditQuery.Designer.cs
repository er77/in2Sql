namespace SqlEngine
{
    sealed partial class Wf04EditQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wf04EditQuery));
            this.SQLEditToolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Execute = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SqlEditor = new FastColoredTextBoxNS.FastColoredTextBox();
            this.SQLEditToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SqlEditor)).BeginInit();
            this.SuspendLayout();
            // 
            // SQLEditToolStrip
            // 
            this.SQLEditToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator1,
            this.Execute,
            this.toolStripSeparator3});
            this.SQLEditToolStrip.Location = new System.Drawing.Point(0, 0);
            this.SQLEditToolStrip.Name = "SQLEditToolStrip";
            this.SQLEditToolStrip.Size = new System.Drawing.Size(481, 25);
            this.SQLEditToolStrip.TabIndex = 2;
            this.SQLEditToolStrip.Text = "SQLEditToolStrip";
            this.SQLEditToolStrip.VisibleChanged += new System.EventHandler(this.SQLEditToolStrip_VisibleChanged);
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "New";
            this.newToolStripButton.Click += new System.EventHandler(this.SqlEditTol_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.SqlEditTol_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.SqlEditTol_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "Cut";
            this.cutToolStripButton.Click += new System.EventHandler(this.SqlEditTol_Click);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "Copy";
            this.copyToolStripButton.Click += new System.EventHandler(this.SqlEditTol_Click);
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "Paste";
            this.pasteToolStripButton.Click += new System.EventHandler(this.SqlEditTol_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Execute
            // 
            this.Execute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Execute.Image = ((System.Drawing.Image)(resources.GetObject("Execute.Image")));
            this.Execute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Execute.Name = "Execute";
            this.Execute.Size = new System.Drawing.Size(23, 22);
            this.Execute.Text = "Execute";
            this.Execute.Click += new System.EventHandler(this.SqlEditTol_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // SqlEditor
            // 
            this.SqlEditor.AutoCompleteBracketsList = new char[] {
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
            this.SqlEditor.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.SqlEditor.BackBrush = null;
            this.SqlEditor.CharHeight = 14;
            this.SqlEditor.CharWidth = 8;
            this.SqlEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SqlEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.SqlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SqlEditor.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.SqlEditor.IsReplaceMode = false;
            this.SqlEditor.Location = new System.Drawing.Point(0, 25);
            this.SqlEditor.Name = "SqlEditor";
            this.SqlEditor.Paddings = new System.Windows.Forms.Padding(0);
            this.SqlEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.SqlEditor.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("SqlEditor.ServiceColors")));
            this.SqlEditor.Size = new System.Drawing.Size(481, 440);
            this.SqlEditor.TabIndex = 3;
            this.SqlEditor.Zoom = 100;
            this.SqlEditor.Load += new System.EventHandler(this.SqlEditor_Load);
            // 
            // In2SqlWF04EditQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(481, 465);
            this.Controls.Add(this.SqlEditor);
            this.Controls.Add(this.SQLEditToolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "In2SqlWF04EditQuery";
            this.Text = "Sql Editor";
            this.Load += new System.EventHandler(this.In2SqlWF04EditQuery_Load);
            this.SQLEditToolStrip.ResumeLayout(false);
            this.SQLEditToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SqlEditor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip SQLEditToolStrip;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Execute;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private FastColoredTextBoxNS.FastColoredTextBox SqlEditor;
    }
}