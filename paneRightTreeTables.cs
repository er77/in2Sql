using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SqlEngine.svcCloud;

namespace SqlEngine
{
    class paneRightTreeTables
    {
        private static void initSQlObjects(ref svcODBC.OdbcProperties vCurrOdbc)
        {
            try
            {
                if (vCurrOdbc.Tables == null)
                {
                    vCurrOdbc.Tables = new List<svcODBC.SqlObjects>();
                }

                if (vCurrOdbc.Views == null)
                {
                    vCurrOdbc.Views = new List<svcODBC.SqlObjects>();
                }

                if (vCurrOdbc.SQLProgramms == null)
                {
                    vCurrOdbc.SQLProgramms = new List<svcODBC.SqlObjects>();
                }

                if (vCurrOdbc.SQLFunctions == null)
                {
                    vCurrOdbc.SQLFunctions = new List<svcODBC.SqlObjects>();
                }

                if (vCurrOdbc.Tables.Count == 0)
                {
                    vCurrOdbc.Tables.AddRange(svcODBC.getTableList(vCurrOdbc.OdbcName));
                }

                if (vCurrOdbc.Views.Count == 0)
                {
                    vCurrOdbc.Views.AddRange(svcODBC.getViewList(vCurrOdbc.OdbcName));
                }

                if (vCurrOdbc.SQLProgramms.Count == 0)
                {
                    vCurrOdbc.SQLProgramms.AddRange(svcODBC.getSQLProgrammsList(vCurrOdbc.OdbcName));
                }

                if (vCurrOdbc.SQLFunctions.Count == 0)
                {
                    vCurrOdbc.SQLFunctions.AddRange(svcODBC.getSQLFunctionsList(vCurrOdbc.OdbcName));
                }

            }
            catch (Exception er)
            {
                svcTool.ExpHandler(er, "initSQlObjects");
            }
        } 

        public static void setODBCTreeLineSimple(TreeNode nodeToAddTo, string vOdbcName, string vOdbcType= "ODBC$")
        {
            TreeNode vNodeDatabase = new TreeNode(vOdbcName, 1, 1);

            nodeToAddTo.Nodes.Add(vNodeDatabase);
            vNodeDatabase.Tag = vOdbcType;
            TreeNode vNodeTable = new TreeNode(" ".ToString(), 100, 100); // vNodeTable.Tag = vCurrTable.Name;
            vNodeDatabase.Nodes.Add(vNodeTable);
        }

        public static void setCSVTreeLineSimple(TreeNode nodeToAddTo, string vOdbcName, string vOdbcType = "CSV$")
        {
            TreeNode vNodeDatabase = new TreeNode(vOdbcName, 21, 21);

            nodeToAddTo.Nodes.Add(vNodeDatabase);
            vNodeDatabase.Tag = vOdbcType;
            TreeNode vNodeTable = new TreeNode(" ".ToString(), 100, 100); // vNodeTable.Tag = vCurrTable.Name;
            vNodeDatabase.Nodes.Add(vNodeTable);
        }


        public static void setSQLiteTreeLineSimple(TreeNode nodeToAddTo, string vOdbcName, string vOdbcType = "SQLITE$")
        {
            TreeNode vNodeDatabase = new TreeNode(vOdbcName, 8, 8);

            nodeToAddTo.Nodes.Add(vNodeDatabase);
            vNodeDatabase.Tag = "FLD|" + vOdbcType;
            TreeNode vNodeTable = new TreeNode(" ".ToString(), 100, 100); // vNodeTable.Tag = vCurrTable.Name;
            vNodeDatabase.Nodes.Add(vNodeTable);
        }

        public static void setODBCTreeLineComplex(TreeNode nodeToAddTo, string vCurrvListOdbcName, string VCurrOdbcName)
        {
            try
            {
                var vCurrODBC = svcODBC.vODBCList.Find(item => item.OdbcName == vCurrvListOdbcName);

                if ((vCurrODBC.ConnStatus == 0))
                {
                    setODBCTreeLineSimple(nodeToAddTo, vCurrvListOdbcName);
                    return;
                }


                if (vCurrODBC.ConnStatus < 0)
                {
                    TreeNode vNodeDatabase = new TreeNode(vCurrODBC.OdbcName, 7, 7);
                    nodeToAddTo.Nodes.Add(vNodeDatabase);
                    vNodeDatabase.Tag = "ODBC%";
                    TreeNode vNodeTable = new TreeNode(vCurrODBC.ConnErrMsg, 99, 99);
                    vNodeDatabase.Nodes.Add(vNodeTable);

                    return;
                }

                if ((vCurrODBC.ConnStatus == 1) & vCurrvListOdbcName.Contains(VCurrOdbcName) & vCurrvListOdbcName.Length == VCurrOdbcName.Length)
                {
                    initSQlObjects(ref vCurrODBC);

                }

                if (vCurrODBC.ConnStatus == 1 & (vCurrODBC.Tables.Count == 0 & vCurrODBC.Views.Count == 0))
                {
                    TreeNode vNodeDatabase = new TreeNode(vCurrODBC.OdbcName, 2, 2);

                    nodeToAddTo.Nodes.Add(vNodeDatabase);
                    vNodeDatabase.Tag = "ODBC#";

                    return;
                }

                if (vCurrODBC.ConnStatus == 1 & (vCurrODBC.Tables.Count > 0 | vCurrODBC.Views.Count > 0))
                {
                    TreeNode vNodeDatabase = new TreeNode(vCurrODBC.OdbcName, 2, 2);

                    nodeToAddTo.Nodes.Add(vNodeDatabase);
                    vNodeDatabase.Tag = "ODBC#";

                    if (vCurrODBC.Tables.Count > 0)
                    {
                        TreeNode vNodeTableFolder = new TreeNode("Tables".ToString(), 3, 3);
                        vNodeTableFolder.Tag = vCurrODBC.OdbcName + "tf";
                        vNodeDatabase.Nodes.Add(vNodeTableFolder);

                        foreach (var vCurrTable in vCurrODBC.Tables)
                        {
                            TreeNode vNodeTable = new TreeNode(vCurrTable.Name, 4, 4); // vNodeTable.Tag = vCurrTable.Name;
                            vNodeTableFolder.Nodes.Add(vNodeTable);
                        }
                    }

                    if (vCurrODBC.Views.Count > 0)
                    {
                        TreeNode vNodeViewFolder = new TreeNode("Views".ToString(), 5, 5);
                        vNodeViewFolder.Tag = vCurrODBC.OdbcName + "vf";
                        vNodeDatabase.Nodes.Add(vNodeViewFolder);

                        foreach (var vCurrView in vCurrODBC.Views)
                        {
                            TreeNode vNodeView = new TreeNode(vCurrView.Name, 6, 6); // vNodeTable.Tag = vCurrTable.Name;
                            vNodeViewFolder.Nodes.Add(vNodeView);
                        }
                    }
                }
            }
            catch (Exception er)
            {
                svcTool.ExpHandler(er, "setODBCTreeLineComplex");
            }
        }
         

        public static void getTablesAndViews(TreeNodeMouseClickEventArgs e)
        {
            e.Node.Nodes.Clear();
            string vCurrOdbcName = e.Node.Text;
            svcODBC.checkOdbcStatus(vCurrOdbcName);

            var vCurrODBC = svcODBC.vODBCList.Find(item => item.OdbcName == vCurrOdbcName);

            try
            {
                if ((vCurrODBC.ConnStatus == 0))
                {
                    return;
                }

                if (vCurrODBC.ConnStatus < 0)
                {
                    e.Node.ImageIndex = 7;
                    e.Node.SelectedImageIndex = 7;
                    e.Node.Tag = "ODBC%";
                    TreeNode vNodeErrRecord = new TreeNode(vCurrODBC.ConnErrMsg, 99, 99);
                    e.Node.Nodes.Add(vNodeErrRecord);
                    return;
                }

                if (vCurrODBC.ConnStatus == 1)
                {
                    e.Node.ImageIndex = 2;
                    e.Node.SelectedImageIndex = 2;
                    e.Node.Tag = "ODBC#";
                    TreeNode vNodeTableFolder = new TreeNode("Tables".ToString(), 3, 3);
                    vNodeTableFolder.Tag = vCurrODBC.OdbcName + "_tf";
                    e.Node.Nodes.Add(vNodeTableFolder);

                    initSQlObjects(ref vCurrODBC);

                    foreach (var vCurrTable in vCurrODBC.Tables)
                    {
                        TreeNode vNodeTable = new TreeNode(vCurrTable.Name, 4, 4);
                        vNodeTable.Tag = vCurrODBC.OdbcName + "|" + vCurrTable.Name + "|$TABLE$";
                        vNodeTableFolder.Nodes.Add(vNodeTable);
                        TreeNode vNodeColumnTbl = new TreeNode(" ".ToString(), 99, 99);
                        vNodeColumnTbl.Tag = vCurrODBC.OdbcName + "." + vCurrTable.Name;
                        vNodeTable.Nodes.Add(vNodeColumnTbl);
                    }

                    TreeNode vNodeViewFolder = new TreeNode("Views".ToString(), 5, 5);
                    vNodeViewFolder.Tag = vCurrODBC.OdbcName + "_vf";
                    e.Node.Nodes.Add(vNodeViewFolder);

                    foreach (var vCurrView in vCurrODBC.Views)
                    {
                        TreeNode vNodeView = new TreeNode(vCurrView.Name, 6, 6);
                        vNodeView.Tag = vCurrODBC.OdbcName + "." + vNodeView.Name + "|$VIEW$";
                        vNodeViewFolder.Nodes.Add(vNodeView);
                        TreeNode vNodeColumnVw = new TreeNode(" ".ToString(), 99, 99);
                        vNodeColumnVw.Tag = vCurrODBC.OdbcName + "." + vNodeView.Name;
                        vNodeView.Nodes.Add(vNodeColumnVw);
                    }

                    TreeNode vNodeFunctionFolder = new TreeNode("Functions".ToString(), 10, 10);
                    vNodeFunctionFolder.Tag = vCurrODBC.OdbcName + "_fn";
                    e.Node.Nodes.Add(vNodeFunctionFolder);

                    foreach (var vCurrFunc in vCurrODBC.SQLFunctions)
                    {
                        TreeNode vNodeView = new TreeNode(vCurrFunc.Name, 9, 9);
                        vNodeView.Tag = vCurrODBC.OdbcName + "." + vCurrFunc.Name;
                        vNodeFunctionFolder.Nodes.Add(vNodeView);
                    }

                    TreeNode vNodeExecFolder = new TreeNode("Procedures".ToString(), 8, 8);
                    vNodeExecFolder.Tag = vCurrODBC.OdbcName + "_pr";
                    e.Node.Nodes.Add(vNodeExecFolder);

                    foreach (var vCurrProced in vCurrODBC.SQLProgramms)
                    {
                        TreeNode vNodeView = new TreeNode(vCurrProced.Name, 11, 11);
                        vNodeView.Tag = vCurrODBC.OdbcName + "|" + vCurrProced.Name;
                        vNodeExecFolder.Nodes.Add(vNodeView);
                    }

                    return;
                }
            }
            catch (Exception er)
            {
                svcTool.ExpHandler(er, "treeODBC_NodeMouseClick 1 ");
            }
        }

        public static void getColumnsandIndexes(TreeNodeMouseClickEventArgs e)
        {
            try
            {

                String vNodeTag = e.Node.Parent.Parent.Text + '.' + e.Node.Text;
                var vCurrObjProp = svcODBC.vObjProp.Find(item => item.ObjName == vNodeTag);

                if (vCurrObjProp.objColumns == null)
                {
                    svcODBC.vObjProp.AddRange(svcODBC.getObjectProperties(e.Node.Parent.Parent.Text, e.Node.Text));
                    vCurrObjProp = svcODBC.vObjProp.Find(item => item.ObjName == vNodeTag);
                }

                if (vCurrObjProp.objColumns != null)
                    if (vCurrObjProp.objColumns.Count > 0)
                    {
                        e.Node.Nodes.Clear();

                        foreach (var vCurrColumn in vCurrObjProp.objColumns)
                        {
                            TreeNode vNodeColumn = new TreeNode(vCurrColumn.ToString(), 14, 14);
                            vNodeColumn.Tag = vNodeTag + '.' + vCurrColumn + "_clm";
                            e.Node.Nodes.Add(vNodeColumn);
                        }
                        if (e.Node.Tag.ToString().Contains("$TABLE$"))
                        {
                            e.Node.Tag = vNodeTag + ".TABLE";
                            TreeNode vNodeIndexFolder = new TreeNode("Indexes".ToString(), 12, 12);
                            vNodeIndexFolder.Tag = vNodeTag + "_idx";
                            e.Node.Nodes.Add(vNodeIndexFolder);
                            foreach (var vCurrIndx in vCurrObjProp.objIndexes)
                            {
                                TreeNode vNodeIndx = new TreeNode(vCurrIndx.ToString(), 13, 13);
                                vNodeIndx.Tag = vNodeTag + '.' + vCurrIndx + "_idx";
                                vNodeIndexFolder.Nodes.Add(vNodeIndx);
                            }
                        }
                        else
                        {
                            e.Node.Tag = vNodeTag + ".VIEW";
                        }
                    }
            }
            catch (Exception er)
            {
                svcTool.ExpHandler(er, "getColumnsandIndexes ");
            }

        }
    }
}
