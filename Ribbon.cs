using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;

 

namespace SqlEngine
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        private struct BooksVsPannel
        {
            public Microsoft.Office.Tools.CustomTaskPane ObjPanelRightSqlM;
            public Microsoft.Office.Tools.CustomTaskPane ObjIn2SqlLeftOtl;
            public Wf03PanelRightSqlM RightPaneCntrlSqlm;
            public Wf02PaneLeftOutline LeftPaneCntrlOtl;
            public string BookName;
        }

        private List<BooksVsPannel> vListofPanes; 

          private void AddToListPanes ()
        { 
            if (vListofPanes == null)
                vListofPanes = new List<BooksVsPannel>();           

            var  pannel = vListofPanes.Find(item => item.BookName == SqlEngine.CurrExcelApp.ActiveWorkbook.Name);

            if (pannel.BookName != null) return;
            
            pannel.BookName = SqlEngine.CurrExcelApp.ActiveWorkbook.Name;

            pannel.LeftPaneCntrlOtl = new Wf02PaneLeftOutline();
            pannel.ObjIn2SqlLeftOtl = Globals.SqlEngine.CustomTaskPanes.Add(pannel.LeftPaneCntrlOtl, "in2Sql.outline");
            pannel.ObjIn2SqlLeftOtl.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionLeft;
            pannel.ObjIn2SqlLeftOtl.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
            pannel.ObjIn2SqlLeftOtl.Visible = false;
            pannel.ObjIn2SqlLeftOtl.Width = 200;

            pannel.RightPaneCntrlSqlm = new Wf03PanelRightSqlM();
            pannel.ObjPanelRightSqlM = Globals.SqlEngine.CustomTaskPanes.Add(pannel.RightPaneCntrlSqlm, "in2Sql.explorer");
            pannel.ObjPanelRightSqlM.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
            pannel.ObjPanelRightSqlM.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
            pannel.ObjPanelRightSqlM.Visible = false;
            pannel.ObjPanelRightSqlM.Width = 200;

            vListofPanes.Add(pannel);
        }

        private void InitLeftPaneOtl()
        {
            if (vListofPanes == null)
                AddToListPanes();

            if (vListofPanes == null) return;
            
            var pannel = vListofPanes.Find(item => item.BookName == SqlEngine.CurrExcelApp.ActiveWorkbook.Name);

            if (pannel.ObjIn2SqlLeftOtl != null) return;
            
            {
                AddToListPanes();
                pannel = vListofPanes.Find(item => item.BookName == SqlEngine.CurrExcelApp.ActiveWorkbook.Name);
                pannel.ObjIn2SqlLeftOtl.Visible = true;
            }
        }

        private Wf07OdbcProperties frmShowOdbcProp;
        public void ShowOdbcProp()
        {
            if (frmShowOdbcProp == null  )
                frmShowOdbcProp = new Wf07OdbcProperties();

            if (  frmShowOdbcProp.IsDisposed  )
               frmShowOdbcProp = new Wf07OdbcProperties(); 
            frmShowOdbcProp.Show();

        }

        //


        private Wf08About frmshowAbout;
        public void ShowAbout()
        {
            if (frmshowAbout == null)
                frmshowAbout = new Wf08About();

            if (frmshowAbout.IsDisposed)
                frmshowAbout = new Wf08About();
            frmshowAbout.Show();
        }


            public void ShowEditPane()
        {
            if (vListofPanes == null)
                AddToListPanes();

            if (vListofPanes == null) return;
            
            var pannel = vListofPanes.Find(item => item.BookName == SqlEngine.CurrExcelApp.ActiveWorkbook.Name );

            if (pannel.ObjPanelRightSqlM != null)
            {
                pannel.ObjPanelRightSqlM.Visible = !pannel.ObjPanelRightSqlM.Visible;
                if (pannel.ObjPanelRightSqlM.Visible)
                    pannel.ObjPanelRightSqlM.Width = 600;
                pannel.RightPaneCntrlSqlm.ShowSqlEdit();

            }
            else
            {
                AddToListPanes();
                pannel = vListofPanes.Find(item => item.BookName == SqlEngine.CurrExcelApp.ActiveWorkbook.Name);
                pannel.ObjPanelRightSqlM.Visible = true;
                pannel.ObjPanelRightSqlM.Width = 600;
                pannel.RightPaneCntrlSqlm.ShowSqlEdit();
            }
        }

        public void ShowSQlMAnPane()
        {
            if ( vListofPanes == null )
                AddToListPanes();

            if (vListofPanes == null) return;
            
            var pannel = vListofPanes.Find(item => item.BookName == SqlEngine.CurrExcelApp.ActiveWorkbook.Name);
            if (pannel.ObjPanelRightSqlM == null)
            {
                AddToListPanes();
            }
            else
            {
                pannel.ObjPanelRightSqlM.Visible = !pannel.ObjPanelRightSqlM.Visible;

                if (!pannel.ObjPanelRightSqlM.Visible) return;
                
                pannel.RightPaneCntrlSqlm.ShowOdbcTree();
                pannel.ObjPanelRightSqlM.Width = 200;
            }
        }

 

        public void ShowOutlinePane()
        {
            if (vListofPanes == null)
                AddToListPanes();

            if (vListofPanes == null) return;
            
            var pannel = vListofPanes.Find(item => item.BookName == SqlEngine.CurrExcelApp.ActiveWorkbook.Name);
            if (pannel.ObjIn2SqlLeftOtl == null)
            {
                InitLeftPaneOtl();
            }
            else
            {
                pannel.ObjIn2SqlLeftOtl.Visible = !pannel.ObjIn2SqlLeftOtl.Visible;                
            }
        }


        public void ActivateTab()
        {
            if (ribbon == null)
            {
                STool.RunGarbageCollector();
                return;
            }
            
            ribbon.ActivateTab("SqlEngine");
            
        }

        public Ribbon()
        {

        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonId)
        {
            return GetResourceText("SqlEngine.Ribbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        public static int VRowCount = 10000;

        public void SetRowCount(Office.IRibbonControl vControl,String text)
        {
            if (!int.TryParse(text, out VRowCount)) return;
            
            if(  VRowCount > 0 & VRowCount < 1000001)
                MessageBox.Show(@"Row limit  is " + VRowCount,   @" Row Count");
            else
                VRowCount = 10000;
        }

        public string GetRowCount(Office.IRibbonControl vControl)
        {
            return "" + VRowCount;
        }

        private Wf04EditQuery frmShowSqlEdit; 

        private void ShowSqlEdit ()
        {
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
            if  (activeCell.ListObject == null) 
            {
                MessageBox.Show(@"Please select table with SQL query");
                return;
            }
            if ( frmShowSqlEdit == null )
                frmShowSqlEdit = new Wf04EditQuery();
            if (  frmShowSqlEdit.IsDisposed )
              frmShowSqlEdit = new Wf04EditQuery(); 

            frmShowSqlEdit.Show();
        }
        

        public void ExecMenuButton(Office.IRibbonControl vControl)
        {
            try
            {
                STool.RunGarbageCollector();

                switch (vControl.Id)
                {
                    case "ExecConnManager":
                        ShowSQlMAnPane();
                        ActivateTab();
                        break;


                    case "ODBCManager":
                        STool.RunCmdLauncher("odbcad32");
                        ActivateTab();
                        break;

                    case "OdbcProp":
                        ShowOdbcProp();
                        ActivateTab();
                        break;

                    case "BackOutl":
                        ShowOutlinePane();
                        ActivateTab();
                        break;                       

                    case "SqlEdit":
                        ShowEditPane();
                        break;

                    case "KeepOnly":
                        IntSqlVbaEngine.RibbonKeepOnly();
                        ActivateTab();
                        break;

                    case "RemoveOnly":
                        IntSqlVbaEngine.RibbonRemoveOnly();
                        ActivateTab();
                        break;

                    case "Retrieve":
                        IntSqlVbaEngine.RibbonRefresh();
                        ActivateTab();
                        break;

                    case "RetrieveAll":
                        IntSqlVbaEngine.RibbonRefreshAll();
                        ActivateTab();
                        break;
                         

                    case "EditQuery":
                        ShowSqlEdit();
                        ActivateTab();
                        break;

                    case "PivotExcel":
                        IntSqlVbaEngine.RibbonPivotExcel();
                        ActivateTab();
                        break;

                    case "Undo":
                        IntSqlVbaEngine.Undo();
                        ActivateTab();
                        break;

                    case "Redo":
                        IntSqlVbaEngine.Redo();
                        ActivateTab();
                        break;

                    //  ()
                    case "UpdateDataAll":
                         IntSqlVbaEngine.UpdateTablesAll();
                        ActivateTab();
                        break;
                    case "UpdateData":
                        IntSqlVbaEngine.UpdateTables();
                        ActivateTab();
                        break;
                    //

                    case "PowerPivotMM":
                        IntSqlVbaEngine.RunPowerPivotM();
                        //intSqlVBAEngine.checkTableName();
                        ActivateTab();
                        break;

                    case "Options":
                        IntSqlVbaEngine.RunSqlProperties();
                        //intSqlVBAEngine.checkTableName();
                        ActivateTab();
                        break;

                    case "TableProp":
                        IntSqlVbaEngine.RunTableProperties(); 
                        break;

                    case "About":
                        ShowAbout();
                        break;
                }
                
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "ExecMenuButton");
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
                STool.ExpHandler(e, "ExecDropDown");
            }

        }



        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resourceNames = asm.GetManifestResourceNames();
            foreach (var t in resourceNames)
            {
                if (string.Compare(resourceName, t, StringComparison.OrdinalIgnoreCase) != 0) continue;
                using (var resourceReader = new StreamReader(asm.GetManifestResourceStream(t)))
                { 
                    return resourceReader.ReadToEnd();
                }
            }
            return null;
        }

        #endregion
    }
}
