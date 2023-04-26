namespace SqlEngine
{
    partial class wf03PanelRigtSqlM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wf03PanelRigtSqlM));
            this.ODBCImages = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuExcelRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuRootODBC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuOdbcError = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuExTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.treeODBC = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ODBCtabControl = new System.Windows.Forms.TabControl();
            this.tabSQL = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Joiner = new System.Windows.Forms.TabPage();
            this.contextMenuSqlConnections = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuEditCH = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuCHRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuCSV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuSqLiteRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuTableSQLite = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuSQLiteFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.ODBCtabControl.SuspendLayout();
            this.tabSQL.SuspendLayout();
            this.SuspendLayout();
            // 
            // ODBCImages
            // 
            this.ODBCImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ODBCImages.ImageStream")));
            this.ODBCImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ODBCImages.Images.SetKeyName(0, "00_odbc.png");
            this.ODBCImages.Images.SetKeyName(1, "01_db_disconnect.png");
            this.ODBCImages.Images.SetKeyName(2, "02_db_connected.png");
            this.ODBCImages.Images.SetKeyName(3, "03_table_folder.png");
            this.ODBCImages.Images.SetKeyName(4, "04_table.png");
            this.ODBCImages.Images.SetKeyName(5, "05_view_folder.png");
            this.ODBCImages.Images.SetKeyName(6, "06_view.png");
            this.ODBCImages.Images.SetKeyName(7, "07_db_errconn.png");
            this.ODBCImages.Images.SetKeyName(8, "08_prg_folder.png");
            this.ODBCImages.Images.SetKeyName(9, "09_prg.png");
            this.ODBCImages.Images.SetKeyName(10, "10_funct_folder.png");
            this.ODBCImages.Images.SetKeyName(11, "11_prg_list.png");
            this.ODBCImages.Images.SetKeyName(12, "12_index_folder.png");
            this.ODBCImages.Images.SetKeyName(13, "13_index.png");
            this.ODBCImages.Images.SetKeyName(14, "14_column.png");
            this.ODBCImages.Images.SetKeyName(15, "15_excel.png");
            this.ODBCImages.Images.SetKeyName(16, "16_excel_sheet.png");
            this.ODBCImages.Images.SetKeyName(17, "17_excel_table.png");
            this.ODBCImages.Images.SetKeyName(18, "22_cloud.png");
            this.ODBCImages.Images.SetKeyName(19, "23_CH.png");
            this.ODBCImages.Images.SetKeyName(20, "24_csv.png");
            this.ODBCImages.Images.SetKeyName(21, "25_csv.png");
            this.ODBCImages.Images.SetKeyName(22, "26_csv_file.png");
            this.ODBCImages.Images.SetKeyName(23, "27_sqlite.png");
            // 
            // contextMenuTable
            // 
            this.contextMenuTable.Name = "contextMenuStrip1";
            this.contextMenuTable.Size = new System.Drawing.Size(61, 4);
            this.contextMenuTable.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuTable_Opening);
            // 
            // contextMenuExcelRoot
            // 
            this.contextMenuExcelRoot.Name = "contextMenuExcelRoot";
            this.contextMenuExcelRoot.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuRootODBC
            // 
            this.contextMenuRootODBC.Name = "contextMenuRootODBC";
            this.contextMenuRootODBC.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuOdbcError
            // 
            this.contextMenuOdbcError.Name = "contextMenuOdbcError";
            this.contextMenuOdbcError.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuExTable
            // 
            this.contextMenuExTable.Name = "contextMenuExTable";
            this.contextMenuExTable.Size = new System.Drawing.Size(61, 4);
            // 
            // treeODBC
            // 
            this.treeODBC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeODBC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeODBC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.treeODBC.ImageIndex = 0;
            this.treeODBC.ImageList = this.ODBCImages;
            this.treeODBC.Location = new System.Drawing.Point(0, 0);
            this.treeODBC.Name = "treeODBC";
            this.treeODBC.SelectedImageIndex = 0;
            this.treeODBC.Size = new System.Drawing.Size(816, 756);
            this.treeODBC.TabIndex = 2;
            this.treeODBC.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeODBC_ItemDrag);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ODBCtabControl);
            this.splitContainer1.Size = new System.Drawing.Size(1540, 781);
            this.splitContainer1.SplitterDistance = 816;
            this.splitContainer1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeODBC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 756);
            this.panel1.TabIndex = 4;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(816, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // ODBCtabControl
            // 
            this.ODBCtabControl.Controls.Add(this.tabSQL);
            this.ODBCtabControl.Controls.Add(this.Joiner);
            this.ODBCtabControl.Location = new System.Drawing.Point(3, 3);
            this.ODBCtabControl.Name = "ODBCtabControl";
            this.ODBCtabControl.SelectedIndex = 0;
            this.ODBCtabControl.Size = new System.Drawing.Size(785, 800);
            this.ODBCtabControl.TabIndex = 5;
            // 
            // tabSQL
            // 
            this.tabSQL.Controls.Add(this.tabControl1);
            this.tabSQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabSQL.Location = new System.Drawing.Point(4, 22);
            this.tabSQL.Name = "tabSQL";
            this.tabSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tabSQL.Size = new System.Drawing.Size(777, 774);
            this.tabSQL.TabIndex = 0;
            this.tabSQL.Text = "Engine";
            this.tabSQL.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(774, 774);
            this.tabControl1.TabIndex = 0;
            // 
            // Joiner
            // 
            this.Joiner.AllowDrop = true;
            this.Joiner.Location = new System.Drawing.Point(4, 22);
            this.Joiner.Name = "Joiner";
            this.Joiner.Padding = new System.Windows.Forms.Padding(3);
            this.Joiner.Size = new System.Drawing.Size(777, 774);
            this.Joiner.TabIndex = 4;
            this.Joiner.Text = "AutoBuilder";
            this.Joiner.UseVisualStyleBackColor = true;
            // 
            // contextMenuSqlConnections
            // 
            this.contextMenuSqlConnections.Name = "contextMenuSqlConnections";
            this.contextMenuSqlConnections.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuEditCH
            // 
            this.contextMenuEditCH.Name = "contextMenuEditCH";
            this.contextMenuEditCH.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuCHRoot
            // 
            this.contextMenuCHRoot.Name = "contextMenuCHRoot";
            this.contextMenuCHRoot.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuCSV
            // 
            this.contextMenuCSV.Name = "contextMenuExTable";
            this.contextMenuCSV.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuSqLiteRoot
            // 
            this.contextMenuSqLiteRoot.Name = "contextMenuSqLiteRoot";
            this.contextMenuSqLiteRoot.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuTableSQLite
            // 
            this.contextMenuTableSQLite.Name = "contextMenuTableSQLite";
            this.contextMenuTableSQLite.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuSQLiteFolder
            // 
            this.contextMenuSQLiteFolder.Name = "contextMenuSQLiteFolder";
            this.contextMenuSQLiteFolder.Size = new System.Drawing.Size(181, 26);
            // 
            // in2SqlWF03PanelRigtSqlM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "in2SqlWF03PanelRigtSqlM";
            this.Size = new System.Drawing.Size(1540, 781);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ODBCtabControl.ResumeLayout(false);
            this.tabSQL.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList ODBCImages;
        private System.Windows.Forms.ContextMenuStrip contextMenuTable;
        private System.Windows.Forms.ContextMenuStrip contextMenuExcelRoot;
        private System.Windows.Forms.ContextMenuStrip contextMenuRootODBC;
        private System.Windows.Forms.ContextMenuStrip contextMenuOdbcError;
        private System.Windows.Forms.ContextMenuStrip contextMenuExTable;
        private System.Windows.Forms.TreeView treeODBC;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuSqlConnections;
        private System.Windows.Forms.TabControl ODBCtabControl;
        private System.Windows.Forms.TabPage Joiner;
        private System.Windows.Forms.TabPage tabSQL;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuEditCH;
        private System.Windows.Forms.ContextMenuStrip contextMenuCHRoot;
        private System.Windows.Forms.ContextMenuStrip contextMenuCSV;
        private System.Windows.Forms.ContextMenuStrip contextMenuSqLiteRoot;
        private System.Windows.Forms.ContextMenuStrip contextMenuTableSQLite;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuSQLiteFolder;
    }
}
