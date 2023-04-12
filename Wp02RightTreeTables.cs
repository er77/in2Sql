using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SqlEngine
{
    class Wp02RightTreeTables
    {
        private static void InitSQlObjects(ref SOdbc.OdbcProperties vCurrOdbc)
        {
            try
            {
                if (vCurrOdbc.Tables == null)
                {
                    vCurrOdbc.Tables = new List<SOdbc.SqlObjects>();
                }

                if (vCurrOdbc.Views == null)
                {
                    vCurrOdbc.Views = new List<SOdbc.SqlObjects>();
                }

                if (vCurrOdbc.SqlPrograms == null)
                {
                    vCurrOdbc.SqlPrograms = new List<SOdbc.SqlObjects>();
                }

                if (vCurrOdbc.SqlFunctions == null)
                {
                    vCurrOdbc.SqlFunctions = new List<SOdbc.SqlObjects>();
                }

                if (vCurrOdbc.Tables.Count == 0)
                {
                    vCurrOdbc.Tables.AddRange(SOdbc.GetTableList(vCurrOdbc.OdbcName));
                }

                if (vCurrOdbc.Views.Count == 0)
                {
                    vCurrOdbc.Views.AddRange(SOdbc.GetViewList(vCurrOdbc.OdbcName));
                }

                if (vCurrOdbc.SqlPrograms.Count == 0)
                {
                    vCurrOdbc.SqlPrograms.AddRange(SOdbc.GetSqlProgrammsList(vCurrOdbc.OdbcName));
                }

                if (vCurrOdbc.SqlFunctions.Count == 0)
                {
                    vCurrOdbc.SqlFunctions.AddRange(SOdbc.GetSqlFunctionsList(vCurrOdbc.OdbcName));
                }

            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "InitSQlObjects");
            }
        } 

        public static void SetOdbcTreeLineSimple(TreeNode nodeToAddTo, string vOdbcName, string vOdbcType= "ODBC$")
        {
            var vNodeDatabase = new TreeNode(vOdbcName, 1, 1);

            nodeToAddTo.Nodes.Add(vNodeDatabase);
            vNodeDatabase.Tag = vOdbcType;
            var vNodeTable = new TreeNode(" ", 100, 100); // vNodeTable.Tag = vCurrTable.Name;
            vNodeDatabase.Nodes.Add(vNodeTable);
        }

        public static void SetCsvTreeLineSimple(TreeNode nodeToAddTo, string vOdbcName, string vOdbcType = "CSV$")
        {
            var vNodeDatabase = new TreeNode(vOdbcName, 21, 21);

            nodeToAddTo.Nodes.Add(vNodeDatabase);
            vNodeDatabase.Tag = vOdbcType;
            var vNodeTable = new TreeNode(" ", 100, 100); // vNodeTable.Tag = vCurrTable.Name;
            vNodeDatabase.Nodes.Add(vNodeTable);
        }


        public static void SetSqLiteTreeLineSimple(TreeNode nodeToAddTo, string vOdbcName, string vOdbcType = "SQLITE$")
        {
            TreeNode vNodeDatabase = new TreeNode(vOdbcName, 8, 8);

            nodeToAddTo.Nodes.Add(vNodeDatabase);
            vNodeDatabase.Tag = "FLD|" + vOdbcType;
            TreeNode vNodeTable = new TreeNode(" ", 100, 100); // vNodeTable.Tag = vCurrTable.Name;
            vNodeDatabase.Nodes.Add(vNodeTable);
        }

        public static void SetOdbcTreeLineComplex(TreeNode nodeToAddTo, string vCurrvListOdbcName, string vCurrOdbcName)
        {
            try
            {
                var vCurrOdbc = SOdbc.OdbcPropertiesList.Find(item => item.OdbcName == vCurrvListOdbcName);

                if ((vCurrOdbc.ConnStatus == 0))
                {
                    SetOdbcTreeLineSimple(nodeToAddTo, vCurrvListOdbcName);
                    return;
                }


                if (vCurrOdbc.ConnStatus < 0)
                {
                    TreeNode vNodeDatabase = new TreeNode(vCurrOdbc.OdbcName, 7, 7);
                    nodeToAddTo.Nodes.Add(vNodeDatabase);
                    vNodeDatabase.Tag = "ODBC%";
                    TreeNode vNodeTable = new TreeNode(vCurrOdbc.ConnErrMsg, 99, 99);
                    vNodeDatabase.Nodes.Add(vNodeTable);

                    return;
                }

                if ((vCurrOdbc.ConnStatus == 1) & vCurrvListOdbcName.Contains(vCurrOdbcName) & vCurrvListOdbcName.Length == vCurrOdbcName.Length)
                {
                    InitSQlObjects(ref vCurrOdbc);

                }

                if (vCurrOdbc.ConnStatus == 1 & (vCurrOdbc.Tables.Count == 0 & vCurrOdbc.Views.Count == 0))
                {
                    TreeNode vNodeDatabase = new TreeNode(vCurrOdbc.OdbcName, 2, 2);

                    nodeToAddTo.Nodes.Add(vNodeDatabase);
                    vNodeDatabase.Tag = "ODBC#";

                    return;
                }

                if (vCurrOdbc.ConnStatus == 1 & (vCurrOdbc.Tables.Count > 0 | vCurrOdbc.Views.Count > 0))
                {
                    TreeNode vNodeDatabase = new TreeNode(vCurrOdbc.OdbcName, 2, 2);

                    nodeToAddTo.Nodes.Add(vNodeDatabase);
                    vNodeDatabase.Tag = "ODBC#";

                    if (vCurrOdbc.Tables.Count > 0)
                    {
                        TreeNode vNodeTableFolder = new TreeNode("Tables", 3, 3);
                        vNodeTableFolder.Tag = vCurrOdbc.OdbcName + "tf";
                        vNodeDatabase.Nodes.Add(vNodeTableFolder);

                        foreach (var vCurrTable in vCurrOdbc.Tables)
                        {
                            TreeNode vNodeTable = new TreeNode(vCurrTable.Name, 4, 4); // vNodeTable.Tag = vCurrTable.Name;
                            vNodeTableFolder.Nodes.Add(vNodeTable);
                        }
                    }

                    if (vCurrOdbc.Views.Count > 0)
                    {
                        TreeNode vNodeViewFolder = new TreeNode("Views", 5, 5);
                        vNodeViewFolder.Tag = vCurrOdbc.OdbcName + "vf";
                        vNodeDatabase.Nodes.Add(vNodeViewFolder);

                        foreach (var vCurrView in vCurrOdbc.Views)
                        {
                            TreeNode vNodeView = new TreeNode(vCurrView.Name, 6, 6); // vNodeTable.Tag = vCurrTable.Name;
                            vNodeViewFolder.Nodes.Add(vNodeView);
                        }
                    }
                }
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "SetOdbcTreeLineComplex");
            }
        }
         

        public static void GetTablesAndViews(TreeNodeMouseClickEventArgs e)
        {
            e.Node.Nodes.Clear();
            var vCurrOdbcName = e.Node.Text;
            SOdbc.CheckOdbcStatus(vCurrOdbcName);

            var vCurrOdbc = SOdbc.OdbcPropertiesList.Find(item => item.OdbcName == vCurrOdbcName);

            try
            {
                if ((vCurrOdbc.ConnStatus == 0))
                {
                    return;
                }

                if (vCurrOdbc.ConnStatus < 0)
                {
                    e.Node.ImageIndex = 7;
                    e.Node.SelectedImageIndex = 7;
                    e.Node.Tag = "ODBC%";
                    TreeNode vNodeErrRecord = new TreeNode(vCurrOdbc.ConnErrMsg, 99, 99);
                    e.Node.Nodes.Add(vNodeErrRecord);
                    return;
                }

                if (vCurrOdbc.ConnStatus == 1)
                {
                    e.Node.ImageIndex = 2;
                    e.Node.SelectedImageIndex = 2;
                    e.Node.Tag = "ODBC#";
                    TreeNode vNodeTableFolder = new TreeNode("Tables", 3, 3);
                    vNodeTableFolder.Tag = vCurrOdbc.OdbcName + "_tf";
                    e.Node.Nodes.Add(vNodeTableFolder);

                    InitSQlObjects(ref vCurrOdbc);

                    foreach (var vCurrTable in vCurrOdbc.Tables)
                    {
                        TreeNode vNodeTable = new TreeNode(vCurrTable.Name, 4, 4);
                        vNodeTable.Tag = vCurrOdbc.OdbcName + "|" + vCurrTable.Name + "|$TABLE$";
                        vNodeTableFolder.Nodes.Add(vNodeTable);
                        TreeNode vNodeColumnTbl = new TreeNode(" ", 99, 99);
                        vNodeColumnTbl.Tag = vCurrOdbc.OdbcName + "." + vCurrTable.Name;
                        vNodeTable.Nodes.Add(vNodeColumnTbl);
                    }

                    TreeNode vNodeViewFolder = new TreeNode("Views", 5, 5);
                    vNodeViewFolder.Tag = vCurrOdbc.OdbcName + "_vf";
                    e.Node.Nodes.Add(vNodeViewFolder);

                    foreach (var vCurrView in vCurrOdbc.Views)
                    {
                        TreeNode vNodeView = new TreeNode(vCurrView.Name, 6, 6);
                        vNodeView.Tag = vCurrOdbc.OdbcName + "." + vNodeView.Name + "|$VIEW$";
                        vNodeViewFolder.Nodes.Add(vNodeView);
                        TreeNode vNodeColumnVw = new TreeNode(" ", 99, 99);
                        vNodeColumnVw.Tag = vCurrOdbc.OdbcName + "." + vNodeView.Name;
                        vNodeView.Nodes.Add(vNodeColumnVw);
                    }

                    TreeNode vNodeFunctionFolder = new TreeNode("Functions", 10, 10);
                    vNodeFunctionFolder.Tag = vCurrOdbc.OdbcName + "_fn";
                    e.Node.Nodes.Add(vNodeFunctionFolder);

                    foreach (var vCurrFunc in vCurrOdbc.SqlFunctions)
                    {
                        TreeNode vNodeView = new TreeNode(vCurrFunc.Name, 9, 9);
                        vNodeView.Tag = vCurrOdbc.OdbcName + "." + vCurrFunc.Name;
                        vNodeFunctionFolder.Nodes.Add(vNodeView);
                    }

                    TreeNode vNodeExecFolder = new TreeNode("Procedures", 8, 8);
                    vNodeExecFolder.Tag = vCurrOdbc.OdbcName + "_pr";
                    e.Node.Nodes.Add(vNodeExecFolder);

                    foreach (var vCurrProced in vCurrOdbc.SqlPrograms)
                    {
                        var vNodeView = new TreeNode(vCurrProced.Name, 11, 11)
                        {
                            Tag = vCurrOdbc.OdbcName + "|" + vCurrProced.Name
                        };
                        vNodeExecFolder.Nodes.Add(vNodeView);
                    }

                }
            }
            catch (Exception er)
            {
                STool.ExpHandler(er, "treeODBC_NodeMouseClick 1 ");
            }
        }

        public static void GetColumnsandIndexes(TreeNodeMouseClickEventArgs e)
        {
            try
            {

                String vNodeTag = e.Node.Parent.Parent.Text + '.' + e.Node.Text;
                var vCurrObjProp = SOdbc.ObjectsAndPropertiesList.Find(item => item.ObjName == vNodeTag);

                if (vCurrObjProp.ObjColumns == null)
                {
                    SOdbc.ObjectsAndPropertiesList.AddRange(SOdbc.GetObjectProperties(e.Node.Parent.Parent.Text, e.Node.Text));
                    vCurrObjProp = SOdbc.ObjectsAndPropertiesList.Find(item => item.ObjName == vNodeTag);
                }

                if (vCurrObjProp.ObjColumns != null)
                    if (vCurrObjProp.ObjColumns.Count > 0)
                    {
                        e.Node.Nodes.Clear();

                        foreach (var vCurrColumn in vCurrObjProp.ObjColumns)
                        {
                            TreeNode vNodeColumn = new TreeNode(vCurrColumn, 14, 14);
                            vNodeColumn.Tag = vNodeTag + '.' + vCurrColumn + "_clm";
                            e.Node.Nodes.Add(vNodeColumn);
                        }
                        if (e.Node.Tag.ToString().Contains("$TABLE$"))
                        {
                            e.Node.Tag = vNodeTag + ".TABLE";
                            var vNodeIndexFolder = new TreeNode("Indexes", 12, 12)
                            {
                                Tag = vNodeTag + "_idx"
                            };
                            e.Node.Nodes.Add(vNodeIndexFolder);
                            foreach (var vCurrIndx in vCurrObjProp.ObjIndexes)
                            {
                                var vNodeIndx = new TreeNode(vCurrIndx, 13, 13)
                                {
                                    Tag = vNodeTag + '.' + vCurrIndx + "_idx"
                                };
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
                STool.ExpHandler(er, "getColumnsandIndexes ");
            }

        }
    }
}
