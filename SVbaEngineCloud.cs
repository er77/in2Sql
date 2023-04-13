using System;
using System.Windows.Forms;
using static SqlEngine.SCloud;
using Excel = Microsoft.Office.Interop.Excel;

namespace SqlEngine
{
    internal static class SVbaEngineCloud
    {
        public static string SetSqlLimit(string vCloudType, string vCurrSql)
        {
            var vSql = IntSqlVbaEngine.RemoveSqlLimit(vCurrSql);

            if (!vCloudType.Contains("CloudCH")) return vSql;
            
            if (vSql.ToUpper().Contains("LIMIT") == false)
                vSql = vSql + Environment.NewLine + "/*`*/ LIMIT " + Ribbon.VRowCount + " /*`*/ ";

            return vSql;
        }


        public static void CreateExTable(string vCurrCloudName, string vTableName, string vCurrSql = null, int isReplace=0, string vOldTableName="")
        {
            var vCurrWorkSheet = SqlEngine.CurrExcelApp.ActiveSheet;
            
            if (isReplace == 1)
            {
                vCurrWorkSheet.ListObjects(vOldTableName).Range().Select();
            }

            var activeCell = SqlEngine.CurrExcelApp.ActiveCell;

            SqlEngine.CurrExcelApp.ScreenUpdating = false;

            if (vCurrSql == null )
                vCurrSql = "SELECT \n\t * \n FROM \n\t " + vTableName + "\n where 1=1   "; 

            var vConnUrl = PrepareCloudQuery(vCurrCloudName, vCurrSql );   
            
            if ( (isReplace ==  0 )  &  ((activeCell.ListObject !=null) | (activeCell.Value != null) ) )
            {
                MessageBox.Show(@" Please select empty area  in Excel data grid");
                return;
            }

            if (isReplace == 1)
               if (activeCell.ListObject != null)
               { try
                   {
                       if (vOldTableName == "")
                           activeCell.ListObject.Delete();
                       else
                           vCurrWorkSheet.ListObjects(vOldTableName).Delete();
                   }
                   catch
                   {
                       return;
                   } 
               }


            if (!(vConnUrl.Length > 1 & vTableName.Length > 1)) return;
            
            var vTempFile = "TEXT;" + STool.WriteHttpToFile(vConnUrl);
            var xlQueryTable = vCurrWorkSheet.QueryTables.Add(
                Connection: vTempFile
                , Destination: activeCell
            );

            xlQueryTable.Name = vCurrCloudName + "|" + vTableName;
            xlQueryTable.FieldNames = true;
            xlQueryTable.RowNumbers = false;
            xlQueryTable.FillAdjacentFormulas = false;
            xlQueryTable.PreserveFormatting = true;
            xlQueryTable.Connection = vTempFile;
            xlQueryTable.RefreshOnFileOpen = false;
            xlQueryTable.RefreshStyle = Excel.XlCellInsertionMode.xlInsertDeleteCells;
            xlQueryTable.SavePassword = false;
            xlQueryTable.SaveData = true;
            xlQueryTable.AdjustColumnWidth = true;
            xlQueryTable.RefreshPeriod = 0;
            xlQueryTable.TextFilePromptOnRefresh = false;
            xlQueryTable.TextFileStartRow = 1;
            xlQueryTable.TextFileConsecutiveDelimiter = false;
            xlQueryTable.TextFileTabDelimiter = true;
            xlQueryTable.TextFileCommaDelimiter = true;
            xlQueryTable.TextFileSemicolonDelimiter = true;
            xlQueryTable.TextFileOtherDelimiter = "|";
            xlQueryTable.TextFileSpaceDelimiter = false;
            xlQueryTable.SourceDataFile = vCurrCloudName + "|" + vCurrSql;
            xlQueryTable.Refresh(true);

            vTempFile = vTempFile.Replace("TEXT;", "");
            STool.DeleteFile(vTempFile);


            var qtAddress = xlQueryTable.ResultRange.Address;

            xlQueryTable.Delete();
            //, Selection, , xlYes
            var xlTable = vCurrWorkSheet.ListObjects.Add(
                SourceType: Excel.XlListObjectSourceType.xlSrcRange
                , Source: vCurrWorkSheet.Range(qtAddress)
                , XlListObjectHasHeaders: Excel.XlYesNoGuess.xlYes);

            var vExTName = vOldTableName;
            if (vExTName =="" )
                vExTName = vCurrCloudName + "|" + vTableName + '|' + DateTime.Now.ToString("YYYYMMDDTHHmmss");
            try
            {
                vCurrWorkSheet.ListObjects(vExTName).Delete();
            }
            catch
            {
                //vExTName = "";
            }

            xlTable.Name = vExTName;
            xlTable.Comment = "CLOUD|" + vCurrCloudName + "|" + vCurrSql;

            xlTable.TableStyle = "TableStyleLight13";
            IntSqlVbaEngine.GetSelectedTab();

            SqlEngine.CurrExcelApp.ScreenUpdating = true;

            IntSqlVbaEngine.GetSelectedTab();


        }
    }
}
