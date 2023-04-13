using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SqlEngine
{
    public partial class Wf03PanelRightSqlM : UserControl
    {
        private TreeNode miSelectNode;

        public static Wf03PanelRightSqlM CurrSqlPanel;


   /*     private static void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // doneInitSQlObjects = true;
        }
*/
         public void ShowOdbcTree() 
        {
            try
            { 
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Hide(); 
                ODBCtabControl.Dock  = DockStyle.Fill;                
            }
            catch
            {
                // ignored
            }
        }

        public void ShowSqlEdit()
        { 
            try
            {
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel2.Show();                
                splitContainer1.SplitterDistance = 205;               
             //   this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
                splitContainer1.FixedPanel = FixedPanel.Panel1;
            }
            catch
            {
                // ignored
            }
        } 

        private   void InitParam ()
        {
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Hide();
            splitContainer1.AutoSize = true;
            ODBCtabControl.Dock = DockStyle.Fill;
            treeODBC.Dock = DockStyle.Fill;  
            treeODBC.NodeMouseClick += treeODBC_NodeMouseClick; 
            treeODBC.KeyPress += in2SqlRightPane_KeyPress; 
            treeODBC.AllowDrop = true;

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

        public static TreeNode GetNode ( int x, int y )

        {
            return
                  CurrSqlPanel.treeODBC.GetNodeAt(x, y); 
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("N1");
            tabControl1.TabPages[tabControl1.TabCount - 1].Text = @"S0" + tabControl1.TabCount;

            var sqlTab = new Wf05SqlEngine();
            sqlTab.TopLevel = false;
            sqlTab.FormBorderStyle = FormBorderStyle.None;
            sqlTab.Parent = tabControl1.TabPages[tabControl1.TabCount - 1];
            sqlTab.Dock = DockStyle.Fill;
            sqlTab.Show();
        }

        private Wf06SqlBuilder sqlBuild  ;

        private void SqlBuild ()
        {
            sqlBuild = new Wf06SqlBuilder();
            sqlBuild.TopLevel = false;
            sqlBuild.FormBorderStyle = FormBorderStyle.None;
            sqlBuild.Parent = ODBCtabControl.TabPages[1];
            sqlBuild.Dock = DockStyle.Fill;
            sqlBuild.Show();
               
        }

        private Wf07OdbcProperties odbcEdit;

/*        private void OdbcEditPanel ()
        {
            odbcEdit = new Wf07OdbcProperties();
           // OdbcEdit.TopLevel = false;
           // OdbcEdit.FormBorderStyle = FormBorderStyle.None;
            odbcEdit.Parent = ODBCtabControl.TabPages[2];
            odbcEdit.Dock = DockStyle.Fill;
            odbcEdit.Show();
        }
  */      
        public Wf03PanelRightSqlM()
        {
            try
            {
                InitializeComponent();
                PopulateOdbcTreeView();
                InitParam();
                newToolStripButton_Click(null, null);

                SqlBuild();
              //  OdbcEditPanel();
                CurrSqlPanel = this;

            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "in2SqlRightPane");
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

        private void PopulateOdbcTreeView(int isUi = 0)
        {
            try
            {
                var rootNode = new TreeNode("ODBC")
                {
                    Tag = "root"
                };
                GetODbcRecords(rootNode, isUi);
                treeODBC.Nodes.Add(rootNode); 

                var rootCloud = new TreeNode("Сloud", 18, 18)
                {
                    Tag = "cloud"
                };

                var tnClickHouse = new TreeNode("ClickHouse" , 19, 19)
                {
                    Tag = "ClickHouse"
                };
                GetCloudRecords(tnClickHouse, "CloudCH");
                rootCloud.Nodes.Add(tnClickHouse);

                treeODBC.Nodes.Add(rootCloud);                

                var rootCsv = new TreeNode("csv", 20, 20)
                {
                    Tag = "CSV"
                };
                GetCsvRecords(rootCsv);
                treeODBC.Nodes.Add(rootCsv);
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "PopulateOdbcTreeView");
            }
        }


        private static void GetCloudRecords(TreeNode nodeToAddTo, string vCloudType)
        {
            try
            {
                SCloud.CloudPropertiesList = SCloud.CloudList();

                foreach (var vCurrCloudList in SCloud.CloudPropertiesList.Where(vCurrCloudList => vCurrCloudList.CloudType.Contains(vCloudType)))
                {
                    Wp02RightTreeTables.SetOdbcTreeLineSimple(nodeToAddTo, vCurrCloudList.CloudName, vCloudType + '$');
                }
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "GetCloudRecords");
            }
        } 

        private static void GetCsvRecords(TreeNode nodeToAddTo )
        {
            try
            {
                SCsv.FolderPropertiesList = SCsv.FolderList();

                foreach (var vCurrFolder in SCsv.FolderPropertiesList)
                {
                      Wp02RightTreeTables.SetCsvTreeLineSimple(nodeToAddTo, vCurrFolder.FolderName,   "CSV$");
                }
                return;
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "GetCloudRecords");
            }
        }


        private static void GetODbcRecords(TreeNode nodeToAddTo, int isUi = 0)
        {
            try
            {

                /*       foreach (var vv in SCloud.CloudPropertiesList.Select(vCurrCloudList => vCurrCloudList.CloudName))
                       {
                       }
                  */     

                switch (isUi)
                {
                    case 0:
                    {
                        foreach (var odbcProperties in SOdbc.OdbcPropertiesList)
                        {
                            Wp02RightTreeTables.SetOdbcTreeLineSimple(nodeToAddTo, odbcProperties.OdbcName);
                        }
                        return;
                    }
                    case 1:
                    {
                        foreach (var odbcProperties in SOdbc.OdbcPropertiesList)
                        {
                            SOdbc.CheckOdbcStatus(odbcProperties.OdbcName);
                            Wp02RightTreeTables.SetOdbcTreeLineComplex(nodeToAddTo, odbcProperties.OdbcName, odbcProperties.OdbcName);
                        }
                        return;
                    }
                }
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "GetODbcRecords");
            }
        } 
      
        private static ContextMenuStrip CreateMenu(TreeNodeMouseClickEventArgs e, IEnumerable<string> vMenu, EventHandler myFuncName, ContextMenuStrip vCMenu)
        {          
            if (vCMenu == null)
            {
                var vCurrMenu = new ContextMenuStrip();
                foreach (var rw in vMenu)
                {
                    var vMenuRun = new ToolStripMenuItem(rw);
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
               STool.RunCmdLauncher("odbcad32");

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
                var frm1 = new Wf01Login(miSelectNode.Text);//.Sho
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
                var frmCsvEdit = new Wf10CsvEdit();
                frmCsvEdit.Show();
            }
           
            else if (sender.ToString().Contains("Refresh"))
            {
                miSelectNode.Nodes.Clear();
                GetCsvRecords(miSelectNode);
            } 
        }

        private void clickHouse_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Edit"))
            {
                var vConnName = miSelectNode.Tag + "." + miSelectNode.Text;
                vConnName = vConnName.Replace("#","");
                vConnName = vConnName.Replace("$", "");

                var cloudCloudChe =  new Wf09CloudCloudConnectionEditor(vConnName);
                cloudCloudChe.Show();  
            }
            else if(sender.ToString().Contains("Create"))
            {
                var cloudCloudChc = new Wf09CloudCloudConnectionEditor();
                cloudCloudChc.Show(); 

            }
            else if (sender.ToString().Contains("Refresh"))
            {
                miSelectNode.Nodes.Clear();
                GetCloudRecords(miSelectNode, "CloudCH");
            }

            else if (sender.ToString().Contains("Delete"))
            {
                SRegistry.DelLocalValue(miSelectNode.Tag + "." + miSelectNode.Text);

                miSelectNode.Text = "";
                miSelectNode.Tag = "";
                miSelectNode.Nodes.Clear();
                miSelectNode.ImageIndex = 990;  
            }

        }  

        private TreeNode fTn = null;

        private void  FindTreeNode(TreeNode treeNode , string nodeName )
        { 

            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (fTn == null)
                    if (tn.Text.Equals(nodeName))
                    {
                        fTn = tn;
                    }
                    else
                        FindTreeNode(tn, nodeName);
                else
                    break;
            } 
        }


        public   TreeNode FindeTable( string nodeName , string odbcName )
        { 
            fTn = null;
            foreach (TreeNode tn in treeODBC.Nodes) {             
                 foreach (TreeNode tn1 in tn.Nodes )
                 { FindTreeNode(tn1, nodeName);                        
                     if ((fTn == null) == false)
                         break;
                 }                                   
            } 
            return fTn; 
        }

        //


        private void SQLiteFolder_Click(object sender, EventArgs e)
        {  //{ "Create SQLiteDatabase", "Refresh" }

            if (sender.ToString().Contains("Create SQLiteDatabase"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Refresh"))
                miSelectNode.Nodes.Clear();

            else 
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

/*        private void ExTableMenu_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Rename"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Edit Sql"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Refresh"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Properties"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (sender.ToString().Contains("Delete"))
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
        } 
 
*/
        private void RegularObjectMenuClick(object sender, EventArgs e)
        {

            if (sender.ToString().Contains("PivotTable"))
                IntSqlVbaEngine.CreatePivotTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);

            else if (sender.ToString().Contains("Table"))
                if (miSelectNode.Parent.Parent.Tag.ToString().Contains("Cloud"))
                    SVbaEngineCloud.CreateExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text,null);             

                else
                     IntSqlVbaEngine.CreateExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);
            /* fix me */
            else if (sender.ToString().Contains("generate CSV"))
                if (miSelectNode.Parent.Parent.Tag.ToString().ToUpper().Contains("ODBC"))
                    SOdbc.DumpOdbctoCsv(
                                     miSelectNode.Parent.Parent.Text
                                   , "select * from  " + miSelectNode.Text
                                   , SCsv.GetFirstFolder() + miSelectNode.Text + ".csv");                 

                else if (sender.ToString().Contains("Chart"))
                    MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (sender.ToString().Contains("Editor"))
                    MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (sender.ToString().Contains("Properties"))
                    MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private new void ContextMenu(TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return;

            if (e.Node.ContextMenuStrip != null) return;
            
            if ((e.Node.Tag.ToString().Contains("clm")))
            { 
                return;
            }

            var vNodeTag = e.Node.Tag.ToString().ToUpper();

            if (vNodeTag.Contains("FLD")  & vNodeTag.Contains("SQLITE") ) 
            {
                contextMenuSQLiteFolder = CreateMenu(
                    e
                    , new[] { "Create SQLiteDatabase", "Refresh" }
                    , SQLiteFolder_Click
                    , contextMenuSQLiteFolder);
                return;
            }

            if ((vNodeTag.Contains("VIEW") | vNodeTag.Contains("TABLE")))
                if (miSelectNode.Parent.Parent.Tag.ToString().ToUpper().Contains("SQLITE"))
                { 
                    contextMenuTableSQLite = CreateMenu(
                        e
                        , new[] { "to Table", "generate CSV" }
                        , RegularObjectMenuClick
                        , this.contextMenuTableSQLite);
                    return;                        
                }
                else
                {                         
                    contextMenuTable = CreateMenu(
                        e
                        , new[] { "to Table", "to PivotTable", "generate CSV" }
                        , RegularObjectMenuClick
                        , this.contextMenuTable);
                    return;                        
                }

            if ((vNodeTag.Contains("ROOT")))
            {                     
                contextMenuRootODBC = CreateMenu(
                    e
                    , new[] { "Edit", "Refresh" }
                    , rootMenu_Click
                    , contextMenuRootODBC);                    
                return;
            }

            if ((vNodeTag.Contains("CLOUD")) & (vNodeTag.Contains("CH")))
            {
                     
                contextMenuEditCH = CreateMenu(
                    e
                    , new[] { "Edit", "Delete" }
                    , clickHouse_Click
                    , contextMenuEditCH);
                return;
                    
            }

            if ((vNodeTag.Contains("CSV")) & (vNodeTag.Contains("CSV")))
            {                    
                contextMenuCSV = CreateMenu(
                    e
                    , new[] { "Edit", "Refresh" }
                    , clickCSV_Click
                    , contextMenuCSV);
                return;                    
            }

            if ((vNodeTag.Contains("CLICKHOUSE")))
            {                    
                contextMenuCHRoot = CreateMenu(
                    e
                    , new[] { "Create", "Refresh" }
                    , clickHouse_Click
                    , contextMenuCHRoot);
                return;                    
            }

            if ((!vNodeTag.Contains("ODBC%"))) return;
            
            contextMenuOdbcError = CreateMenu(
                e
                , new[] { "ReConnect", "Edit", "Login" }
                , rootMenu_Click
                , contextMenuOdbcError);
        }

         private void expand_action (TreeNodeMouseClickEventArgs e)
        { 
            if (e.Node.Parent == null | e.Button == MouseButtons.Right )
                    return;
 

            if (  e.Node.Tag.ToString().ToUpper().Contains("CLOUD") & e.Node.Tag.ToString().Contains("$"))
            {
                Wp01RightTreeCloud.GetCloudTablesAndViews(e);
                return;
            }
            
            if ( e.Node.Tag.ToString().Contains("ODBC$"))
            {
                Wp02RightTreeTables.GetTablesAndViews(e);
                sqlBuild.SetLblConnectionName(e.Node.Text);
                return;
            }

            if ( e.Node.Tag.ToString().ToUpper().Contains("CSV") & e.Node.Tag.ToString().Contains("$") & (e.Node.Parent.Text.ToString().ToUpper().Contains("CSV")))
            {
                Wp01RightTreeCloud.GetCsvFilesList(e);
                sqlBuild.SetLblConnectionName(e.Node.Text, "CSV");
                return;
            }

            if (  e.Node.Tag.ToString().ToUpper().Contains("CSV") & (e.Node.Tag.ToString().Contains("$")) & e.Node.Parent.Text.ToString().ToUpper().Contains("CSV"))
            {
                Wp01RightTreeCloud.GetCsvFilesList(e);
                sqlBuild.SetLblConnectionName(e.Node.Text, "CSV");
                return;
            }

            if ( e.Node.Tag.ToString().ToUpper().Contains("$FILE_CSV$"))
            {
                Wp01RightTreeCloud.GetCsvHeaders(e);
                return;
            }

            if (!(e.Node.Tag.ToString().Contains("VIEW") | e.Node.Tag.ToString().Contains("TABLE"))) return;

            if (!(e.Button == MouseButtons.Left | e.Node.Tag.ToString().Contains('$'))) return;
            
            if (e.Node.Tag.ToString().Contains("CLD") | e.Node.Parent.Parent.Tag.ToString().Contains("Cloud"))
            {
                Wp01RightTreeCloud.GetColumnsAndIndexes(e);
                return;
            }                       
            else
            {
                Wp02RightTreeTables.GetColumnsandIndexes(e);
                return;

            }
        }

         private void treeODBC_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                miSelectNode = treeODBC.GetNodeAt(e.X, e.Y);

                if (e.Node.Tag == null) return;
                
                if (e.Button == MouseButtons.Left)
                    expand_action(e);
                else 
                    ContextMenu(e);
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "treeODBC_NodeMouseClick  ");
            }

        }

 /*       private void treeODBC_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                  string selectedNodeText = e.Node.Text;
                  DialogResult  result = MessageBox.Show(selectedNodeText + "  1");
                  
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "treeODBC_AfterSelect");
            }
        }
*/

  /*      private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "treeODBC_AfterSelect");
            }
        }

*/

        private void in2SqlRightPane_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\u0003') return; //Control+C
            
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
