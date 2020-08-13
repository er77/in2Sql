﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.Odbc;
using System.Data.Common;


namespace SqlEngine
{
    public partial class in2SqlWF03PanelRigtSqlM : UserControl
    {
        TreeNode miSelectNode;

        public static in2SqlWF03PanelRigtSqlM CurrSqlPanel;

        

        static void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // doneInitSQlObjects = true;
        }

         public void ShowOdbcTree() 
        {
            try
            { 
                this.splitContainer1.Panel2Collapsed = true;
                this.splitContainer1.Panel2.Hide(); 
                this.ODBCtabControl.Dock  = System.Windows.Forms.DockStyle.Fill;                
            }
            catch { }
        }

        public void showSqlEdit()
        { 
            try
            {
                this.splitContainer1.Panel2Collapsed = false;
                this.splitContainer1.Panel2.Show();                
                this.splitContainer1.SplitterDistance = 205;               
             //   this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
                this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            }
            catch { }
        } 

        private   void initParam ()
        {
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Panel2.Hide();
            this.splitContainer1.AutoSize = true;
            this.ODBCtabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeODBC.Dock = System.Windows.Forms.DockStyle.Fill;  
            this.treeODBC.NodeMouseClick +=
                new TreeNodeMouseClickEventHandler(this.treeODBC_NodeMouseClick); 
            this.treeODBC.KeyPress +=
                new KeyPressEventHandler(this.in2SqlRightPane_KeyPress); 
            this.treeODBC.AllowDrop = true; 


 
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("N1");
            tabControl1.TabPages[tabControl1.TabCount - 1].Text = "S0" + tabControl1.TabCount;

            in2SqlWF05SqlEngine sqlTab = new in2SqlWF05SqlEngine();
            sqlTab.TopLevel = false;
            sqlTab.FormBorderStyle = FormBorderStyle.None;
            sqlTab.Parent = tabControl1.TabPages[tabControl1.TabCount - 1];
            sqlTab.Dock = DockStyle.Fill;
            sqlTab.Show();
        }

        in2SqlWF06SqlBuilder sqlBuild = null;

        private void SqlBuild ()
        {
            sqlBuild = new in2SqlWF06SqlBuilder();
            sqlBuild.TopLevel = false;
            sqlBuild.FormBorderStyle = FormBorderStyle.None;
            sqlBuild.Parent = ODBCtabControl.TabPages[1];
            sqlBuild.Dock = DockStyle.Fill;
            sqlBuild.Show();
               
        }

        in2SqlWF07OdbcProperties OdbcEdit =  null;

        private void OdbcEditPanel ()
        {
            OdbcEdit = new in2SqlWF07OdbcProperties();
           // OdbcEdit.TopLevel = false;
           // OdbcEdit.FormBorderStyle = FormBorderStyle.None;
            OdbcEdit.Parent = ODBCtabControl.TabPages[2];
            OdbcEdit.Dock = DockStyle.Fill;
            OdbcEdit.Show();

        }

        

        public in2SqlWF03PanelRigtSqlM()
        {
            try
            {
                InitializeComponent();
                PopulateOdbcTreeView();
                initParam();
                newToolStripButton_Click(null, null);

                SqlBuild();
              //  OdbcEditPanel();
                CurrSqlPanel = this;

            }
            catch (Exception er)
            {
                In2SqlSvcTool.ExpHandler(er, "in2SqlRightPane");
            }
        }

        private void treeODBC_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var node = (TreeNode)e.Item;
            if ( node.Level > 0)
            {
                DoDragDrop(node.Text, DragDropEffects.Copy);
            }
        }



        public void PopulateOdbcTreeView(int vIsUI = 0)
        {
            try
            {
                TreeNode rootNode = new TreeNode("ODBC");
                rootNode.Tag = "root";
                GetODbcRecords(rootNode, vIsUI);
                treeODBC.Nodes.Add(rootNode); 

                TreeNode rootCloud = new TreeNode("Сloud", 18, 18);
                rootCloud.Tag = "cloud";

                TreeNode tnClickHouse = new TreeNode("ClickHouse" , 19, 19);
                tnClickHouse.Tag = "ClickHouse";
                GetCloudRecords(tnClickHouse, "CloudCH");
                rootCloud.Nodes.Add(tnClickHouse);

                treeODBC.Nodes.Add(rootCloud);

                TreeNode rootCSV = new TreeNode("csv", 20, 20);
                rootCSV.Tag = "CSV";
                GetCSVRecords(rootCSV);
                treeODBC.Nodes.Add(rootCSV);
            }
            catch (Exception er)
            {
                In2SqlSvcTool.ExpHandler(er, "PopulateOdbcTreeView");
            }
        }


        private void GetCloudRecords(TreeNode nodeToAddTo, string vCloudType)
        {
            try
            {
                in2sqlSvcCloud.vCloudList = in2sqlSvcCloud.CloudList();

                foreach (var vCurrCloudList in in2sqlSvcCloud.vCloudList)
                {
                    if (vCurrCloudList.CloudType.Contains(vCloudType))
                        in2SqlRightPaneTreeTables.setODBCTreeLineSimple(nodeToAddTo, vCurrCloudList.CloudName, vCloudType + '$');
                }
                return;
            }
            catch (Exception er)
            {
                In2SqlSvcTool.ExpHandler(er, "GetCloudRecords");
            }
        }

        private void GetCSVRecords(TreeNode nodeToAddTo )
        {
            try
            {
                In2SqlSvcCsv.vFolderList = In2SqlSvcCsv.FolderList();

                foreach (var vCurrFolder in In2SqlSvcCsv.vFolderList)
                {
                      in2SqlRightPaneTreeTables.setCSVTreeLineSimple(nodeToAddTo, vCurrFolder.FolderName,   "CSV$");
                }
                return;
            }
            catch (Exception er)
            {
                In2SqlSvcTool.ExpHandler(er, "GetCloudRecords");
            }
        }


        private void GetODbcRecords(TreeNode nodeToAddTo, int vIsUI = 0)
        {
            try
            {

                foreach (var vCurrCloudList in in2sqlSvcCloud.vCloudList)
                {
                    string vv = vCurrCloudList.CloudName;
                }
                

                if (vIsUI == 0)
                {
                    foreach (var vCurrvODBCList in In2SqlSvcODBC.vODBCList)
                    {
                        in2SqlRightPaneTreeTables.setODBCTreeLineSimple(nodeToAddTo, vCurrvODBCList.OdbcName);
                    }
                    return;
                }
                if (vIsUI == 1)
                {

                    foreach (var vCurrvODBCList in In2SqlSvcODBC.vODBCList)
                    {
                        In2SqlSvcODBC.checkOdbcStatus(vCurrvODBCList.OdbcName);
                        in2SqlRightPaneTreeTables.setODBCTreeLineComplex(nodeToAddTo, vCurrvODBCList.OdbcName, vCurrvODBCList.OdbcName);
                    }
                    return;
                }
            }
            catch (Exception er)
            {
                In2SqlSvcTool.ExpHandler(er, "GetODbcRecords");
            }
        }

     /*   private void refreshTreeOdbc_all(TreeNodeMouseClickEventArgs e)
        {
            try
            {
                e.Node.Nodes.Clear();
                PopulateOdbcTreeView(1);
            }
            catch (Exception er)
            {
                In2SqlSvcTool.ExpHandler(er, "refreshTreeOdbc_all");
            }
        }
        */
        private ContextMenuStrip createMenu(TreeNodeMouseClickEventArgs e, String[] vMenu, EventHandler myFuncName, ContextMenuStrip vCurrMenu)
        {            
            if (e.Node.ContextMenuStrip == null)
            {
                if (vCurrMenu.Items.Count < 1 )
                {
                    vCurrMenu.Items.Clear();

                    foreach (string rw in vMenu)
                    {
                        ToolStripMenuItem vMenuRun = new ToolStripMenuItem(rw);
                        vMenuRun.Click += myFuncName;
                        vCurrMenu.Items.Add(vMenuRun);
                    }
                }
                e.Node.ContextMenuStrip = vCurrMenu;
            }
            return vCurrMenu;
        }         

        private void rootMenu_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Edit"))
               In2SqlSvcTool.RunCmdLauncher("odbcad32");

            else if (sender.ToString().Contains("Refresh"))
            {
                miSelectNode.Nodes.Clear();
                GetODbcRecords(miSelectNode, 0);
            }

            else if (sender.ToString().Contains("ReConnect"))
            {
                miSelectNode.ImageIndex = 1;
                miSelectNode.Collapse();
                miSelectNode.SelectedImageIndex = 1;
                miSelectNode.Tag = "ODBC$";
            }

            else if (sender.ToString().Contains("Login"))
            {
                in2SqlWF01Login frm1 = new in2SqlWF01Login(miSelectNode.Text);//.Sho
                frm1.Show();
                miSelectNode.ImageIndex = 1;
                miSelectNode.Collapse();
                miSelectNode.SelectedImageIndex = 1;
                miSelectNode.Tag = "ODBC$";
            }
        }

        //

       

        private void clickCSV_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Edit"))
            {
                in2SqlWF10CsvEdit frmCsvEdit = new in2SqlWF10CsvEdit();
                frmCsvEdit.Show();
            }
           
            else if (sender.ToString().Contains("Refresh"))
            {
                miSelectNode.Nodes.Clear();
                GetCSVRecords(miSelectNode);
            }

         
        }

        private void clickHouse_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Edit"))
            {
                string vConnName = miSelectNode.Tag + "." + miSelectNode.Text;
                vConnName = vConnName.Replace("#","");
                vConnName = vConnName.Replace("$", "");

                in2SqlWF09CloudConnectionEditor frmshowCloudCHE =  new in2SqlWF09CloudConnectionEditor(vConnName);
                frmshowCloudCHE.Show();  
            }
            else if(sender.ToString().Contains("Create"))
            {
                in2SqlWF09CloudConnectionEditor frmshowCloudCHC = new in2SqlWF09CloudConnectionEditor();
                frmshowCloudCHC.Show(); 

            }
            else if (sender.ToString().Contains("Refresh"))
            {
                miSelectNode.Nodes.Clear();
                GetCloudRecords(miSelectNode, "CloudCH");
            }

            else if (sender.ToString().Contains("Delete"))
            {
                in2SqlRegistry.delLocalValue(miSelectNode.Tag + "." + miSelectNode.Text);

                miSelectNode.Text = "";
                miSelectNode.Tag = "";
                miSelectNode.Nodes.Clear();
                miSelectNode.ImageIndex = 990;  
            }

        }

        private TreeNode fTN = null;

        private void  FindTreeNode(TreeNode treeNode , string NodeName )
        { 

            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (fTN == null)
                    if (tn.Text.Equals(NodeName))
                    {
                        fTN = tn;
                    }
                    else
                        FindTreeNode(tn, NodeName);
                else
                    break;
            } 
        }


        public   TreeNode findeTable( string NodeName , string vODBCName )
        {

            // Find the node specified by the user.
            fTN = null;
            foreach (TreeNode tn in this.treeODBC.Nodes) {
                if (tn.Text.Equals("ODBC"))
                { foreach (TreeNode tn1 in tn.Nodes )
                    {
                        if (tn1.Text.Equals(vODBCName))
                        {
                            FindTreeNode(tn1, NodeName);
                        }
                        if ((fTN == null) == false)
                            break;
                    }
                   
                }
            }

            return fTN; 
         }
                  

        private void ExTableMenu_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Rename"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Edit Sql"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Refresh"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Properties"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Delete"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
        } 
 

        private void RegularObjecteMenu_Click(object sender, EventArgs e)
        {

            if (sender.ToString().Contains("PivotTable"))
                intSqlVBAEngine.createPivotTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);

            else if (sender.ToString().Contains("Table"))
                if (miSelectNode.Parent.Parent.Tag.ToString().Contains("Cloud"))
                    In2SqlVBAEngineCloud.createExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text,null);
                else
                     intSqlVBAEngine.createExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);
 
            else if (sender.ToString().Contains("Chart"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Editor"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Properties"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

 

         private void treeODBC_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                miSelectNode = treeODBC.GetNodeAt(e.X, e.Y);

                if (e.Node.Tag != null)
                {
                    if ((e.Button == MouseButtons.Right) & (e.Node.Tag.ToString().Contains("root")))
                    { 
                        contextMenuRootODBC = createMenu( 
                                        e
                                    , new String[] { "Edit", "Refresh" }
                                    , rootMenu_Click
                                    , contextMenuRootODBC);
                        return;

                    }

                    if ((e.Button == MouseButtons.Right) & (e.Node.Tag.ToString().ToUpper().Contains("CLOUD")) & (e.Node.Tag.ToString().ToUpper().Contains("CH")))
                    {
                        contextMenuEditCH = createMenu(
                                        e
                                    , new String[] { "Edit", "Delete" }
                                    , clickHouse_Click
                                    , contextMenuEditCH);
                        return;
                    } 

                    if ((e.Button == MouseButtons.Right) & (e.Node.Tag.ToString().ToUpper().Contains("CSV")) & (e.Node.Text.ToString().ToUpper().Contains("CSV")))
                    {
                        contextMenuEditCH = createMenu(
                                        e
                                    , new String[] { "Edit", "Refresh" }
                                    , clickCSV_Click
                                    , contextMenuCSV);
                        return;
                    }

                    if ((e.Button == MouseButtons.Right) & (e.Node.Tag.ToString().ToUpper().Contains("CLICKHOUSE")))
                    {
                        contextMenuCHRoot = createMenu(
                                        e
                                    , new String[] { "Create", "Refresh" }
                                    , clickHouse_Click
                                    , contextMenuCHRoot);
                        return;
                    }



                    if ((e.Button == MouseButtons.Right) & (e.Node.Tag.ToString().Contains("ODBC%")))
                    {
                        contextMenuOdbcError = createMenu(
                                   e
                                   , new String[] { "ReConnect", "Edit", "Login" }
                                   , rootMenu_Click
                                   , contextMenuOdbcError);
                        return;
                    }

                    if (e.Node.Parent == null)
                        return;

                        if ((e.Button == MouseButtons.Left) & (e.Node.Tag.ToString().ToUpper().Contains("CLOUD")) & (e.Node.Tag.ToString().Contains("$")))
                        {
                            in2SqlRightPaneTreeCloud.getCloudTablesAndViews(e);
                            //   sqlBuild.setLblConnectionName(e.Node.Text);
                            return;
                        }


                    if ((e.Button == MouseButtons.Left) & (e.Node.Tag.ToString().Contains("ODBC$")))
                        {
                            in2SqlRightPaneTreeTables.getTablesAndViews(e);
                            sqlBuild.setLblConnectionName(e.Node.Text, "ODBC");
                            return;
                        }

                        if ((e.Button == MouseButtons.Left) & (e.Node.Tag.ToString().ToUpper().Contains("CSV")) & (e.Node.Tag.ToString().Contains("$")) & (e.Node.Parent.Text.ToString().ToUpper().Contains("CSV")))
                        {
                            in2SqlRightPaneTreeCloud.getCsvFilesList(e);
                            sqlBuild.setLblConnectionName(e.Node.Text, "CSV");
                            return;
                        }

                    if ((e.Button == MouseButtons.Left) & (e.Node.Tag.ToString().ToUpper().Contains("CSV")) & (e.Node.Tag.ToString().Contains("$")) & (e.Node.Parent.Text.ToString().ToUpper().Contains("CSV")))
                    {
                        in2SqlRightPaneTreeCloud.getCsvFilesList(e);
                        sqlBuild.setLblConnectionName(e.Node.Text, "CSV");
                        return;
                    }

                    if ((e.Button == MouseButtons.Left) & (e.Node.Tag.ToString().ToUpper().Contains("$FILE_CSV$")) )
                        {
                            in2SqlRightPaneTreeCloud.getCsvHeaders(e);
                        return;
                        }

                    if ((e.Node.Tag.ToString().Contains("VIEW") | e.Node.Tag.ToString().Contains("TABLE")))
                    {
                        if (e.Button == MouseButtons.Left | e.Node.Tag.ToString().Contains('$'))
                        {
                            if (e.Node.Tag.ToString().Contains("CLD") | e.Node.Parent.Parent.Tag.ToString().Contains("Cloud"))
                            {
                                in2SqlRightPaneTreeCloud.getColumnsAndIndexes(e);
                                return;
                            }
                            else
                            {
                                in2SqlRightPaneTreeTables.getColumnsandIndexes(e);
                                return;

                            }

                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            this.contextMenuTable = createMenu(
                                e
                                , new String[] { "to Table", "to PivotTable", "to Sql Editor", "get Properties" }
                                , RegularObjecteMenu_Click
                                , this.contextMenuTable);
                            return;
                        }
                    }                    
                }
            }
            catch (Exception er)
            {
                In2SqlSvcTool.ExpHandler(er, "treeODBC_NodeMouseClick 2 ");
            }

        }

        private void treeODBC_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                /*  string selectedNodeText = e.Node.Text;
                  DialogResult  result = MessageBox.Show(selectedNodeText + "  1");
                  */
            }
            catch (Exception er)
            {
                In2SqlSvcTool.ExpHandler(er, "treeODBC_AfterSelect");
            }
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
            }
            catch (Exception er)
            {
                In2SqlSvcTool.ExpHandler(er, "treeODBC_AfterSelect");
            }
        }



        private void in2SqlRightPane_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\u0003')  //Control+C
                if (treeODBC.SelectedNode != null)
                {
                    Clipboard.SetText(treeODBC.SelectedNode.Text);
                }
        }

        private void contextMenuTable_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
