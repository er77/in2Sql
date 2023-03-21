using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;


namespace SqlEngine
{
    internal class sRibbon
    {

        private struct BooksVsPannel
        {
            public Microsoft.Office.Tools.CustomTaskPane objPanelRigtSqlM;
            public Microsoft.Office.Tools.CustomTaskPane objIn2sqlLeftOtl;
            public wf03PanelRigtSqlM rightPaneCntrlSqlm;
            public wf02PaneLeftOtline leftPaneCntrlOtl;
            public string BookName;
        }

        private static List<BooksVsPannel> vListofPanes;

        private static void addToListPanes()
        {
            if (vListofPanes == null)
                vListofPanes = new List<BooksVsPannel>();

            var vcurrPannel = vListofPanes.Find(item => item.BookName == SqlEngine.currExcelApp.ActiveWorkbook.Name);

            if (vcurrPannel.BookName == null)
            {
                vcurrPannel.BookName = SqlEngine.currExcelApp.ActiveWorkbook.Name;

                vcurrPannel.leftPaneCntrlOtl = new wf02PaneLeftOtline();
                vcurrPannel.objIn2sqlLeftOtl = Globals.SqlEngine.CustomTaskPanes.Add(vcurrPannel.leftPaneCntrlOtl, "in2Sql.outline");
                vcurrPannel.objIn2sqlLeftOtl.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionLeft;
                vcurrPannel.objIn2sqlLeftOtl.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
                vcurrPannel.objIn2sqlLeftOtl.Visible = false;
                vcurrPannel.objIn2sqlLeftOtl.Width = 200;

                vcurrPannel.rightPaneCntrlSqlm = new wf03PanelRigtSqlM();
                vcurrPannel.objPanelRigtSqlM = Globals.SqlEngine.CustomTaskPanes.Add(vcurrPannel.rightPaneCntrlSqlm, "in2Sql.explorer");
                vcurrPannel.objPanelRigtSqlM.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                vcurrPannel.objPanelRigtSqlM.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
                vcurrPannel.objPanelRigtSqlM.Visible = false;
                vcurrPannel.objPanelRigtSqlM.Width = 200;

                vListofPanes.Add(vcurrPannel);
            }
        }

        private static void InitLeftPaneOtl()
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

        public static void showOutlinePane()
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

        public void showSQlMAnPane()
        {
            if (vListofPanes == null)
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

        public static void showEditPane()
        {
            if (vListofPanes == null)
                addToListPanes();

            var vcurrPannel = vListofPanes.Find(item => item.BookName == SqlEngine.currExcelApp.ActiveWorkbook.Name);

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
          
        public static void RunExecConnManager ( )
        {
            if (vListofPanes == null)
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
                    vcurrPannel.objPanelRigtSqlM.Width = 300;
                }
            }
        }
    }
}
