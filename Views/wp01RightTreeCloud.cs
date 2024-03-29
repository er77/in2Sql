﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SqlEngine.sCloud;
using static SqlEngine.sCsv;

namespace SqlEngine
{
    class wp01RightTreeCloud
    {
      
            private static void initCloudObjects(ref sCloud.CloudProperties vCurrCloudObj)
            {
                try
                {
                    if (vCurrCloudObj.Tables == null)
                    {
                        vCurrCloudObj.Tables = new List<sCloud.CloudObjects>();
                    }

                    if (vCurrCloudObj.Views == null)
                    {
                        vCurrCloudObj.Views = new List<sCloud.CloudObjects>();
                    }

                    if (vCurrCloudObj.SQLProgramms == null)
                    {
                        vCurrCloudObj.SQLProgramms = new List<sCloud.CloudObjects>();
                    }

                    if (vCurrCloudObj.SQLFunctions == null)
                    {
                        vCurrCloudObj.SQLFunctions = new List<sCloud.CloudObjects>();
                    }

                    if (vCurrCloudObj.Tables.Count == 0)
                    {
                        vCurrCloudObj.Tables.AddRange(sCloud.getCloudTableList(vCurrCloudObj.CloudName));
                    }

                    if (vCurrCloudObj.Views.Count == 0)
                    {
                        vCurrCloudObj.Views.AddRange(sCloud.getCloudViewList(vCurrCloudObj.CloudName));
                    }                     

                }
                catch (Exception er)
                {
                    sTool.ExpHandler(er, "initCloudObjects");
                }
            }


        private static void initCsvObjects(ref sCsv.FolderProperties vCurrCloudObj)
        {
            try
            {
                if (vCurrCloudObj.Files == null)
                {
                    vCurrCloudObj.Files = new List<sCsv.CloudObjects>();
                } 

                if (vCurrCloudObj.Files.Count == 0)
                {
                    vCurrCloudObj.Files.AddRange(sCsv.getFileList(vCurrCloudObj.FolderName));
                }                 
            }
            catch (Exception er)
            {
                sTool.ExpHandler(er, "initCloudObjects");
            }
        }

        public static void getCsvFilesList (TreeNodeMouseClickEventArgs e)
        {
            e.Node.Nodes.Clear();
            string vCurrFolderName = e.Node.Text;
             
            FolderProperties vCurrFolder = sCsv.vFolderList.Find(item => item.FolderName == vCurrFolderName);

            try
            {
               
                
               /* e.Node.SelectedImageIndex = 22;
                e.Node.Tag = "CSV#";
                TreeNode vNodeTableFolder = new TreeNode("Tables".ToString(), 3, 3);
                vNodeTableFolder.Tag = vCurrFolder.FolderName + "_csv";
                e.Node.Nodes.Add(vNodeTableFolder);
                */
                initCsvObjects(ref vCurrFolder);

                foreach (var vCurrFile in vCurrFolder.Files)
                {
                    TreeNode vNodeTable = new TreeNode(vCurrFile.Name, 22, 22);
                    vNodeTable.Tag = vCurrFolder.FolderName + "|" + vCurrFile.Name + "|$FILE_CSV$";
                    e.Node.Nodes.Add(vNodeTable);
                    TreeNode vNodeColumnTbl = new TreeNode(" ".ToString(), 99, 99);
                    vNodeColumnTbl.Tag = vCurrFolder.FolderName + "." + vCurrFile.Name;
                    vNodeTable.Nodes.Add(vNodeColumnTbl);
                }
 
                return;
            }
            catch (Exception er)
            {
                sTool.ExpHandler(er, "getCsvFilesList 1 ");
            }
        }

        public static void getCloudTablesAndViews(TreeNodeMouseClickEventArgs e)
            {
                e.Node.Nodes.Clear();
                string vCurrCloudName = e.Node.Text; 

                CloudProperties vCurrCloud = sCloud.vCloudList.Find(item => item.CloudName == vCurrCloudName);

                try
                {
                    if ((sCloud.checkCloudState(vCurrCloudName) < 0))
                    {
                        return;
                    }

                    e.Node.ImageIndex = 2;
                    e.Node.SelectedImageIndex = 2;
                    e.Node.Tag = vCurrCloud.CloudType + '#';
                    TreeNode vNodeTableFolder = new TreeNode("Tables".ToString(), 3, 3);
                    vNodeTableFolder.Tag = vCurrCloud.CloudName + "_tf";
                    e.Node.Nodes.Add(vNodeTableFolder);

                    initCloudObjects(ref vCurrCloud);

                    foreach (var vCurrTable in vCurrCloud.Tables)
                    {
                        TreeNode vNodeTable = new TreeNode(vCurrTable.Name, 4, 4);
                        vNodeTable.Tag = vCurrCloud.CloudName + "|" + vCurrTable.Name + "|$TABLE_CLD$";
                        vNodeTableFolder.Nodes.Add(vNodeTable);
                        TreeNode vNodeColumnTbl = new TreeNode(" ".ToString(), 99, 99);
                        vNodeColumnTbl.Tag = vCurrCloud.CloudName + "." + vCurrTable.Name;
                        vNodeTable.Nodes.Add(vNodeColumnTbl);
                    }

                    TreeNode vNodeViewFolder = new TreeNode("Views".ToString(), 5, 5);
                    vNodeViewFolder.Tag = vCurrCloud.CloudName + "_vf";
                    e.Node.Nodes.Add(vNodeViewFolder);

                    foreach (var vCurrView in vCurrCloud.Views)
                    {
                        TreeNode vNodeView = new TreeNode(vCurrView.Name, 6, 6);
                        vNodeView.Tag = vCurrCloud.CloudName + "." + vNodeView.Name + "|$VIEW_CLD$";
                        vNodeViewFolder.Nodes.Add(vNodeView);
                        TreeNode vNodeColumnVw = new TreeNode(" ".ToString(), 99, 99);
                        vNodeColumnVw.Tag = vCurrCloud.CloudName + "." + vNodeView.Name;
                        vNodeView.Nodes.Add(vNodeColumnVw);
                    }
                    return;
                }
                catch (Exception er)
                {
                    sTool.ExpHandler(er, "treeODBC_NodeMouseClick 1 ");
                }
            }

        public static void getCsvHeaders (TreeNodeMouseClickEventArgs e)
        {
            try
            {
                String vNodeTag = e.Node.Parent.Text + '.' + e.Node.Text;

                FilesAndProperties vCurrObjProp = sCsv.vFileObjProp.Find(item => item.ObjName == vNodeTag); 

                if (vCurrObjProp.objColumns == null)
                {
                    sCsv.vFileObjProp.AddRange(sCsv.getCsvFileColumn(e.Node.Parent.Text, e.Node.Text));
                    vCurrObjProp = sCsv.vFileObjProp.Find(item => item.ObjName == vNodeTag);
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
                    }
            }
            catch (Exception er)
            {
                sTool.ExpHandler(er, "getColumnsandIndexes ");
            }

        }


        public static void getColumnsAndIndexes(TreeNodeMouseClickEventArgs e)
            {
                try
                {
                    String vNodeTag = e.Node.Parent.Parent.Text + '.' + e.Node.Text;
                    var vCurrObjProp = sCloud.vCloudObjProp.Find(item => item.ObjName == vNodeTag);

                    if (vCurrObjProp.objColumns == null)
                    {
                    sCloud.vCloudObjProp.AddRange(sCloud.getObjectProperties(e.Node.Parent.Parent.Text , e.Node.Text  ));
                        vCurrObjProp = sCloud.vCloudObjProp.Find(item => item.ObjName == vNodeTag);
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
                    sTool.ExpHandler(er, "getColumnsandIndexes ");
                }

            }
   

}
}
