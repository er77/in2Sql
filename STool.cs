using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Data.Odbc;
using System.Data;
using Microsoft.VisualBasic.FileIO;

namespace SqlEngine
{
    class STool
    {
        //  [ThreadStatic]
        public static int IsGCrun;

        private static int _isGcRunCount;

        private static readonly List<string> SqlHistory = new List<string>();
        private static readonly List<string> LogViewer = new List<string>();


        private const string FileEventLog = @"%USERPROFILE%\\appdata\\roaming\\Microsoft\\AddIns\\in2Sql_LogEvent.log";
        private const string FileSqlLog = @"%USERPROFILE%\\appdata\\roaming\\Microsoft\\AddIns\\in2Sql_LogSqlEngine.log";

        private static string GetDataTime(string vStr)
        {
            return DateTime.Now.ToString("yyyy.mm.dd HH:mm:ss") + "\n\r" + vStr;
        }

        private static void AppendTxtFile(string vFileName, string vStr)
        {
            var filePath = Environment.ExpandEnvironmentVariables(vFileName);
            if (!File.Exists(filePath))
                File.WriteAllText(filePath, vStr);
            else
                File.AppendAllText(filePath, vStr);
        }

        public static void AddSqlLog(string vStr, string vStr2 = "")
        {
            if (vStr2 != "")
            {
                vStr = vStr + ":\n\r\t" + vStr2;
            }
            AddEventLog(vStr);
            vStr = GetDataTime(vStr);
            SqlHistory.Add(vStr);
            AppendTxtFile(FileSqlLog, vStr);

        }

        public static void AddEventLog(string vStr)
        {
            vStr = GetDataTime(vStr);
            LogViewer.Add(vStr);
            AppendTxtFile(FileEventLog, vStr);

        }

        public static void ExpHandler(Exception e, string additionalInformation = "", string vSQl = "")
        {
            MessageBox.Show($@"Generic Exception Handler: {e}", additionalInformation + @" Error message");
            
            AddEventLog(e + additionalInformation);
            if (vSQl != "")
            {
                MessageBox.Show(vSQl, additionalInformation + @" Error message");
                AddEventLog(vSQl + additionalInformation);
            }
            RunGarbageCollector();
        }

        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            return Convert.ToBase64String(hash);
        }


        private static void ExecuteCommandSync(object runCommand)
        {
            try
            {
                var procStartInfo =
                  new System.Diagnostics.ProcessStartInfo("cmd", "/c " + runCommand)
                     {
                         RedirectStandardOutput = true,
                         UseShellExecute = false,
                         CreateNoWindow = true
                     };
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.Dispose();
            }
            catch (Exception e)
            {
                ExpHandler(e, "ExecuteCommandSync");
            }
        }

        private static void RunGbCollection()
        {
            if (IsGCrun != 0) return;
            
            IsGCrun = 1;
            Thread.Sleep(2000);
            GC.Collect();
            IsGCrun = 0;

        }

        public static void RunGarbageCollector()
        {
            try
            {
                _isGcRunCount += 1;
                if ((_isGcRunCount % 7) != 0) return;
                
                _isGcRunCount = 1;

                var objThread = new Thread(RunGbCollection)
                {
                    IsBackground = true,
                    Priority = ThreadPriority.AboveNormal
                };
                if ((objThread.ThreadState & ThreadState.WaitSleepJoin) == 0)
                {
                    objThread.Start();
                }
            }
            catch (ThreadStartException e)
            {
                ExpHandler(e, "RunCmdLauncher.ThreadStartException");
            }
            catch (ThreadAbortException e)
            {
                ExpHandler(e, "RunCmdLauncher.ThreadAbortException");
            }
            catch (Exception e)
            {
                ExpHandler(e, "RunCmdLauncher.objException");
            }

        }

        public static void RunCmdLauncher(string runCommand)
        {
            try
            {
                var objThread = new Thread(ExecuteCommandSync)
                {
                    IsBackground = true,
                    Priority = ThreadPriority.AboveNormal
                };
                objThread.Start(runCommand);
            }
            catch (ThreadStartException e)
            {
                ExpHandler(e, "RunCmdLauncher.ThreadStartException");
            }
            catch (ThreadAbortException e)
            {
                ExpHandler(e, "RunCmdLauncher.ThreadAbortException");
            }
            catch (Exception e)
            {
                ExpHandler(e, "RunCmdLauncher.objException");
            }

        }

        public static string HttpGet(string vHttpUrl)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(vHttpUrl);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                    if (stream != null)
                        using (var reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
            }
            catch (Exception e)
            {
                ExpHandler(e, "In2SqlSvcTool.HttpGet");
                return null;
            }
            return null;
        }

        public static IEnumerable<String> CloudSplitText(string vHttpText)
        {
            return vHttpText.Split('\n');
        }


        public static IEnumerable<string> HttpGetArray(string vHttpUrl)
        {
            vHttpUrl = vHttpUrl.Replace("\n", " ");
            vHttpUrl = vHttpUrl.Replace("\r", " ");
            vHttpUrl = vHttpUrl.Replace("\t", " ");
            vHttpUrl = vHttpUrl.Replace("/*`*/", " ");
            vHttpUrl = vHttpUrl.Replace("  ", " ");

            var request = (HttpWebRequest)WebRequest.Create(vHttpUrl);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
                if (stream != null)
                    using (var readStream = new StreamReader(stream))
                    {
                        var vCharReadBuf = new Char[256];
                        var vReadCharCount = 1; //readStream.Read(vCharReadBuf, 0, 256);
                        var vStrBuff = "";
                        while (vReadCharCount > 0)
                        {
                            vReadCharCount = readStream.Read(vCharReadBuf, 0, 256);
                            
                            if (vReadCharCount <= 0) continue;
                            
                            vStrBuff = vStrBuff + new String(vCharReadBuf, 0, vReadCharCount);
                            var vStrArr = vStrBuff.Split('\n');

                            if (!vStrArr.Any()) continue;
                            
                            for (var i = 0; i < (vStrArr.Count() - 1); i++)
                            {
                                yield return vStrArr[i];
                            }

                            vStrBuff = vStrArr[vStrArr.Count() - 1];
                        }
                    }
        }

        private static string GetTmpFileName()
        {
            var vFileName = Path.GetTempFileName();
            vFileName = vFileName.ToUpper().Replace(".TMP", ".csv");
            File.Delete(vFileName);

            return vFileName;

        }

        public static string WriteHttpToFile(string vHttpUrl)
        {
            try
            {
                var vFileName = GetTmpFileName();

                using (var vCurrFile = new StreamWriter(vFileName))
                {
                    foreach (var str in HttpGetArray(vHttpUrl))
                    {
                        vCurrFile.WriteLine(str);
                    }

                }
                return vFileName;
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "In2SqlSvcTool.writeHttpToFile", vHttpUrl);
                return null;
            }
        }

        private static IEnumerable<string> SqlReadQuery(string vOdbcName, string queryString = "")
        {
            var vCurrOdbc = SOdbc.OdbcPropertiesList.Find(item => item.OdbcName == vOdbcName);
            using
                  (var conn = new OdbcConnection())
            {
                using (var odbcCommand = new OdbcCommand(queryString, conn))
                {
                    try
                    {
                        vCurrOdbc.DsnStr = "DSN=" + vOdbcName;
                        if (vCurrOdbc.Login != null)
                        {
                            vCurrOdbc.DsnStr = vCurrOdbc.DsnStr + ";Uid=" + vCurrOdbc.Login + ";Pwd=" + vCurrOdbc.Password + ";";
                        }

                        conn.ConnectionString = vCurrOdbc.DsnStr;
                        conn.ConnectionTimeout = 5;
                        conn.Open();
                    }
                    catch (Exception e)
                    {
                        STool.ExpHandler(e, "In2SqlSvcODBC.ReadData", queryString);
                        conn.Close();
                        conn.Dispose();
                        yield break;
                    }
                    var rd = odbcCommand.ExecuteReader();
                    while (rd.Read())
                    {
                        var strRow = "";
                        for (int i = 0; i < rd.FieldCount; i++)
                        {
                            strRow = strRow + '"' + rd.GetString(i) + '"';
                            if (i < rd.FieldCount - 1)
                            {
                                strRow += ",";
                            }
                        }

                        yield return strRow;
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

        }

        public static string WriteSqlToFile(string vOdbcName, string queryString = "")
        {
            try
            {

                string vFileName = GetTmpFileName();

                using (var vCurrFile = new StreamWriter(vFileName))
                {
                    foreach (var str in SqlReadQuery(vOdbcName, queryString))
                    {
                        vCurrFile.WriteLine(str);
                    }

                }
                return vFileName;
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "In2SqlSvcTool.writeSqlToFile", vOdbcName + " # " + queryString);
                return null;
            }

        }  

        public static DataTable ConvertCsVtoDataTable(string strFilePath, char vSplitChar)
        {   var csvData = new DataTable(); 
                using (var csvReader = new TextFieldParser(strFilePath))
                {
                    csvReader.SetDelimiters(new string[] { vSplitChar.ToString() });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    var colFields = csvReader.ReadFields();
                    if (colFields != null)
                        foreach (var column in colFields)
                        {
                            var datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }

                    while (!csvReader.EndOfData)
                    {
                        var fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        if (fieldData != null)
                            for (var i = 0; i < fieldData.Length; i++)
                            {
                                if (ReferenceEquals(fieldData[i], ""))
                                {
                                    fieldData[i] = null;
                                }
                            }
                        if (fieldData != null) csvData.Rows.Add(fieldData);
                    }
                }
            
                return csvData;
        }

        public static void  DeleteFile(string vTempFile)
        {
            try
            {
                File.Delete(vTempFile);
            }
            catch
            {
                //return;
            }
        }

       public struct CurrentTableRecords 
        {  
            public string  TypeConnection, TableName , CurrCloudName, CurrCloudExTName, Sql;
        } 

        public static CurrentTableRecords GetCurrentSql ()
        {           
            var vCurrTable = SqlEngine.CurrExcelApp.ActiveCell.ListObject;
            var vCtr = new CurrentTableRecords
            {
                Sql = "",
                TypeConnection = "",
                TableName = "",
                CurrCloudName = "",
                CurrCloudExTName = ""
            };

            if (vCurrTable == null)
                return vCtr;

            if (vCurrTable.Comment.Contains("CLOUD"))
            {               
                var vTemp1 = vCurrTable.Comment.Split('|');
                if (vTemp1.Count() < 2)
                    return vCtr;
                vCtr.TypeConnection = "CLOUD";
                vCtr.Sql = vTemp1[2];
                vCtr.CurrCloudName = vTemp1[1];

                var vTemp2 = vCurrTable.Name.Split('|');
                vCtr.CurrCloudExTName = vCurrTable.Name;
                vCtr.TableName = vTemp2[1];
            }
            else
            {
                vCtr.TypeConnection = "ODBC";
                vCtr.Sql = vCurrTable.QueryTable.CommandText;
            }

            vCtr.Sql = IntSqlVbaEngine.RemoveBetween(vCtr.Sql, '`', '`');
            vCtr.Sql = vCtr.Sql.Replace("/**/", "");

            return vCtr;
        }

    }
}
