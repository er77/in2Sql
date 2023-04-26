using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using SqlEngine;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Interop.Excel;
using System.Data.Odbc;
using System.Windows.Forms.VisualStyles;

namespace SqlEngine
{
    class intSqlVBAEngine
    {

        public static bool isRefresh = false;
        // intSqlVBAEngine.isRefresh = true;

        //Microsoft.Office.Interop.Excel.ListObject
        public struct InsertList
        { public List<string> SqlUpdate;
            public string DSNName;
        }

        public static List<InsertList> vInsertList = new List<InsertList>();

        public static InsertList newInsertRecord(String vDSNName, string vDDL)
        {
            InsertList vnewRecord;
            vnewRecord.DSNName = vDSNName;
            vnewRecord.SqlUpdate = new List<String>
                {
                    vDDL
                };
            return vnewRecord;
        }


        public static void addToInsertList(string vDSNName, string vDDL)
        {

            if (vInsertList.Count < 0)
            {
                vInsertList.Add(newInsertRecord(vDSNName, vDDL));
                return;
            }

            int vIntInsetId = vInsertList.FindIndex(item => item.DSNName == vDSNName);
            if (vIntInsetId < 0)
            {
                vInsertList.Add(newInsertRecord(vDSNName, vDDL));
                return;
            }
            vInsertList[vIntInsetId].SqlUpdate.Add(vDDL);
        }

        public void setExcelCalcOff()
        {
            /*  On Error Resume Next
  
  If ActiveSheet Is Nothing Then
        MsgBox "active sheet is not determinated "
        End
    End If
    
    vCurrQueryTime = Now
     Application.EnableCancelKey = xlErrorHandler
    Application.ScreenUpdating = False
    Application.Calculation = xlCalculationManual
    Application.EnableEvents = False
    ActiveSheet.DisplayPageBreaks = False
    ActiveSheet.UsedRange.EntireRow.Hidden = False
    Set vActiveCell = Range(ActiveCell.Address)
        
    If Err.Number <> 0 Then
       Err.Clear
    End If
             * 
             */
        }

        public void setExcelCalcOn()
        {
            /*
                   
    Call p_WriteStatusBarTime
      
    On Error Resume Next
    Application.EnableCancelKey = xlInterrupt
    Application.ScreenUpdating = True
    Application.Calculation = xlCalculationAutomatic
    Application.EnableEvents = True
    ActiveSheet.DisplayPageBreaks = False
    ActiveSheet.UsedRange.EntireRow.Hidden = False
    
    If Err.Number <> 0 Then
       Err.Clear
    End If     * 
                        */
        }

        private struct exCellAddress
        {
            public int Column;
            public int Row;
        }

        private static exCellAddress vCurrentCellAddress = new exCellAddress();

        /*    public void  execVbaCode(string vCurrVbaProcedure, ref Office.IRibbonControl vControl, String vSelectedValue)
            {
                try
                {
                    if (String.Equals("QQ", vSelectedValue))
                    {
                        In2SqlAddIn.currExcelApp.Run(vCurrVbaProcedure, vControl, Type.Missing
                        , Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    );
                    }
                    else
                    {
                        In2SqlAddIn.currExcelApp.Run(vCurrVbaProcedure, vControl.Id, vSelectedValue
                         , Type.Missing, Type.Missing, Type.Missing,
                         Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                         Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                         Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                         Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                         Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                     );

                    }
                }
                catch (Exception ex)
                {
                    //  System.Windows.Forms.MessageBox.Show(vCurrVbaProcedure);
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }

            }
        */

        private static void getCurrentExCellAddress()
        {
            Excel.Range rng = (Excel.Range)SqlEngine.currExcelApp.ActiveCell;

            //get the row and column details
            vCurrentCellAddress.Row = rng.Row;
            vCurrentCellAddress.Column = rng.Column;
        }

        public static void checkTableName()
        {
            var vCurrWorkSheet = SqlEngine.currExcelApp.ActiveSheet;
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;

            getCurrentExCellAddress();
            if (vCurrWorkSheet != null)
                System.Windows.Forms.MessageBox.Show(vActivCell.ListObject.QueryTable.CommandText);
        }

        public static string getCurrentBookName()
        {
            return SqlEngine.currExcelApp.ActiveWorkbook.Name;
        }


        public static string getOdbcNameFromCell()
        {
            return getOdbcNameFromObject(SqlEngine.currExcelApp.ActiveCell.ListObject.QueryTable.Connection);

        }

        public static string getOdbcNameFromObject(Microsoft.Office.Interop.Excel.ListObject vCurrObject)
        {
            try
            {
                return getOdbcNameFromObject(vCurrObject.QueryTable.Connection);
            }
            catch
            {
                return null;
            }

        }


        public static string getOdbcNameFromObject(String vDsnSTR)
        {
            string[] vConnStr = vDsnSTR.Split(';');
            return vConnStr[1].Replace("DSN=", "");
        }


        //createPowerQuery


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

        public static string setSqlLimit(string vODBC, string vCurrSql)
        {

            string vSql = RemoveSqlLimit(vCurrSql);

            string vTypeODBC = sODBC.getODBCProperties(vODBC, "DBType");
            if (vTypeODBC.ToUpper().Contains("VERTICA"))
            {
                if (vSql.ToUpper().Contains("LIMIT") == false)
                    vSql = vSql + Environment.NewLine + "/*`*/ LIMIT " + Ribbon.vRowCount + " /*`*/ ";
            }
            else if (vTypeODBC.ToUpper().Contains("MSSQL"))
            {
                if (vSql.ToUpper().Contains("TOP") == false)
                {
                    vSql = vSql.Replace("select", "SELECT");
                    vSql = vSql.Replace("Select", "SELECT");

                    Regex rgx = new Regex("SELECT");
                    vSql = rgx.Replace(vSql, "SELECT /*`*/ TOP(" + Ribbon.vRowCount + ") /*`*/ ", 1);
                }
            }

            return vSql;
        }

        private static string prepareSQL(string vODBC, string vTableName, string vCurrSql = null, int isPivot = 0)
        {
            string vSql;
            vSql = vCurrSql;

            if (vSql == null)
                vSql = " select  " + Environment.NewLine + "  *      from " + vTableName + Environment.NewLine + "   where 1=1  ";
            else
            {
                if (vCurrSql.ToUpper().Contains("WITH") == false)
                    vSql = " select   *  from " + Environment.NewLine + "( " + vCurrSql + " )  dd " + Environment.NewLine + "  where 1=1  ";
            }

            if (isPivot == 1)
                vSql = setSqlLimit(vODBC, vSql);

            return vSql;
        }


        public static void createPivotTable(string vODBC, string vTableName, string vSql = "")
        {
            var vCurrWorkSheet = SqlEngine.currExcelApp.ActiveSheet;
            var vCurrWorkBook = SqlEngine.currExcelApp.ActiveWorkbook;
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;

            string vDSN;
            vDSN = "ODBC;" + sODBC.getODBCProperties(vODBC, "DSNStr");


            if (vCurrWorkSheet != null & vDSN.Length > 1 & vTableName.Length > 1)
            {
                var vDes3 = vCurrWorkSheet.Cells(vActivCell.Row, vActivCell.Column);

                if ((vDes3.Value == null) == false)
                {
                    var xlNewSheet = vCurrWorkBook.Worksheets.Add();
                    vDes3 = xlNewSheet.Cells[1, 1];
                }
                if (vSql == "")
                    vSql = prepareSQL(vODBC, vTableName, null, 1);

                var connections = vCurrWorkBook.Connections.Add2(
                                                   Name: "cn " + vTableName
                                                 , Description: vSql
                                                 , ConnectionString: vDSN
                                                 , CommandText: vSql
                                                 , lCmdtype: Excel.XlCmdType.xlCmdSql);
                var vQT = vCurrWorkBook.PivotCaches().Create(
                                          SourceType: Excel.XlPivotTableSourceType.xlExternal
                                        , SourceData: connections
                                        , Version: 6).CreatePivotTable(
                                                         TableDestination: vDes3
                                                       , TableName: "pvt " + vTableName
                                                       , ReadData: false
                                                       , DefaultVersion: 6);
                vQT.Name = vODBC + " " + vTableName;
                sTool.addSqlLog(vSql);
                vQT.RefreshTable();
                GetSelectedTab();
                return;
            }
            System.Windows.Forms.MessageBox.Show(" Please select empty area  in Excel data grid");
        }


        public static void createExTable(string vODBC, string vTableName, string vCurrSql = null)
        {
            var vCurrWorkSheet = SqlEngine.currExcelApp.ActiveSheet;
            var vCurrWorkBook = SqlEngine.currExcelApp.ActiveWorkbook;
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;

            SqlEngine.currExcelApp.SheetChange += CurrExcelApp_SheetChange;

            string vDSN;
            vDSN = "ODBC;" + sODBC.getODBCProperties(vODBC, "DSNStr");

            if (vActivCell != null & vDSN.Length > 1 & vTableName.Length > 1) {
                if (vActivCell.Value == null)
                    if (vActivCell.ListObject == null)
                    {
                        string vSql = prepareSQL(vODBC, vTableName, vCurrSql);

                        var connections = vCurrWorkBook.Connections.Add(
                                                           Name: "In2Sql|" + vODBC + "|" + vTableName
                                                         , Description: "ODBC|" + vODBC + "|" + vSql  
                                                         , ConnectionString: vDSN
                                                         , CommandText: vSql
                                                         , lCmdtype: Excel.XlCmdType.xlCmdSql);
                        Microsoft.Office.Interop.Excel.ListObject table = vCurrWorkSheet.ListObjects.Add(
                                                 SourceType: Excel.XlListObjectSourceType.xlSrcQuery
                                               , Source: connections
                                               , Destination: vCurrWorkSheet.Cells(vActivCell.Row, vActivCell.Column));

                        table.Name = "In2Sql|" + vODBC + "|" + vTableName;
                        table.Comment = vTableName; 
                        objRefreshHistory(table);
                        GetSelectedTab();
                        return;
                    }
            }
            System.Windows.Forms.MessageBox.Show(" Please select empty area  in Excel data grid");
        }

       

        private static void CurrExcelApp_SheetChange(object Sh, Excel.Range vRange)
        {
            var vCurrWorkSheet = SqlEngine.currExcelApp.ActiveSheet;

            if ((isRefresh == false) & ((vRange.ListObject == null) == false))
                if (vRange.ListObject.Name.Contains("In2Sql"))
                {
                    foreach (Range vChangedCell in vRange.Cells)
                    {
                        isRefresh = true;
                        vChangedCell.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;//Excel.XlThemeColor.xlThemeColorAccent6;
                        isRefresh = false;

                        string vOdbc = getOdbcNameFromObject(vChangedCell.ListObject);
                        string vTrgtColumnName = vCurrWorkSheet.Cells(vChangedCell.ListObject.Range.Row, vChangedCell.Column).Value;

                        string vSql = "UPDATE " + vChangedCell.ListObject.Comment + Environment.NewLine
                                     + " SET \"" + vTrgtColumnName + "\"" + " =  '" + vChangedCell.Value + "'"
                                     + Environment.NewLine + " WHERE 1=1 ";

                        for (int i = 1; i < vChangedCell.ListObject.ListColumns.Count + 1; i++)
                        {
                            string vCurrClmName = vChangedCell.ListObject.ListColumns[i].Name;

                            if ((vCurrClmName.Equals(vTrgtColumnName) == false) & (vCurrClmName.ToUpper().Contains("DATE") == false))
                            {
                                vSql = vSql + Environment.NewLine
                                     + " and \"" + vCurrClmName + "\" = "
                                     + "'" + vCurrWorkSheet.Cells(vChangedCell.Row, vChangedCell.ListObject.QueryTable.Destination.Column + i - 1).Value + "'";
                            }
                        }
                        addToInsertList(vOdbc, vSql);
                        //  System.Windows.Forms.MessageBox.Show(vSql);
                    }

                }

            // throw new NotImplementedException();
        }

        public static void updateTablesAll()
        {
            for (int i = 0; i < vInsertList.Count; i++)
                updateTables(vInsertList[i].DSNName);
        }


        public static void updateTables(string vDNS = "")
        {
            sTool.CurrentTableRecords vCTR = sTool.getCurrentSql();

            if (vCTR.TypeConnection.Contains("CLOUD"))
            {
                System.Windows.Forms.MessageBox.Show("Update cloud is not support");
            }


            if (vDNS == "")
                vDNS = getOdbcNameFromCell();

            int vId = vInsertList.FindIndex(item => item.DSNName == vDNS);
            if (vId < 0)
                return;
            int vRecCount = 0;
            using (OdbcConnection conn = new OdbcConnection(sODBC.getODBCProperties(vInsertList[vId].DSNName, "DSNStr")))
            {
                conn.ConnectionTimeout = 5;
                conn.Open();

                foreach (var vInsert in vInsertList[vId].SqlUpdate)
                {
                    vRecCount = vRecCount + 1;
                    if ((vInsert == "") == false)
                    {
                        sTool.addSqlLog(conn.ToString(), vInsert);
                        using (OdbcCommand cmnd = new OdbcCommand(vInsert, conn))
                            try
                            {
                                isRefresh = true;
                                cmnd.ExecuteNonQuery();
                            }
                            catch (Exception e)
                            {
                                System.Windows.Forms.MessageBox.Show(e.Message);
                            }
                    }
                }
                vInsertList[vId].SqlUpdate.RemoveRange(0, vInsertList[vId].SqlUpdate.Count);
                deleteUpdateList(vId);
            }

            MessageBox.Show(" updated records: " + vRecCount, " update count r");

        }

        private static void deleteUpdateList(int vId = -1)
        {
            if (vId < 0)
                vId = vInsertList.FindIndex(item => item.DSNName == getOdbcNameFromCell());

            if (vId < 0)
                return;

            vInsertList[vId].SqlUpdate.RemoveRange(0, vInsertList[vId].SqlUpdate.Count);
        }

        private static void QueryTable_AfterRefresh(bool Success)
        {
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;
            isRefresh = true; 
          
            if ((vActivCell.ListObject == null) == false)
            {
                for (int i = 1; i < vActivCell.ListObject.ListColumns.Count + 1; i++)
                {
                    string vClmName = vActivCell.ListObject.ListColumns[i].Name;
                    vClmName = vClmName.ToUpper();
                    if (vClmName.Contains("DATE"))
                    {
                        isRefresh = true;
                        vActivCell.ListObject.ListColumns[i].Range.NumberFormat = "yyyy.mm.dd hh:mm:ss";
                    }
                }  
                vActivCell.ListObject.Range.Interior.Color = XlRgbColor.rgbWhite;
                vActivCell.ListObject.HeaderRowRange.Interior.Color = XlRgbColor.rgbSkyBlue;

                deleteUpdateList();
            }
            isRefresh = false;
            GetSelectedTab();
        }


        public static void RibbonKeepOnly()
        {
            var vCurrWorkSheet = SqlEngine.currExcelApp.ActiveSheet;
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;

            sTool.CurrentTableRecords vCTR = sTool.getCurrentSql();

            if (vActivCell != null)

            { 
                vCTR.Sql = vCTR.Sql + Environment.NewLine
                                + " \t and " + vCurrWorkSheet.Cells(vActivCell.ListObject.Range.Row, vActivCell.Column).Value
                                + "= '" + vActivCell.Value + "'";

                tableRefresh(vCTR);

                //  SqlEngine.currExcelApp.SendKeys("%YQA%");
                return;
            }
            GetSelectedTab();
        }

        public static void RibbonRemoveOnly()
        {

            var vCurrWorkSheet = SqlEngine.currExcelApp.ActiveSheet;
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;

            sTool.CurrentTableRecords vCTR = sTool.getCurrentSql();

            if (vActivCell != null)
            { 

                vCTR.Sql = vCTR.Sql + Environment.NewLine
                                + " \t and " + vCurrWorkSheet.Cells(vActivCell.ListObject.Range.Row, vActivCell.Column).Value
                                + " <> '" + vActivCell.Value + "'";

                tableRefresh(vCTR);

                return;
            }
            GetSelectedTab();
        }

        public static void RibbonRefreshAll()
        {
            try
            {
                vInsertList = new List<InsertList>();

                var vCurrWorkBook = SqlEngine.currExcelApp.ActiveWorkbook;
                foreach (Microsoft.Office.Interop.Excel.Worksheet vCurrWorkSheet in vCurrWorkBook.Sheets)
                {
                    foreach (Microsoft.Office.Interop.Excel.ListObject vTable in vCurrWorkSheet.ListObjects)
                    {
                        sTool.CurrentTableRecords vCTR = sTool.getCurrentSql();
                        if (vCTR.TypeConnection.Contains("ODBC"))
                        {
                            objRefreshHistory(vTable);
                            continue;
                        }

                        if (vCTR.TypeConnection.Contains("CLOUD"))
                        {
                            sVbaEngineCloud.createExTable(
                                             vCTR.CurrCloudName
                                           , vCTR.TableName
                                           , vCTR.Sql
                                           , 1
                                           , vCTR.CurrCloudExTName);
                            isRefresh = false;
                            return;

                        }

                    }
                    foreach (var vTable in vCurrWorkSheet.PivotTables())
                    {
                        vTable.RefreshTable();
                    }
                }
            }
            catch {
            }
            GetSelectedTab();
        }

        public static void objRefreshHistory(Microsoft.Office.Interop.Excel.ListObject vCurrObject, int vIsUndoList= 1 )
        {
            // SqlEngine.currExcelApp.EnableEvents = false;
            vCurrObject.QueryTable.CommandText = setSqlLimit(getOdbcNameFromObject(vCurrObject.QueryTable.Connection), vCurrObject.QueryTable.CommandText);
           
                sTool.addSqlLog(vCurrObject.QueryTable.CommandText);

            objRefresh(vCurrObject);
            if (vIsUndoList == 1)
                sUndo.addToUndoList(vCurrObject.Name, vCurrObject.QueryTable.CommandText);

        }

        public static void objRefresh(Microsoft.Office.Interop.Excel.ListObject vCurrObject)
        {
            isRefresh = true;
            vCurrObject.QueryTable.AfterRefresh += QueryTable_AfterRefresh;            
            vCurrObject.Refresh();
            vCurrObject.TableStyle = "TableStyleLight13";
        }

        public static void tableRefresh(sTool.CurrentTableRecords vCTR, int vIsUndoList = 1)
        {
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;

            if (vCTR.TypeConnection.Contains("ODBC"))
            {
                vActivCell.ListObject.QueryTable.CommandText = vCTR.Sql;
                objRefreshHistory(vActivCell.ListObject, vIsUndoList);

                if (vIsUndoList == 1)
                    sUndo.addToUndoList(vActivCell.ListObject.Name, vCTR.Sql);

            }

            if (vCTR.TypeConnection.Contains("CLOUD"))
            {
                sVbaEngineCloud.createExTable(
                                                     vCTR.CurrCloudName
                                                   , vCTR.TableName
                                                   , vCTR.Sql
                                                   , 1
                                                   , vCTR.CurrCloudExTName);

                sTool.addSqlLog(vCTR.Sql);
                if (vIsUndoList == 1)
                    sUndo.addToUndoList(vCTR.CurrCloudExTName, vCTR.Sql);
            }

        }

        public static void Undo()
        {
            try
            {               
                var vActivCell = SqlEngine.currExcelApp.ActiveCell;
                if (vActivCell.ListObject == null)
                {
                    MessageBox.Show(" Please,  select cell from the table", " Refresh error");
                    return;
                }

                sTool.CurrentTableRecords vCTR = sTool.getCurrentSql();

                string vSql = sUndo.getLastSqlActionUndo(vActivCell.ListObject.Name);
                    if ((vSql == null) == false)
                    {
                    vCTR.Sql = vSql;

                    tableRefresh(vCTR,0);

                }

                    GetSelectedTab();
                  
                    return;
               
            }
            catch
            {
                MessageBox.Show(" Please, select cell from the table", " Refresh error");
            }
            
        }

        public static void Redo()
        {
            try
            {               
                var vActivCell = SqlEngine.currExcelApp.ActiveCell;
                if ((vActivCell.ListObject == null) == false)
                {
                    sTool.CurrentTableRecords vCTR = sTool.getCurrentSql();

                    string vSql = sUndo.getLastSqlActionRedo(vActivCell.ListObject.Name);
                    if ((vSql == null) == false)
                    {
                        vCTR.Sql = vSql;

                        tableRefresh(vCTR,0);
                    }
                    GetSelectedTab();
                    
                    return;
                }
                MessageBox.Show(" Please,  select cell from the table", " Refresh error");
            }
            catch
            {
                MessageBox.Show(" Please, select cell from the table", " Refresh error");
            }
            GetSelectedTab();
        }



        public static void RibbonRefresh()
        { try
            { //eeee
                isRefresh = true;

                var vActivCell = SqlEngine.currExcelApp.ActiveCell;                

                sTool.CurrentTableRecords vCTR = sTool.getCurrentSql();

                if (vCTR.TypeConnection.Contains("ODBC"))
                {
                    

                    if ((vActivCell.ListObject != null) )
                    {
                        objRefreshHistory(vActivCell.ListObject);
                        isRefresh = false;
                        return;
                    }

                    if ((vActivCell.PivotTable != null) )
                    {
                        vActivCell.PivotTable.RefreshTable();
                        isRefresh = false;
                        return;
                    }
                }

                if (vCTR.TypeConnection.Contains("CLOUD"))
                {
                    sVbaEngineCloud.createExTable(
                                     vCTR.CurrCloudName
                                   , vCTR.TableName
                                   , vCTR.Sql
                                   , 1
                                   , vCTR.CurrCloudExTName);
                    isRefresh = false;
                    return;

                }

                isRefresh = false;
                MessageBox.Show(" Please, select cell from the table", " Refresh error");
            }
            catch  { 
            isRefresh = false;
            MessageBox.Show(" Please, select cell from the table", " Refresh error");
            }
            
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
                createPivotTable(getOdbcNameFromCell(), sTool.GetHash(vSql), vSql);
            }

            if (vCTR.TypeConnection.Contains("CLOUD"))
            {
                SqlEngine.currExcelApp.SendKeys("%NVT");
            }

        

            GetSelectedTab();

        }

        public static void runTableProperties()
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

        public static void runSqlProperties()
        {
            var vActivCell = SqlEngine.currExcelApp.ActiveCell;
            // SqlEngine.currExcelApp.CommandBars.ExecuteMso("EditQuery");
            if ((vActivCell.ListObject == null) == false)
                SqlEngine.currExcelApp.SendKeys("%j%f%o");
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
             SqlEngine.currExcelApp.SendKeys("%Y%Q%A");
             SqlEngine.currExcelApp.SendKeys("%");
             SqlEngine.currExcelApp.CommandBars.ReleaseFocus();//CommandBars.ReleaseFocus 
             
            SqlEngine.currExcelApp.ScreenUpdating = true;
        }
        //

    

  

    }
    }
