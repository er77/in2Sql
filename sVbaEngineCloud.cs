using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SqlEngine.sCloud;
using Excel = Microsoft.Office.Interop.Excel;

namespace SqlEngine
{
    class sVbaEngineCloud
    {

  
        public static string setSqlLimit(string vCloudType, string vCurrSql)
        {
            string vSql = sSqlVba.RemoveSqlLimit(vCurrSql);
              
            if (vCloudType.Contains("CloudCH"))
            {
                if (vSql.ToUpper().Contains("LIMIT") == false)
                    vSql = vSql + Environment.NewLine + "/*`*/ LIMIT " + Ribbon.vRowCount + " /*`*/ ";
            }
             
            return vSql;
        }
         

        public static void createExTable(string vCurrCloudName, string vTableName, string vCurrSql = null, int isReplace=0, string vOldTableName="")
        {
            var vCurrWorkSheet = SqlEngine.currExcelApp.ActiveSheet;
            var vCurrWorkBook = SqlEngine.currExcelApp.ActiveWorkbook;
           

            if (isReplace == 1)
            {
                vCurrWorkSheet.ListObjects(vOldTableName).Range().Select();
            }

            var vActivCell = SqlEngine.currExcelApp.ActiveCell;

            SqlEngine.currExcelApp.ScreenUpdating = false;

            if (vCurrSql == null )
                vCurrSql = "SELECT \n\t * \n FROM \n\t " + vTableName + "\n where 1=1   "; 

            string vConnURL = sCloud.prepareCloudQuery(vCurrCloudName, vCurrSql );   
            
            if ( (isReplace ==  0 )  &  ((vActivCell.ListObject !=null) | (vActivCell.Value != null) ) )
                {
                    MessageBox.Show(" Please select empty area  in Excel data grid");
                    return;
                }

            if (isReplace == 1)
               if (vActivCell.ListObject != null)
                {
                    try
                    {
                        if (vOldTableName == "")
                            vActivCell.ListObject.Delete();
                        else
                            vCurrWorkSheet.ListObjects(vOldTableName).Delete();
                    }
                    catch
                    {
                    } 
                }


            if (vActivCell != null & vConnURL.Length > 1 & vTableName.Length > 1)
            {
                string vTempFile = "TEXT;" + sTool.writeHttpToFile(vConnURL);
                var xlQueryTable = vCurrWorkSheet.QueryTables.Add(
                                                      Connection: vTempFile
                                                    , Destination: vActivCell
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
                sTool.deleteFile(vTempFile);


                var qtAddress = xlQueryTable.ResultRange.Address;

                xlQueryTable.Delete();
                //, Selection, , xlYes
                var xlTable = vCurrWorkSheet.ListObjects.Add(
                            SourceType: Excel.XlListObjectSourceType.xlSrcRange
                        , Source: vCurrWorkSheet.Range(qtAddress)
                        , XlListObjectHasHeaders: Excel.XlYesNoGuess.xlYes);

                string vExTName = vOldTableName;
                  if (vExTName =="" )
                     vExTName = vCurrCloudName + "|" + vTableName + '|' + DateTime.Now.ToString("YYYYMMDDTHHmmss");
                try
                {
                     vCurrWorkSheet.ListObjects(vExTName).Delete();
                }
                catch { }

                xlTable.Name = vExTName;
                xlTable.Comment = "CLOUD|" + vCurrCloudName + "|" + vCurrSql;

                xlTable.TableStyle = "TableStyleLight13";
                sSqlVba.GetSelectedTab();

                SqlEngine.currExcelApp.ScreenUpdating = true;

                sSqlVba.GetSelectedTab();
                return;
            }          
                
            
        }
    }
}
