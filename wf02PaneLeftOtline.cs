using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlEngine
{
    public partial class wf02PaneLeftOtline : UserControl
    {
        private TreeNode miSelectNode;

        public wf02PaneLeftOtline()
        {
            InitializeComponent();
            PopulateExcelTreeView();

            this.treeExcelOtl.NodeMouseClick +=
                    new TreeNodeMouseClickEventHandler(this.treeExcelOtl_MouseClick);


            this.treeExcelOtl.KeyPress +=
                new KeyPressEventHandler(this.treeExcelOtl_KeyPress);            
        }

        public void PopulateExcelTreeView( )
        {
            try
            {  
                TreeNode rootTable = new TreeNode("Excel".ToString(), 4, 4);
                rootTable.Tag = "excel";                
                TreeNode vNodeExcelSheet = new TreeNode(" ".ToString(), 99, 99);
                rootTable.Nodes.Add(vNodeExcelSheet);
                treeExcelOtl.Nodes.Add(rootTable);

                TreeNode rootTask = new TreeNode("Tasks".ToString(), 3, 3);
                rootTask.Tag = "task";
                TreeNode vNodeExcelTask = new TreeNode(" ".ToString(), 99, 99);
                rootTask.Nodes.Add(vNodeExcelTask);
                treeExcelOtl.Nodes.Add(rootTask);
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "PopulateOdbcTreeView");
            }
        }




        private void RefreshExcel(TreeNode nodeToAddTo)
        {
            // intSqlVBAEngine.createExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);
            nodeToAddTo.Nodes.Clear();
           

            for (int  vBookID = 1; vBookID <= SqlEngine.CurrExcelApp.Workbooks.Count; vBookID++ )
            {
                SqlEngine.CurrExcelApp.Workbooks.Item[vBookID].Activate();

                String vBookName = SqlEngine.CurrExcelApp.Workbooks.Item[vBookID].Name;
                TreeNode vNodeExcelBook = new TreeNode(vBookName, 0, 0);
                vNodeExcelBook.Tag = vBookName + "| ExBook";
                nodeToAddTo.Nodes.Add(vNodeExcelBook);

                foreach (var vCurrSheet in SqlEngine.CurrExcelApp.ActiveSheet.Parent.Worksheets )// SqlEngine.currExcelApp.ActiveSheet.Parent.Worksheets)
                {
                    
                    TreeNode vNodeExcelSheet = new TreeNode(vCurrSheet.Name, 1, 1);
                    vNodeExcelSheet.Tag = vBookName + "." + vCurrSheet.Name + "| ExList";
                    vNodeExcelBook.Nodes.Add(vNodeExcelSheet);                    
                    if (vCurrSheet.ListObjects != null )
                    foreach (var vObj in vCurrSheet.ListObjects )
                    {
                        TreeNode vNodeExcelObject = new TreeNode(vObj.name, 2, 2);
                        vNodeExcelObject.Tag =  vObj.name + "| ExTable";
                        vNodeExcelSheet.Nodes.Add(vNodeExcelObject);
                    }
                }
            }
        }

        private void RefreshExSheet(TreeNode nodeToAddTo)
        {
            // intSqlVBAEngine.createExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);
            nodeToAddTo.Nodes.Clear();

           
            var sheet = SqlEngine.CurrExcelApp.ActiveSheet.Parent.Worksheets(nodeToAddTo.Text); 
            
                foreach (var vObj in sheet.ListObjects)
                {
                    TreeNode vNodeExcelObject = new TreeNode(vObj.name, 2, 2);
                    vNodeExcelObject.Tag =  vObj.name + "|  ExTable ";
                    nodeToAddTo.Nodes.Add(vNodeExcelObject);
                }
            
        }

        private ContextMenuStrip createMenu(TreeNodeMouseClickEventArgs e, String[] vMenu, EventHandler myFuncName, ContextMenuStrip vCurrMenu)
        {
            if (e.Node.ContextMenuStrip == null)
            {
                if (vCurrMenu.Items.Count < 1)
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

        private void rootOutline_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Refresh"))
                RefreshExcel(miSelectNode);             
        }

        private void ExcelActions_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("Refresh"))
                RefreshExSheet(miSelectNode);  
        }
 
        private void treeExcelOtl_MouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                miSelectNode = treeExcelOtl.GetNodeAt(e.X, e.Y);

                if (e.Node.Tag != null)
                {
                    if (e.Node.Tag.ToString().Contains("excel"))
                    {
                        if (e.Button == MouseButtons.Right)
                        {  
                            contextMenuExcelRoot = createMenu(
                                                      e
                                                  , new String[] { "Refresh" /*, "Sort", "Create Outline", "Create Task" */ }
                                                  , rootOutline_Click
                                                  , contextMenuExcelRoot);
                            return; 
                        }
                        else
                        {
                            RefreshExcel(e.Node);
                            return;
                        }
                    }

                    if (e.Node.Tag.ToString().Contains("ExBook"))
                    {
                        if (e.Button == MouseButtons.Left)
                        { 
                            for (int i = 1; i <=  SqlEngine.CurrExcelApp.Workbooks.Count; i++)
                            {
                                var vCurrBook = SqlEngine.CurrExcelApp.Workbooks[i];

                                if (vCurrBook.Name.ToString().Contains(e.Node.Text))
                                {
                                    vCurrBook.Activate();
                                    return;
                                }
                            }
                            return;
                        }
                         
                    }

                    if (e.Node.Tag.ToString().Contains("ExList"))
                    {
                        if (e.Button == MouseButtons.Right)
                        { 
                            contextMenuExSheet = createMenu(
                                               e
                                           , new String[] { "Refresh"/*, "Copy", "Rename",  "Delete"*/ }
                                           , ExcelActions_Click
                                           , contextMenuExSheet);
                            return; 
                        }
                        else
                        {
                            for ( int i=1; i<= SqlEngine.CurrExcelApp.ActiveWorkbook.Sheets.Count; i++    )
                            {
                                var vCurrSheet = SqlEngine.CurrExcelApp.ActiveWorkbook.Sheets[i];

                                if ( vCurrSheet.Name.ToString().Contains(e.Node.Text) )
                                {
                                    vCurrSheet.Activate();
                                    return;
                                }
                            }
                            return;
                        }
                    }
                }
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "treeExcelOtl_MouseClick 2 ");
            }

        }

        private void treeExcelOtl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\u0003')  //Control+C
                if (treeExcelOtl.SelectedNode != null)
                {
                    Clipboard.SetText(treeExcelOtl.SelectedNode.Text);
                }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
