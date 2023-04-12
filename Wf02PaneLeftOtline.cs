using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlEngine
{
    public partial class Wf02PaneLeftOtline : UserControl
    {
        private TreeNode miSelectNode;

        public Wf02PaneLeftOtline()
        {
            InitializeComponent();
            PopulateExcelTreeView();

            treeExcelOtl.NodeMouseClick += (treeExcelOtl_MouseClick);
            treeExcelOtl.KeyPress += (treeExcelOtl_KeyPress);            
        }

        private void PopulateExcelTreeView( )
        {
            try
            {  
                var rootTable = new TreeNode("Excel", 4, 4)
                {
                    Tag = "excel"
                };
                var vNodeExcelSheet = new TreeNode(" ", 99, 99);
                rootTable.Nodes.Add(vNodeExcelSheet);
                treeExcelOtl.Nodes.Add(rootTable);

                var rootTask = new TreeNode("Tasks", 3, 3)
                {
                    Tag = "task"
                };
                var vNodeExcelTask = new TreeNode(" ", 99, 99);
                rootTask.Nodes.Add(vNodeExcelTask);
                treeExcelOtl.Nodes.Add(rootTask);
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "PopulateOdbcTreeView");
            }
        }
 
        private static void RefreshExcel(TreeNode nodeToAddTo)
        {
            // intSqlVBAEngine.createExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);
            nodeToAddTo.Nodes.Clear();
           

            for (int  bookId = 1; bookId <= SqlEngine.CurrExcelApp.Workbooks.Count; bookId++ )
            {
                SqlEngine.CurrExcelApp.Workbooks.Item[bookId].Activate();

                var vBookName = SqlEngine.CurrExcelApp.Workbooks.Item[bookId].Name;
                var vNodeExcelBook = new TreeNode(vBookName, 0, 0)
                {
                    Tag = vBookName + "| ExBook"
                };
                nodeToAddTo.Nodes.Add(vNodeExcelBook);

                foreach (var currSheet in SqlEngine.CurrExcelApp.ActiveSheet.Parent.Worksheets )// SqlEngine.currExcelApp.ActiveSheet.Parent.Worksheets)
                {
                    var vNodeExcelSheet = new TreeNode(currSheet.Name, 1, 1)
                    {
                        Tag = vBookName + "." + currSheet.Name + "| ExList"
                    };
                    vNodeExcelBook.Nodes.Add(vNodeExcelSheet);
                    if (currSheet.ListObjects == null) continue;
                    foreach (var vObj in currSheet.ListObjects )
                    {
                        var vNodeExcelObject = new TreeNode(vObj.name, 2, 2)
                        {
                            Tag = vObj.name + "| ExTable"
                        };
                        vNodeExcelSheet.Nodes.Add(vNodeExcelObject);
                    }
                }
            }
        }

        private static void RefreshExSheet(TreeNode nodeToAddTo)
        {
            // intSqlVBAEngine.createExTable(miSelectNode.Parent.Parent.Text, miSelectNode.Text);
            nodeToAddTo.Nodes.Clear();
            
            var sheet = SqlEngine.CurrExcelApp.ActiveSheet.Parent.Worksheets(nodeToAddTo.Text);
            
                foreach (var vObj in sheet.ListObjects)
                {
                    var vNodeExcelObject = new TreeNode(vObj.name, 2, 2)
                    {
                        Tag = vObj.name + "|  ExTable "
                    };
                    nodeToAddTo.Nodes.Add(vNodeExcelObject);
                }
            
        }

        private static ContextMenuStrip CreateMenu(TreeNodeMouseClickEventArgs e, IEnumerable<string> vMenu, EventHandler myFuncName, ContextMenuStrip vCurrMenu)
        {
            if (e.Node.ContextMenuStrip != null) return vCurrMenu;
            
            if (vCurrMenu.Items.Count < 1)
            {
                vCurrMenu.Items.Clear();

                foreach (var rw in vMenu)
                {
                    var vMenuRun = new ToolStripMenuItem(rw);
                    vMenuRun.Click += myFuncName;
                    vCurrMenu.Items.Add(vMenuRun);
                }
            }
            e.Node.ContextMenuStrip = vCurrMenu;
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
                            contextMenuExcelRoot = CreateMenu(
                                                      e
                                                  , new[] { "Refresh" /*, "Sort", "Create Outline", "Create Task" */ }
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

                                if (vCurrBook.Name.Contains(e.Node.Text))
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
                            contextMenuExSheet = CreateMenu(
                                               e
                                           , new[] { "Refresh"/*, "Copy", "Rename",  "Delete"*/ }
                                           , ExcelActions_Click
                                           , contextMenuExSheet);
                        }
                        else
                        {
                            for ( int i=1; i<= SqlEngine.CurrExcelApp.ActiveWorkbook.Sheets.Count; i++    )
                            {
                                var vCurrSheet = SqlEngine.CurrExcelApp.ActiveWorkbook.Sheets[i];

                                if (!vCurrSheet.Name.ToString().Contains(e.Node.Text)) continue;
                                
                                vCurrSheet.Activate();
                                return;
                            }
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
