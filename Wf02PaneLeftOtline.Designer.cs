namespace SqlEngine
{
    partial class Wf02PaneLeftOtline
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wf02PaneLeftOtline));
            this.treeExcelOtl = new System.Windows.Forms.TreeView();
            this.imageOutline = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuExcelRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuExTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuExSheet = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // treeExcelOtl
            // 
            this.treeExcelOtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeExcelOtl.ImageIndex = 0;
            this.treeExcelOtl.ImageList = this.imageOutline;
            this.treeExcelOtl.Location = new System.Drawing.Point(0, 0);
            this.treeExcelOtl.Name = "treeExcelOtl";
            this.treeExcelOtl.SelectedImageIndex = 0;
            this.treeExcelOtl.Size = new System.Drawing.Size(576, 601);
            this.treeExcelOtl.TabIndex = 0;
            this.treeExcelOtl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeExcelOtl_KeyPress);
            // 
            // imageOutline
            // 
            this.imageOutline.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageOutline.ImageStream")));
            this.imageOutline.TransparentColor = System.Drawing.Color.Transparent;
            this.imageOutline.Images.SetKeyName(0, "15_excel.png");
            this.imageOutline.Images.SetKeyName(1, "16_excel_sheet.png");
            this.imageOutline.Images.SetKeyName(2, "17_excel_table.png");
            this.imageOutline.Images.SetKeyName(3, "18_task_root.png");
            this.imageOutline.Images.SetKeyName(4, "18_excel_app.png");
            // 
            // contextMenuExcelRoot
            // 
            this.contextMenuExcelRoot.Name = "contextMenuExcelRoot";
            this.contextMenuExcelRoot.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuExTable
            // 
            this.contextMenuExTable.Name = "contextMenuExTable";
            this.contextMenuExTable.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuExSheet
            // 
            this.contextMenuExSheet.Name = "contextMenuExSheet";
            this.contextMenuExSheet.Size = new System.Drawing.Size(61, 4);
            this.contextMenuExSheet.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // in2SqlWF02PaneLeftOtline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeExcelOtl);
            this.Name = "in2SqlWF02PaneLeftOtline";
            this.Size = new System.Drawing.Size(576, 601);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeExcelOtl;
        private System.Windows.Forms.ImageList imageOutline;
        private System.Windows.Forms.ContextMenuStrip contextMenuExcelRoot;
        private System.Windows.Forms.ContextMenuStrip contextMenuExTable;
        private System.Windows.Forms.ContextMenuStrip contextMenuExSheet;
    }
}
