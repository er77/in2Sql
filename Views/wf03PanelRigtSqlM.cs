using System;
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
    public partial class wf03PanelRigtSqlM : UserControl
    {
        TreeNode miSelectNode;

        public static wf03PanelRigtSqlM CurrSqlPanel;

        

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

            contextMenuTableSQLite = null;
            contextMenuTable = null;
            contextMenuRootODBC = null;
            contextMenuEditCH = null;
            contextMenuCSV = null;
            contextMenuCHRoot = null;
            contextMenuSqLiteRoot = null;
            contextMenuOdbcError = null;
            contextMenuSQLiteFolder = null;

        }

        public static TreeNode getNode ( int X, int Y )

        {
            return
                  CurrSqlPanel.treeODBC.GetNodeAt(X, Y); 
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("N1");
            tabControl1.TabPages[tabControl1.TabCount - 1].Text = "S0" + tabControl1.TabCount;

            wf05SqlEngine sqlTab = new wf05SqlEngine();
            sqlTab.TopLevel = false;
            sqlTab.FormBorderStyle = FormBorderStyle.None;
            sqlTab.Parent = tabControl1.TabPages[tabControl1.TabCount - 1];
            sqlTab.Dock = DockStyle.Fill;
            sqlTab.Show();
        }

        wf06SqlBuilder sqlBuild = null;

        private void SqlBuild ()
        {
            sqlBuild = new wf06SqlBuilder();
            sqlBuild.TopLevel = false;
            sqlBuild.FormBorderStyle = FormBorderStyle.None;
            sqlBuild.Parent = ODBCtabControl.TabPages[1];
            sqlBuild.Dock = DockStyle.Fill;
            sqlBuild.Show();
               
        }

        wf07OdbcProperties OdbcEdit =  null;

        private void OdbcEditPanel ()
        {
            OdbcEdit = new wf07OdbcProperties();
           // OdbcEdit.TopLevel = false;
           // OdbcEdit.FormBorderStyle = FormBorderStyle.None;
            OdbcEdit.Parent = ODBCtabControl.TabPages[2];
            OdbcEdit.Dock = DockStyle.Fill;
            OdbcEdit.Show();

        }

        

        public wf03PanelRigtSqlM()
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
                sTool.ExpHandler(er, "in2SqlRightPane");
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
                sTool.ExpHandler(er, "PopulateOdbcTreeView");
            }
        }


        private void GetCloudRecords(TreeNode nodeToAddTo, string vCloudType)
        {
            try
            {
                sCloud.vCloudList = sCloud.CloudList();

                foreach (var vCurrCloudList in sCloud.vCloudList)
                {
                    if (vCurrCloudList.CloudType.Contains(vCloudType))
                        wp02RightTreeTables.setODBCTreeLineSimple(nodeToAddTo, vCurrCloudList.CloudName, vCloudType + '$');
                }
                return;
            }
            catch (Exception er)
            {
                sTool.ExpHandler(er, "GetCloudRecords");
            }
        } 

        private void GetCSVRecords(TreeNode nodeToAddTo )
        {
            try
            {
                sCsv.vFolderList = sCsv.FolderList();

                foreach (var vCurrFolder in sCsv.vFolderList)
                {
                      wp02RightTreeTables.setCSVTreeLineSimple(nodeToAddTo, vCurrFolder.FolderName,   "CSV$");
                }
                return;
            }
            catch (Exception er)
            {
                sTool.ExpHandler(er, "GetCloudRecords");
            }
        }


        private void GetODbcRecords(TreeNode nodeToAddTo, int vIsUI = 0)
        {
            try
            {

                foreach (var vCurrCloudList in sCloud.vCloudList)
                {
                    string vv = vCurrCloudList.CloudName;
                }
                

                if (vIsUI == 0)
                {
                    foreach (var vCurrvODBCList in sODBC.vODBCList)
                    {
                        wp02RightTreeTables.setODBCTreeLineSimple(nodeToAddTo, vCurrvODBCList.OdbcName);
                    }
                    return;
                }
                if (vIsUI == 1)
                {

                    foreach (var vCurrvODBCList in sODBC.vODBCList)
                    {
                        sODBC.checkOdbcStatus(vCurrvODBCList.OdbcName);
                        wp02RightTreeTables.setODBCTreeLineComplex(nodeToAddTo, vCurrvODBCList.OdbcName, vCurrvODBCList.OdbcName);
                    }
                    return;
                }
            }
            catch (Exception er)
            {
                sTool.ExpHandler(er, "GetODbcRecords");
            }
        } 
      
        private ContextMenuStrip createMenu(TreeNodeMouseClickEventArgs e, String[] vMenu, EventHandler myFuncName, ContextMenuStrip vCMenu)
        {          
            if (vCMenu == null)
             {
                ContextMenuStrip vCurrMenu = new ContextMenuStrip();
                foreach (string rw in vMenu)
                {
                    ToolStripMenuItem vMenuRun = new ToolStripMenuItem(rw);
                    vMenuRun.Click += myFuncName;
                    vCurrMenu.Items.Add(vMenuRun);
                }
                vCMenu = vCurrMenu;
            }

              e.Node.ContextMenuStrip = vCMenu;
            
            return vCMenu;
        }         

        private void rootMenu_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Edit"))
               sTool.RunCmdLauncher("odbcad32");

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
                wf01Login frm1 = new wf01Login(miSelectNode.Text);//.Sho
                frm1.Show();
                miSelectNode.ImageIndex = 1;
                miSelectNode.Collapse();
                miSelectNode.SelectedImageIndex = 1;
                miSelectNode.Tag = "ODBC$";
            }
        } 

        private void clickCSV_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Edit"))
            {
                wf10CsvEdit frmCsvEdit = new wf10CsvEdit();
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

                wf09CloudConnectionEditor frmshowCloudCHE =  new wf09CloudConnectionEditor(vConnName);
                frmshowCloudCHE.Show();  
            }
            else if(sender.ToString().Contains("Create"))
            {
                wf09CloudConnectionEditor frmshowCloudCHC = new wf09CloudConnectionEditor();
                frmshowCloudCHC.Show(); 

            }
            else if (sender.ToString().Contains("Refresh"))
            {
                miSelectNode.Nodes.Clear();
                GetCloudRecords(miSelectNode, "CloudCH");
            }

            else if (sender.ToString().Contains("Delete"))
            {
                sRegistry.delLocalValue(miSelectNode.Tag + "." + miSelectNode.Text);

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
            fTN = null;
            foreach (TreeNode tn in this.treeODBC.Nodes) {             
                 foreach (TreeNode tn1 in tn.Nodes )
                   { FindTreeNode(tn1, NodeName);                        
                        if ((fTN == null) == false)
                            break;
                    }                                   
            } 
                return fTN; 
        }

        //


        private void SQLiteFolder_Click(object sender, EventArgs e)
        {  //{ "Create SQLiteDatabase", "Refresh" }

            if (sender.ToString().Contains("Create SQLiteDatabase"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Refresh"))
                miSelectNode.Nodes.Clear();

            else 
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);


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
                VbaEngine.createPivotTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);

            else if (sender.ToString().Contains("Table"))
                if (miSelectNode.Parent.Parent.Tag.ToString().Contains("Cloud"))
                    sVbaEngineCloud.createExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text,null);             

                else
                     VbaEngine.createExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);
            /* fix me */
            else if (sender.ToString().Contains("generate CSV"))
                if (miSelectNode.Parent.Parent.Tag.ToString().ToUpper().Contains("ODBC"))
                    sODBC.dumpOdbctoCsv(
                                     miSelectNode.Parent.Parent.Text
                                   , "select * from  " + miSelectNode.Text
                                   , sCsv.getFirstFolder() + miSelectNode.Text + ".csv");                 

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

        private void contextMenu(TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return;

            if (e.Node.ContextMenuStrip == null)
            {
                if ((e.Node.Tag.ToString().Contains("clm")))
                { 
                    return;
                }

                string vNodeTag = e.Node.Tag.ToString().ToUpper();

                if (vNodeTag.Contains("FLD")  & vNodeTag.Contains("SQLITE") ) 
                {
                    contextMenuSQLiteFolder = createMenu(
                                    e
                                , new String[] { "Create SQLiteDatabase", "Refresh" }
                                , SQLiteFolder_Click
                                , contextMenuSQLiteFolder);
                    return;
                }

                if ((vNodeTag.Contains("VIEW") | vNodeTag.Contains("TABLE")))
                    if (miSelectNode.Parent.Parent.Tag.ToString().ToUpper().Contains("SQLITE"))
                    { 
                            this.contextMenuTableSQLite = createMenu(
                              e
                              , new String[] { "to Table", "generate CSV" }
                              , RegularObjecteMenu_Click
                              , this.contextMenuTableSQLite);
                            return;                        
                    }
                    else
                    {                         
                            this.contextMenuTable = createMenu(
                         e
                         , new String[] { "to Table", "to PivotTable", "generate CSV" }
                         , RegularObjecteMenu_Click
                         , this.contextMenuTable);
                            return;                        
                    }

                if ((vNodeTag.Contains("ROOT")))
                {                     
                        contextMenuRootODBC = createMenu(
                                        e
                                    , new String[] { "Edit", "Refresh" }
                                    , rootMenu_Click
                                    , contextMenuRootODBC);                    
                        return;
                }

                if ((vNodeTag.Contains("CLOUD")) & (vNodeTag.Contains("CH")))
                {
                     
                        contextMenuEditCH = createMenu(
                                        e
                                    , new String[] { "Edit", "Delete" }
                                    , clickHouse_Click
                                    , contextMenuEditCH);
                        return;
                    
                }

                if ((vNodeTag.Contains("CSV")) & (vNodeTag.Contains("CSV")))
                {                    
                        contextMenuCSV = createMenu(
                                        e
                                    , new String[] { "Edit", "Refresh" }
                                    , clickCSV_Click
                                    , contextMenuCSV);
                        return;                    
                }

                if ((vNodeTag.Contains("CLICKHOUSE")))
                {                    
                        contextMenuCHRoot = createMenu(
                                        e
                                    , new String[] { "Create", "Refresh" }
                                    , clickHouse_Click
                                    , contextMenuCHRoot);
                        return;                    
                } 

                if ((vNodeTag.Contains("ODBC%")))
                { 
                        contextMenuOdbcError = createMenu(
                                    e
                                    , new String[] { "ReConnect", "Edit", "Login" }
                                    , rootMenu_Click
                                    , contextMenuOdbcError);
                        return;                     
                }
            }
        }

         private void expand_action (TreeNodeMouseClickEventArgs e)
        { 
            if (e.Node.Parent == null | e.Button == MouseButtons.Right )
                    return;
 

            if (  (e.Node.Tag.ToString().ToUpper().Contains("CLOUD")) & (e.Node.Tag.ToString().Contains("$")))
                {
                    wp01RightTreeCloud.getCloudTablesAndViews(e);
                    return;
                }
            
                if ( (e.Node.Tag.ToString().Contains("ODBC$")))
                {
                    wp02RightTreeTables.getTablesAndViews(e);
                    sqlBuild.setLblConnectionName(e.Node.Text, "ODBC");
                    return;
                }

                if ( (e.Node.Tag.ToString().ToUpper().Contains("CSV")) & (e.Node.Tag.ToString().Contains("$")) & (e.Node.Parent.Text.ToString().ToUpper().Contains("CSV")))
                {
                    wp01RightTreeCloud.getCsvFilesList(e);
                    sqlBuild.setLblConnectionName(e.Node.Text, "CSV");
                    return;
                }

                if (  (e.Node.Tag.ToString().ToUpper().Contains("CSV")) & (e.Node.Tag.ToString().Contains("$")) & (e.Node.Parent.Text.ToString().ToUpper().Contains("CSV")))
                {
                    wp01RightTreeCloud.getCsvFilesList(e);
                    sqlBuild.setLblConnectionName(e.Node.Text, "CSV");
                    return;
                }

                if ( (e.Node.Tag.ToString().ToUpper().Contains("$FILE_CSV$")))
                {
                    wp01RightTreeCloud.getCsvHeaders(e);
                    return;
                }

                if ((e.Node.Tag.ToString().Contains("VIEW") | e.Node.Tag.ToString().Contains("TABLE")))
                {
                    if (e.Button == MouseButtons.Left | e.Node.Tag.ToString().Contains('$'))
                    {
                        if (e.Node.Tag.ToString().Contains("CLD") | e.Node.Parent.Parent.Tag.ToString().Contains("Cloud"))
                        {
                            wp01RightTreeCloud.getColumnsAndIndexes(e);
                            return;
                        }                       
                        else
                        {
                            wp02RightTreeTables.getColumnsandIndexes(e);
                            return;

                        }

                    }
                }           
        }

         private void treeODBC_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                miSelectNode = treeODBC.GetNodeAt(e.X, e.Y);

                if (e.Node.Tag != null)
                {
                    if (e.Button == MouseButtons.Left)
                        expand_action(e);
                    else 
                        contextMenu(e);                               
                }
            }
            catch (Exception er)
            {
                sTool.ExpHandler(er, "treeODBC_NodeMouseClick  ");
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
                sTool.ExpHandler(er, "treeODBC_AfterSelect");
            }
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
            }
            catch (Exception er)
            {
                sTool.ExpHandler(er, "treeODBC_AfterSelect");
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
