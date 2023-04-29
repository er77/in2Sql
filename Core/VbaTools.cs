using System;
using System.Text.RegularExpressions;
using System.Windows.Forms; 

namespace SqlEngine
{
    class VbaTools
    {


        public static string RemoveBetween(string s, char begin, char end)
        {
            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return regex.Replace(s, string.Empty);
        }


        public static string RemoveSqlLimit(string vCurrSql)
        {
            string vSql = RemoveBetween(vCurrSql, '`', '`');
            vSql = vSql.Replace("/**/", "");
            vSql = vSql.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);

            return vSql;
        }


        public static void RibbonPivotExcel()
        {
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;
            if (vActivCell.ListObject == null)
            {
                MessageBox.Show(" Please, select cell from the table", " Refresh error");
                return;
            }

            sTool.CurrentTableRecords vCTR = sTool.getCurrentSql();

            if (vCTR.TypeConnection.Contains("ODBC"))
            {
                string vSql = RemoveSqlLimit(vActivCell.ListObject.QueryTable.CommandText);
                VbaEngine.createPivotTable(VbaEngine.getOdbcNameFromCell(), sTool.GetHash(vSql), vSql);
            }

            if (vCTR.TypeConnection.Contains("CLOUD"))
            {
                SqlEngine.currExcelApp.SendKeys("%NVT");
                SqlEngine.currExcelApp.ActiveSheet.PivotTableWizard();                    
            }



            GetSelectedTab();

        }

    /*    public static void runTableProperties()
        {
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;
            // SqlEngine.currExcelApp.CommandBars.ExecuteMso("EditQuery");
            if ((vActivCell.ListObject == null) == false)
            {
                SqlEngine.currExcelApp.ScreenUpdating = false;

                SqlEngine.currExcelApp.SendKeys("%A%P%S");
                SqlEngine.currExcelApp.SendKeys("+");
                SqlEngine.currExcelApp.CommandBars.ReleaseFocus();

                SqlEngine.currExcelApp.ScreenUpdating = true;
            }
            else
                MessageBox.Show(" Please, select  the external table", " Refresh error");

            GetSelectedTab();
        }
    */
        public static void runSqlProperties()
        {
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;
            // SqlEngine.currExcelApp.CommandBars.ExecuteMso("EditQuery");
            if ((vActivCell.ListObject == null) == false)
                  SqlEngine.currExcelApp.ActiveSheet.ListObjects[1].QueryTable.ShowQueryEditor = true;
             //   MessageBox.Show(" SqlEngine.currExcelApp.ActiveSheet.ListObjects[1].QueryTable.ShowQueryEditor = true", " Refresh error");
            else
                MessageBox.Show(" Please, select  the external table", " Refresh error");

            GetSelectedTab();
        }
         
        public static void runPowerPivotM()
        {
            SqlEngine.currExcelApp.SendKeys("%a%d%m");
                  GetSelectedTab();
        }

        public static void GetSelectedTab ()
        {
            SqlEngine.currExcelApp.ScreenUpdating = false;            
            //var wnd2 = SqlEngine.currExcelApp.ActiveWindow.Hwnd;
           // SqlEngine.currExcelApp.s
              //  .SendMessageW(wnd2, &H400, 0, "in2sql")
            //SqlEngine.currExcelApp.("in2sql");// "customTab2" SendKeys("%Y%Q%A");
            // SqlEngine.currExcelApp.SendKeys("%");
             SqlEngine.currExcelApp.CommandBars.ReleaseFocus();//CommandBars.ReleaseFocus 
             
            SqlEngine.currExcelApp.ScreenUpdating = true;
        }
         

    }
    }
