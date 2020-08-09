﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;

 

namespace SqlEngine
{
    [ComVisible(true)]
    public class In2SqlRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        private struct BooksVsPannel
        {
            public Microsoft.Office.Tools.CustomTaskPane objPanelRigtSqlM;
            public Microsoft.Office.Tools.CustomTaskPane objIn2sqlLeftOtl;
            public in2SqlWF03PanelRigtSqlM rightPaneCntrlSqlm;
            public in2SqlWF02PaneLeftOtline leftPaneCntrlOtl;
            public string BookName;
        }

        private List<BooksVsPannel> vListofPanes; 

          private void addToListPanes ()
        { 
            if (vListofPanes == null)
                vListofPanes = new List<BooksVsPannel>();           

             var  vcurrPannel = vListofPanes.Find(item => item.BookName == SqlEngine.currExcelApp.ActiveWorkbook.Name);

            if (vcurrPannel.BookName == null)
            {
                vcurrPannel.BookName = SqlEngine.currExcelApp.ActiveWorkbook.Name;

                vcurrPannel.leftPaneCntrlOtl = new in2SqlWF02PaneLeftOtline();
                vcurrPannel.objIn2sqlLeftOtl = Globals.SqlEngine.CustomTaskPanes.Add(vcurrPannel.leftPaneCntrlOtl, "Outline");
                vcurrPannel.objIn2sqlLeftOtl.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionLeft;
                vcurrPannel.objIn2sqlLeftOtl.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
                vcurrPannel.objIn2sqlLeftOtl.Visible = false;
                vcurrPannel.objIn2sqlLeftOtl.Width = 200;

                vcurrPannel.rightPaneCntrlSqlm = new in2SqlWF03PanelRigtSqlM();
                vcurrPannel.objPanelRigtSqlM = Globals.SqlEngine.CustomTaskPanes.Add(vcurrPannel.rightPaneCntrlSqlm, "in2sql");
                vcurrPannel.objPanelRigtSqlM.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                vcurrPannel.objPanelRigtSqlM.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
                vcurrPannel.objPanelRigtSqlM.Visible = false;
                vcurrPannel.objPanelRigtSqlM.Width = 200;

                vListofPanes.Add(vcurrPannel);
            }
        }

        private void InitLeftPaneOtl()
        {
            if (vListofPanes == null)
                addToListPanes();

            var vcurrPannel = vListofPanes.Find(item => item.BookName == SqlEngine.currExcelApp.ActiveWorkbook.Name);

            if (vcurrPannel.objIn2sqlLeftOtl == null)
            {
                addToListPanes();
                vcurrPannel = vListofPanes.Find(item => item.BookName == SqlEngine.currExcelApp.ActiveWorkbook.Name);
                vcurrPannel.objIn2sqlLeftOtl.Visible = true;               
            }
        }

        in2SqlWF07OdbcProperties frmShowODBCProp = null;
        public void showODBCProp()
        {
            if (frmShowODBCProp == null  )
                frmShowODBCProp = new in2SqlWF07OdbcProperties();

            if (  frmShowODBCProp.IsDisposed  )
               frmShowODBCProp = new in2SqlWF07OdbcProperties(); 
            frmShowODBCProp.Show();

        }

        //



        in2SqlWF08About frmshowAbout = null;
        public void showAbout()
        {
            if (frmshowAbout == null)
                frmshowAbout = new in2SqlWF08About();

            if (frmshowAbout.IsDisposed)
                frmshowAbout = new in2SqlWF08About();
            frmshowAbout.Show();
        }


            public void showEditPane()
        {
            if (vListofPanes == null)
                addToListPanes();

            var vcurrPannel = vListofPanes.Find(item => item.BookName == SqlEngine.currExcelApp.ActiveWorkbook.Name );

            if (vcurrPannel.objPanelRigtSqlM != null)
            {
                vcurrPannel.objPanelRigtSqlM.Visible = !vcurrPannel.objPanelRigtSqlM.Visible;
                if (vcurrPannel.objPanelRigtSqlM.Visible)
                    vcurrPannel.objPanelRigtSqlM.Width = 600;
                vcurrPannel.rightPaneCntrlSqlm.showSqlEdit();

            }
            else
            {
                addToListPanes();
                  vcurrPannel = vListofPanes.Find(item => item.BookName == SqlEngine.currExcelApp.ActiveWorkbook.Name);
                vcurrPannel.objPanelRigtSqlM.Visible = true;
                vcurrPannel.objPanelRigtSqlM.Width = 600;
                vcurrPannel.rightPaneCntrlSqlm.showSqlEdit();
            }
        }

        public void showSQlMAnPane()
        {
            if ( vListofPanes == null )
                addToListPanes();

            var vcurrPannel = vListofPanes.Find(item => item.BookName == SqlEngine.currExcelApp.ActiveWorkbook.Name);
            if (vcurrPannel.objPanelRigtSqlM == null)
            {
                addToListPanes();
            }
            else
            {
                vcurrPannel.objPanelRigtSqlM.Visible = !vcurrPannel.objPanelRigtSqlM.Visible;

                if (vcurrPannel.objPanelRigtSqlM.Visible)
                {
                    vcurrPannel.rightPaneCntrlSqlm.ShowOdbcTree();
                    vcurrPannel.objPanelRigtSqlM.Width = 200;
                }
            }
        }

 

        public void showOutlinePane()
        {
            if (vListofPanes == null)
                addToListPanes();

            var vcurrPannel = vListofPanes.Find(item => item.BookName == SqlEngine.currExcelApp.ActiveWorkbook.Name);
            if (vcurrPannel.objIn2sqlLeftOtl == null)
            {
              InitLeftPaneOtl();
             }
            else
            {
                vcurrPannel.objIn2sqlLeftOtl.Visible = !vcurrPannel.objIn2sqlLeftOtl.Visible;                
            }
        }


        public void ActivateTab()
        {
            if (ribbon != null)
                ribbon.ActivateTab("SqlEngine");
            In2SqlSvcTool.RunGarbageCollector();
        }

        public In2SqlRibbon()
        {

        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("SqlEngine.In2SqlRibbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        public static int vRowCount = 10000;

        public void SetRowCount(Office.IRibbonControl vControl,String text)
        {
            if (int.TryParse(text, out vRowCount))
            {     if(  vRowCount > 0 & vRowCount < 1000001)
                    MessageBox.Show("Row limit  is " + vRowCount,   " Row Count");
                  else
                    vRowCount = 10000;
            } 
        }

        public string getRowCount(Office.IRibbonControl vControl)
        {
            return "" + vRowCount;
        }

        In2SqlWF04EditQuery frmShowSqlEdit = null ; 

        private void showSqlEdit ()
        {
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;
            if  (vActivCell.ListObject == null) 
            {
                MessageBox.Show("Please select table with SQL query");
                return;
            }
            if ( frmShowSqlEdit == null )
                frmShowSqlEdit = new In2SqlWF04EditQuery();
            if (  frmShowSqlEdit.IsDisposed )
              frmShowSqlEdit = new In2SqlWF04EditQuery(); 

            frmShowSqlEdit.Show();
        }
        

        public void ExecMenuButton(Office.IRibbonControl vControl)
        {
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;
            try
            {
                In2SqlSvcTool.RunGarbageCollector();

                switch (vControl.Id)
                {
                    case "ExecConnManager":
                        showSQlMAnPane();
                        ActivateTab();
                        break;


                    case "ODBCManager":
                        In2SqlSvcTool.RunCmdLauncher("odbcad32");
                        ActivateTab();
                        break;

                    case "OdbcProp":
                        showODBCProp();
                        ActivateTab();
                        break;

                    case "BackOutl":
                        showOutlinePane();
                        ActivateTab();
                        break;                       

                    case "SqlEdit":
                        showEditPane();
                        break;

                    case "KeepOnly":
                        intSqlVBAEngine.RibbonKeepOnly();
                        ActivateTab();
                        break;

                    case "RemoveOnly":
                        intSqlVBAEngine.RibbonRemoveOnly();
                        ActivateTab();
                        break;

                    case "Retrieve":
                        intSqlVBAEngine.RibbonRefresh();
                        ActivateTab();
                        break;

                    case "RetrieveAll":
                        intSqlVBAEngine.RibbonRefreshAll();
                        ActivateTab();
                        break;
                         

                    case "EditQuery":
                        showSqlEdit();
                        ActivateTab();
                        break;

                    case "PivotExcel":
                        intSqlVBAEngine.RibbonPivotExcel();
                        ActivateTab();
                        break;

                    case "Undo":
                        intSqlVBAEngine.Undo();
                        ActivateTab();
                        break;

                    case "Redo":
                        intSqlVBAEngine.Redo();
                        ActivateTab();
                        break;

                    //  ()
                    case "UpdateDataAll":
                         intSqlVBAEngine.updateTablesAll();
                        ActivateTab();
                        break;
                    case "UpdateData":
                        intSqlVBAEngine.updateTables();
                        ActivateTab();
                        break;
                    //

                    case "PowerPivotMM":
                        intSqlVBAEngine.runPowerPivotM();
                        //intSqlVBAEngine.checkTableName();
                        ActivateTab();
                        break;

                    case "Options":
                        intSqlVBAEngine.runSqlProperties();
                        //intSqlVBAEngine.checkTableName();
                        ActivateTab();
                        break;

                    case "TableProp":
                        intSqlVBAEngine.runTableProperties(); 
                        break;

                    case "About":
                        showAbout();
                     /*  var frmshowAbout = new in2SqlWF09CloudConnectionEditor();
                        frmshowAbout.Show();
                        ActivateTab();*/
                        break;


 

                    //RightTaskPane

                    default:
                        /*  string caption = "Information message";
                          MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                          DialogResult result;

                          // Displays the MessageBox.
                          result = MessageBox.Show(vControl.Id, caption, buttons);
                          */
                        break;


                }
               // In2SqlSvcTool.RunGarbageCollector();
            }
            catch (Exception e)
            {
                In2SqlSvcTool.ExpHandler(e, "ExecMenuButton");
            }

        }

        public void ExecDropDown(Office.IRibbonControl vControl)
        {
            try
            {
                /*  string caption = "Information message";
              MessageBoxButtons buttons = MessageBoxButtons.YesNo;
              DialogResult result;

              result = MessageBox.Show(vControl.Id, caption, buttons);*/
            }
            catch (Exception e)
            {
                In2SqlSvcTool.ExpHandler(e, "ExecDropDown");
            }

        }



        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
