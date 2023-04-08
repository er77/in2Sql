using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Data.Odbc;
namespace SqlEngine
{
    class IntSqlVbaEngine
    {
        private static bool _isRefresh ;
        // intSqlVBAEngine.isRefresh = true;

        //Microsoft.Office.Interop.Excel.ListObject
        public struct InsertList
        { public List<string> SqlUpdate;
            public string DsnName;
        }

        public static List<InsertList> VInsertList = new List<InsertList>();

        public static InsertList NewInsertRecord(String vDsnName, string vDdl)
        {
            InsertList vnewRecord;
            vnewRecord.DsnName = vDsnName;
            vnewRecord.SqlUpdate = new List<String>
                {
                    vDdl
                };
            return vnewRecord;
        }


        public static void AddToInsertList(string vDsnName, string vDdl)
        {

            if (VInsertList.Count < 0)
            {
                VInsertList.Add(NewInsertRecord(vDsnName, vDdl));
                return;
            }

            int vIntInsetId = VInsertList.FindIndex(item => item.DsnName == vDsnName);
            if (vIntInsetId < 0)
            {
                VInsertList.Add(NewInsertRecord(vDsnName, vDdl));
                return;
            }
            VInsertList[vIntInsetId].SqlUpdate.Add(vDdl);
        }

        public void SetExcelCalcOff()
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

        private struct ExCellAddress
        {
            public int Column;
            public int Row;
        }

        private static ExCellAddress _vCurrentCurrentCellAddress ;

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

        private static void GetCurrentExCellAddress()
        {
            var rng = SqlEngine.CurrExcelApp.ActiveCell;

            //get the row and column details
            _vCurrentCurrentCellAddress.Row = rng.Row;
            _vCurrentCurrentCellAddress.Column = rng.Column;
        }

        public static void CheckTableName()
        {
            var vCurrWorkSheet = SqlEngine.CurrExcelApp.ActiveSheet;
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;

            GetCurrentExCellAddress();
            if (vCurrWorkSheet != null)
                MessageBox.Show(activeCell.ListObject.QueryTable.CommandText);
        }

        public static string GetCurrentBookName()
        {
            return SqlEngine.CurrExcelApp.ActiveWorkbook.Name;
        }


        public static string GetOdbcNameFromCell()
        {
            return GetOdbcNameFromObject((ListObject)SqlEngine.CurrExcelApp.ActiveCell.ListObject.QueryTable.Connection);

        }

        public static string GetOdbcNameFromObject( ListObject vCurrObject)
        {
            try
            {
                return GetOdbcNameFromObject((ListObject)vCurrObject.QueryTable.Connection);
            }
            catch
            {
                return null;
            }

        }


        public static string GetOdbcNameFromObject(String vDsnStr)
        {
            string[] vConnStr = vDsnStr.Split(';');
            return vConnStr[1].Replace("DSN=", "");
        }


        //createPowerQuery


        public static string RemoveBetween(string s, char begin, char end)
        {
            var regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return regex.Replace(s, string.Empty);
        }

        public static string RemoveSqlLimit(string vCurrSql)
        {
            var vSql = RemoveBetween(vCurrSql, '`', '`');
            vSql = vSql.Replace("/**/", "");
            vSql = vSql.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);

            return vSql;
        }

        public static string SetSqlLimit(string vOdbc, string vCurrSql)
        {

            var vSql = RemoveSqlLimit(vCurrSql);

            var vTypeOdbc = SOdbc.GetOdbcProperties(vOdbc, "DBType");
            if (vTypeOdbc.ToUpper().Contains("VERTICA"))
            {
                if (vSql.ToUpper().Contains("LIMIT") == false)
                    vSql = vSql + Environment.NewLine + "/*`*/ LIMIT " + Ribbon.vRowCount + " /*`*/ ";
            }
            else if (vTypeOdbc.ToUpper().Contains("MSSQL"))
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

        private static string PrepareSql(string vOdbc, string vTableName, string vCurrSql = null, int isPivot = 0)
        {
            string setSqlLimit;
            setSqlLimit = vCurrSql;

            if (setSqlLimit == null)
                setSqlLimit = " select  " + Environment.NewLine + "  *      from " + vTableName + Environment.NewLine + "   where 1=1  ";
            else
            {
                if (vCurrSql.ToUpper().Contains("WITH") == false)
                    setSqlLimit = " select   *  from " + Environment.NewLine + "( " + vCurrSql + " )  dd " + Environment.NewLine + "  where 1=1  ";
            }

            if (isPivot == 1)
                setSqlLimit = SetSqlLimit(vOdbc, setSqlLimit);

            return setSqlLimit;
        }


        public static void CreatePivotTable(string vOdbc, string vTableName, string vSql = "")
        {
            var vCurrWorkSheet = SqlEngine.CurrExcelApp.ActiveSheet;
            var vCurrWorkBook = SqlEngine.CurrExcelApp.ActiveWorkbook;
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
            var vDsn = "ODBC;" + SOdbc.GetOdbcProperties(vOdbc, "DSNStr");
            if (vCurrWorkSheet != null & vDsn.Length > 1 & vTableName.Length > 1)
            {
                if (vCurrWorkSheet != null)
                {
                    var vDes3 = vCurrWorkSheet.Cells(activeCell.Row, activeCell.Column);

                    if ((vDes3.Value == null) == false)
                    {
                        var xlNewSheet = vCurrWorkBook.Worksheets.Add();
                        vDes3 = xlNewSheet.Cells[1, 1];
                    }
                    if (vSql == "")
                        vSql = PrepareSql(vOdbc, vTableName, null, 1);

                    var connections = vCurrWorkBook.Connections.Add2(
                        Name: "cn " + vTableName
                        , Description: vSql
                        , ConnectionString: vDsn
                        , CommandText: vSql
                        , lCmdtype: XlCmdType.xlCmdSql);
                    var vQt = vCurrWorkBook.PivotCaches().Create(
                        SourceType: XlPivotTableSourceType.xlExternal
                        , SourceData: connections
                        , Version: 6).CreatePivotTable(
                        TableDestination: vDes3
                        , TableName: "pvt " + vTableName
                        , ReadData: false
                        , DefaultVersion: 6);
                    vQt.Name = vOdbc + " " + vTableName;
                    STool.AddSqlLog(vSql);
                    vQt.RefreshTable();
                }

                GetSelectedTab();
                return;
            }
            MessageBox.Show(@" Please select empty area  in Excel data grid");
        }


        public static void CreateExTable(string vOdbc, string vTableName, string vCurrSql = null)
        {
            var currWorkSheet = SqlEngine.CurrExcelApp.ActiveSheet;
            var currWorkBook = SqlEngine.CurrExcelApp.ActiveWorkbook;
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;

            SqlEngine.CurrExcelApp.SheetChange += CurrExcelApp_SheetChange;

            string vDsn;
            vDsn = "ODBC;" + SOdbc.GetOdbcProperties(vOdbc, "DSNStr");

            if (activeCell != null & vDsn.Length > 1 & vTableName.Length > 1) {
                if (activeCell.Value == null)
                    if (activeCell.ListObject == null)
                    {
                        string vSql = PrepareSql(vOdbc, vTableName, vCurrSql);

                        var connections = currWorkBook.Connections.Add(
                                                           Name: "In2Sql|" + vOdbc + "|" + vTableName
                                                         , Description: "ODBC|" + vOdbc + "|" + vSql  
                                                         , ConnectionString: vDsn
                                                         , CommandText: vSql
                                                         , lCmdtype: XlCmdType.xlCmdSql);
                        var table = currWorkSheet.ListObjects.Add(
                                                 SourceType: XlListObjectSourceType.xlSrcQuery
                                               , Source: connections
                                               , Destination: currWorkSheet.Cells(activeCell.Row, activeCell.Column));

                        table.Name = "In2Sql|" + vOdbc + "|" + vTableName;
                        table.Comment = vTableName; 
                        ObjRefreshHistory(table);
                        GetSelectedTab();
                        return;
                    }
            }
           MessageBox.Show(@" Please select empty area  in Excel data grid");
        }

        private static void CurrExcelApp_SheetChange(object sh, Range vRange)
        {
            var vCurrWorkSheet = SqlEngine.CurrExcelApp.ActiveSheet;
            if ((_isRefresh == false) & ((vRange.ListObject == null) == false))
                if (vRange.ListObject.Name.Contains("In2Sql"))
                {
                    foreach (Range vChangedCell in vRange.Cells)
                    {
                        _isRefresh = true;
                        vChangedCell.Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;//Excel.XlThemeColor.xlThemeColorAccent6;
                        _isRefresh = false;

                        var vOdbc = GetOdbcNameFromObject(vChangedCell.ListObject);
                        var vTrgtColumnName = vCurrWorkSheet.Cells(vChangedCell.ListObject.Range.Row, vChangedCell.Column).Value;

                        var vSql = "UPDATE " + vChangedCell.ListObject.Comment + Environment.NewLine
                                     + " SET \"" + vTrgtColumnName + "\"" + " =  '" + vChangedCell.Value + "'"
                                     + Environment.NewLine + " WHERE 1=1 ";

                        for (var i = 1; i < vChangedCell.ListObject.ListColumns.Count + 1; i++)
                        {
                            var vCurrClmName = vChangedCell.ListObject.ListColumns[i].Name;

                            if ((vCurrClmName.Equals(vTrgtColumnName) == false) & (vCurrClmName.ToUpper().Contains("DATE") == false))
                            {
                                vSql = vSql + Environment.NewLine
                                     + " and \"" + vCurrClmName + "\" = "
                                     + "'" + vCurrWorkSheet.Cells(vChangedCell.Row, vChangedCell.ListObject.QueryTable.Destination.Column + i - 1).Value + "'";
                            }
                        }
                        AddToInsertList(vOdbc, vSql);
                        //  System.Windows.Forms.MessageBox.Show(vSql);
                    }

                }

            // throw new NotImplementedException();
        }

        public static void UpdateTablesAll()
        {
            for (int i = 0; i < VInsertList.Count; i++)
                UpdateTables(VInsertList[i].DsnName);
        }


        public static void UpdateTables(string vDns = "")
        {
            STool.CurrentTableRecords vCtr = STool.GetCurrentSql();
            
            if (vCtr.TypeConnection.Contains("CLOUD"))
            {
                MessageBox.Show(@"Update cloud is not support");
            }
            
            if (vDns == "")
                vDns = GetOdbcNameFromCell();

            var vId = VInsertList.FindIndex(item => item.DsnName == vDns);
            if (vId < 0)
                return;
            var vRecCount = 0;
            using (OdbcConnection conn = new OdbcConnection(SOdbc.GetOdbcProperties(VInsertList[vId].DsnName, "DSNStr")))
            {
                conn.ConnectionTimeout = 5;
                conn.Open();

                foreach (var vInsert in VInsertList[vId].SqlUpdate)
                {
                    vRecCount = vRecCount + 1;
                    if ((vInsert == "") == false)
                    {
                        STool.AddSqlLog(conn.ToString(), vInsert);
                        using (OdbcCommand cmnd = new OdbcCommand(vInsert, conn))
                            try
                            {
                                _isRefresh = true;
                                cmnd.ExecuteNonQuery();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                    }
                }
                VInsertList[vId].SqlUpdate.RemoveRange(0, VInsertList[vId].SqlUpdate.Count);
                DeleteUpdateList(vId);
            }

            MessageBox.Show(@" updated records: " + vRecCount, @" update count r");

        }

        private static void DeleteUpdateList(int vId = -1)
        {
            if (vId < 0)
                vId = VInsertList.FindIndex(item => item.DsnName == GetOdbcNameFromCell());

            if (vId < 0)
                return;

            VInsertList[vId].SqlUpdate.RemoveRange(0, VInsertList[vId].SqlUpdate.Count);
        }

        private static void QueryTable_AfterRefresh(bool success)
        {
            var vActivCell = SqlEngine.CurrExcelApp.ActiveCell;
            _isRefresh = true; 
          
            if ((vActivCell.ListObject == null) == false)
            {
                for (int i = 1; i < vActivCell.ListObject.ListColumns.Count + 1; i++)
                {
                    var vClmName = vActivCell.ListObject.ListColumns[i].Name;
                    vClmName = vClmName.ToUpper();
                    if (vClmName.Contains("DATE"))
                    {
                        _isRefresh = true;
                        vActivCell.ListObject.ListColumns[i].Range.NumberFormat = "yyyy.mm.dd hh:mm:ss";
                    }
                }  
                vActivCell.ListObject.Range.Interior.Color = XlRgbColor.rgbWhite;
                vActivCell.ListObject.HeaderRowRange.Interior.Color = XlRgbColor.rgbSkyBlue;

                DeleteUpdateList();
            }
            _isRefresh = false;
            GetSelectedTab();
        }


        public static void RibbonKeepOnly()
        {
            var vCurrWorkSheet = SqlEngine.CurrExcelApp.ActiveSheet;
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
            var vCtr = STool.GetCurrentSql();
            if (activeCell != null)
            { 
                vCtr.Sql = vCtr.Sql + Environment.NewLine
                                + " \t and " + vCurrWorkSheet.Cells(activeCell.ListObject.Range.Row, activeCell.Column).Value
                                + "= '" + activeCell.Value + "'";
                TableRefresh(vCtr); 
                return;
            }
            GetSelectedTab();
        }

        public static void RibbonRemoveOnly()
        {
            var vCurrWorkSheet = SqlEngine.CurrExcelApp.ActiveSheet;
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
            var vCtr = STool.GetCurrentSql();
            if (activeCell != null)
            {
                vCtr.Sql = vCtr.Sql + Environment.NewLine
                                    + " \t and " + vCurrWorkSheet.Cells(activeCell.ListObject.Range.Row, activeCell.Column).Value
                                    + " <> '" + activeCell.Value + "'";
                TableRefresh(vCtr);
                return;
            }
            GetSelectedTab();
        }

        public static void RibbonRefreshAll()
        {
            try
            {
                VInsertList = new List<InsertList>();

                var vCurrWorkBook = SqlEngine.CurrExcelApp.ActiveWorkbook;
                foreach (Worksheet vCurrWorkSheet in vCurrWorkBook.Sheets)
                {
                    foreach (ListObject vTable in vCurrWorkSheet.ListObjects)
                    {
                        var vCtr = STool.GetCurrentSql();
                        if (vCtr.TypeConnection.Contains("ODBC"))
                        {
                            ObjRefreshHistory(vTable);
                            continue;
                        }

                        if (vCtr.TypeConnection.Contains("CLOUD"))
                        {
                            SVbaEngineCloud.CreateExTable(
                                             vCtr.CurrCloudName
                                           , vCtr.TableName
                                           , vCtr.Sql
                                           , 1
                                           , vCtr.CurrCloudExTName);
                            _isRefresh = false;
                            return;

                        }

                    }
                    foreach (var vTable in vCurrWorkSheet.PivotTables())
                    {
                        vTable.RefreshTable();
                    }
                }
            }
            catch
            {
                return;
            }
            GetSelectedTab();
        }

        public static void ObjRefreshHistory(ListObject vCurrObject, int vIsUndoList= 1 )
        {
            // SqlEngine.currExcelApp.EnableEvents = false;
            vCurrObject.QueryTable.CommandText = SetSqlLimit(GetOdbcNameFromObject((ListObject)vCurrObject.QueryTable.Connection), vCurrObject.QueryTable.CommandText);
           
                STool.AddSqlLog(vCurrObject.QueryTable.CommandText);

            ObjRefresh(vCurrObject);
            if (vIsUndoList == 1)
                SUndo.AddToUndoList(vCurrObject.Name, vCurrObject.QueryTable.CommandText);

        }

        public static void ObjRefresh(ListObject vCurrObject)
        {
            _isRefresh = true;
            vCurrObject.QueryTable.AfterRefresh += QueryTable_AfterRefresh;            
            vCurrObject.Refresh();
            vCurrObject.TableStyle = "TableStyleLight13";
        }

        public static void TableRefresh(STool.CurrentTableRecords vCtr, int vIsUndoList = 1)
        {
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;

            if (vCtr.TypeConnection.Contains("ODBC"))
            {
                activeCell.ListObject.QueryTable.CommandText = vCtr.Sql;
                ObjRefreshHistory(activeCell.ListObject, vIsUndoList);

                if (vIsUndoList == 1)
                    SUndo.AddToUndoList(activeCell.ListObject.Name, vCtr.Sql);

            }

            if (vCtr.TypeConnection.Contains("CLOUD"))
            {
                SVbaEngineCloud.CreateExTable(
                                                     vCtr.CurrCloudName
                                                   , vCtr.TableName
                                                   , vCtr.Sql
                                                   , 1
                                                   , vCtr.CurrCloudExTName);
                STool.AddSqlLog(vCtr.Sql);
                if (vIsUndoList == 1)
                    SUndo.AddToUndoList(vCtr.CurrCloudExTName, vCtr.Sql);
            }
        }

        public static void Undo()
        {
            try
            {               
                var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
                if (activeCell.ListObject == null)
                {
                    MessageBox.Show(@" Please,  select cell from the table", @" Refresh error");
                    return;
                }

                var vCtr = STool.GetCurrentSql();

                var vSql = SUndo.GetLastSqlActionUndo(activeCell.ListObject.Name);
                    if ((vSql == null) == false)
                    {
                    vCtr.Sql = vSql;

                    TableRefresh(vCtr,0);

                }
                    GetSelectedTab();
            }
            catch
            {
                MessageBox.Show(@" Please, select cell from the table", @" Refresh error");
            }
            
        }

        public static void Redo()
        {
            try
            {              
                var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
                if ((activeCell.ListObject == null) == false)
                {
                    var vCtr = STool.GetCurrentSql();
                    string vSql = SUndo.GetLastSqlActionRedo(activeCell.ListObject.Name);
                    if ((vSql == null) == false)
                    {
                        vCtr.Sql = vSql;
                        TableRefresh(vCtr,0);
                    }
                    GetSelectedTab();
                    return;
                }
                MessageBox.Show(@" Please,  select cell from the table", @" Refresh error");
            }
            catch
            {
                MessageBox.Show(@" Please, select cell from the table", @" Refresh error");
            }
            GetSelectedTab();
        }



        public static void RibbonRefresh()
        { try
            { //eeee
                _isRefresh = true;
                var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
                var vCtr = STool.GetCurrentSql();
                if (vCtr.TypeConnection.Contains("ODBC"))
                {
                    if ((activeCell.ListObject != null) )
                    {
                        ObjRefreshHistory(activeCell.ListObject);
                        _isRefresh = false;
                        return;
                    }
                    if ((activeCell.PivotTable != null) )
                    {
                        activeCell.PivotTable.RefreshTable();
                        _isRefresh = false;
                        return;
                    }
                }

                if (vCtr.TypeConnection.Contains("CLOUD"))
                {
                    SVbaEngineCloud.CreateExTable(
                                     vCtr.CurrCloudName
                                   , vCtr.TableName
                                   , vCtr.Sql
                                   , 1
                                   , vCtr.CurrCloudExTName);
                    _isRefresh = false;
                    return;

                }
                _isRefresh = false;
                MessageBox.Show(@" Please, select cell from the table", @" Refresh error");
            }
            catch  { 
            _isRefresh = false;
            MessageBox.Show(@" Please, select cell from the table", @" Refresh error");
            }
            
        }

        public static void RibbonPivotExcel()
        {
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
            if (activeCell.ListObject == null)
            {
                MessageBox.Show(@" Please, select cell from the table", @" Refresh error");
                return;
            }
            var vCtr = STool.GetCurrentSql();
            if (vCtr.TypeConnection.Contains("ODBC"))
            {
                string vSql = RemoveSqlLimit(activeCell.ListObject.QueryTable.CommandText);
                CreatePivotTable(GetOdbcNameFromCell(), STool.GetHash(vSql), vSql);
            }
            if (vCtr.TypeConnection.Contains("CLOUD"))
            {
                SqlEngine.CurrExcelApp.SendKeys("%NVT");
            }
            GetSelectedTab();
        }

        public static void RunTableProperties()
        {
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
            // SqlEngine.currExcelApp.CommandBars.ExecuteMso("EditQuery");
            if ((activeCell.ListObject == null) == false)
            {
                SqlEngine.CurrExcelApp.ScreenUpdating = false;

                SqlEngine.CurrExcelApp.SendKeys("%A%P%S");
                SqlEngine.CurrExcelApp.SendKeys("+");
                SqlEngine.CurrExcelApp.CommandBars.ReleaseFocus();

                SqlEngine.CurrExcelApp.ScreenUpdating = true;
            }
            else
                MessageBox.Show(@" Please, select  the external table", @" Refresh error");

            GetSelectedTab();
        }

        public static void RunSqlProperties()
        {
            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;
            // SqlEngine.currExcelApp.CommandBars.ExecuteMso("EditQuery");
            if ((activeCell.ListObject == null) == false)
                SqlEngine.CurrExcelApp.SendKeys("%j%f%o");
            else
                MessageBox.Show(@" Please, select  the external table", @" Refresh error");

            GetSelectedTab();
        }
         
        public static void RunPowerPivotM()
        {
            SqlEngine.CurrExcelApp.SendKeys("%a%d%m");
                  GetSelectedTab();
        }

        public static void GetSelectedTab ()
        {
            SqlEngine.CurrExcelApp.ScreenUpdating = false;
             SqlEngine.CurrExcelApp.SendKeys("%Y%Q%A");
             SqlEngine.CurrExcelApp.SendKeys("%");
             SqlEngine.CurrExcelApp.CommandBars.ReleaseFocus();//CommandBars.ReleaseFocus 
             
            SqlEngine.CurrExcelApp.ScreenUpdating = true;
            } //
        }
    }
