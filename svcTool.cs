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
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

namespace SqlEngine
{
    class svcTool
    {
        //  [ThreadStatic]
        public static int isGCrun = 0;

        private static int isGcRunCount = 0;

        public static List<String> SqlHistory = new List<String>();
        public static List<String> LogViewer = new List<String>();


        public const string fileEventLog = @"%USERPROFILE%\\appdata\\roaming\\Microsoft\\AddIns\\in2Sql_LogEvent.log";
        public const string fileSqlLog = @"%USERPROFILE%\\appdata\\roaming\\Microsoft\\AddIns\\in2Sql_LogSqlEngine.log";

        private static string getDataTime(string vStr)
        {
            return DateTime.Now.ToString("yyyy.mm.dd HH:mm:ss") + "\n\r" + vStr;
        }

        private static void appendTxtFile(string vFileName, string vStr)
        {
            var filePath = Environment.ExpandEnvironmentVariables(vFileName);
            if (!File.Exists(filePath))
                File.WriteAllText(filePath, vStr);
            else
                File.AppendAllText(filePath, vStr);
        }

        public static void addSqlLog(string vStr, string vStr2 = "")
        {
            if (vStr2 != "")
            {
                vStr = vStr + ":\n\r\t" + vStr2;
            }
            addEventLog(vStr);
            vStr = getDataTime(vStr);
            SqlHistory.Add(vStr);
            appendTxtFile(fileSqlLog, vStr);

        }

        public static void addEventLog(string vStr)
        {
            vStr = getDataTime(vStr);
            LogViewer.Add(vStr);
            appendTxtFile(fileEventLog, vStr);

        }

        public static void ExpHandler(Exception e, string AdditionalInformation = "", string vSQl = "")
        {
            DialogResult result;

            result = MessageBox.Show($"Generic Exception Handler: {e}", AdditionalInformation + " Error message");

            addEventLog(e.ToString() + AdditionalInformation);
            if (vSQl != "")
            {
                result = MessageBox.Show(vSQl, AdditionalInformation + " Error message");
                addEventLog(vSQl + AdditionalInformation);
            }

            RunGarbageCollector();
        }

        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            return Convert.ToBase64String(hash);
        }


        private static void ExecuteCommandSync(object vRunCommad)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo =
                  new System.Diagnostics.ProcessStartInfo("cmd", "/c " + vRunCommad.ToString());

                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;

                procStartInfo.CreateNoWindow = true;

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.Dispose();
            }
            catch (Exception e)
            {
                ExpHandler(e, "ExecuteCommandSync");
            }
        }

        private static void runGBCollection()
        {
            if (isGCrun == 0)
            {
                isGCrun = 1;
                Thread.Sleep(2000);
                GC.Collect();
                isGCrun = 0;
            }

        }

        public static void RunGarbageCollector()
        {
            try
            {
                isGcRunCount = isGcRunCount + 1;
                if ((isGcRunCount % 7) == 0)
                {
                    isGcRunCount = 1;

                    Thread objThread = new Thread(new ThreadStart(runGBCollection))
                    {
                        IsBackground = true,
                        Priority = ThreadPriority.AboveNormal
                    };
                    if ((objThread.ThreadState & ThreadState.WaitSleepJoin) == 0)
                    {
                        objThread.Start();
                    }

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

        public static void RunCmdLauncher(string vRunCommad)
        {
            try
            {
                Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
                objThread.IsBackground = true;
                objThread.Priority = ThreadPriority.AboveNormal;
                objThread.Start(vRunCommad);
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(vHttpUrl);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                ExpHandler(e, "In2SqlSvcTool.HttpGet");
                return null;
            }

        }

        public static IEnumerable<String> CloudSplitText(string vHttpText)
        {
            String[] vSplittedText = vHttpText.Split('\n');
            foreach (String vCurrLine in vSplittedText)
            {
                yield return vCurrLine;//.Replace('\r', "");
            }
        }


        public static IEnumerable<String> HttpGetArray(string vHttpUrl)
        {
            vHttpUrl = vHttpUrl.Replace("\n", " ");
            vHttpUrl = vHttpUrl.Replace("\r", " ");
            vHttpUrl = vHttpUrl.Replace("\t", " ");
            vHttpUrl = vHttpUrl.Replace("/*`*/", " ");
            vHttpUrl = vHttpUrl.Replace("  ", " ");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(vHttpUrl);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader readStream = new StreamReader(stream))
            {
                Char[] vCharReadBuf = new Char[256];
                int vReadCharCount = 1; //readStream.Read(vCharReadBuf, 0, 256);
                string vStrBuff = "";
                while (vReadCharCount > 0)
                {
                    vReadCharCount = readStream.Read(vCharReadBuf, 0, 256);
                    if (vReadCharCount > 0)
                    {
                        vStrBuff = vStrBuff + new String(vCharReadBuf, 0, vReadCharCount);
                        String[] vStrArr = vStrBuff.Split('\n');
                        if (vStrArr.Count() > 0)
                        {
                            for (int i = 0; i < (vStrArr.Count() - 1); i++)
                            {
                                yield return vStrArr[i];
                            }
                            vStrBuff = vStrArr[vStrArr.Count() - 1];
                        }
                    }

                }
            }

        }

        public static string getTmpFileName()
        {
            string vFileName = Path.GetTempFileName();
            vFileName = vFileName.ToUpper().Replace(".TMP", ".csv");
            File.Delete(vFileName);

            return vFileName;

        }

        public static string writeHttpToFile(string vHttpUrl)
        {
            try
            {
                string vFileName = getTmpFileName();

                using (StreamWriter vCurrFile = new StreamWriter(vFileName))
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
                svcTool.ExpHandler(e, "In2SqlSvcTool.writeHttpToFile", vHttpUrl);
                return null;
            }
        }

        public static IEnumerable<String> sqlReadQuery(string vOdbcName, string queryString = "")
        {
            var vCurrODBC = svcODBC.vODBCList.Find(item => item.OdbcName == vOdbcName);

            using
                  (OdbcConnection conn = new System.Data.Odbc.OdbcConnection())
            {
                using (OdbcCommand cmnd = new OdbcCommand(queryString, conn))
                {
                    try
                    {
                        vCurrODBC.DSNStr = "DSN=" + vOdbcName;
                        if (vCurrODBC.Login != null)
                        {
                            vCurrODBC.DSNStr = vCurrODBC.DSNStr + ";Uid=" + vCurrODBC.Login + ";Pwd=" + vCurrODBC.Password + ";";
                        }

                        conn.ConnectionString = vCurrODBC.DSNStr;
                        conn.ConnectionTimeout = 5;
                        conn.Open();
                    }
                    catch (Exception e)
                    {
                        svcTool.ExpHandler(e, "In2SqlSvcODBC.ReadData", queryString);
                        conn.Close();
                        conn.Dispose();
                        yield break;
                    }
                    OdbcDataReader rd = cmnd.ExecuteReader();
                    string strRow = "";
                    while (rd.Read())
                    {
                        strRow = "";
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

        public static string writeSqlToFile(string vOdbcName, string queryString = "")
        {
            try
            {

                string vFileName = getTmpFileName();

                using (StreamWriter vCurrFile = new StreamWriter(vFileName))
                {
                    foreach (var str in sqlReadQuery(vOdbcName, queryString))
                    {
                        vCurrFile.WriteLine(str);
                    }

                }
                return vFileName;
            }
            catch (Exception e)
            {
                svcTool.ExpHandler(e, "In2SqlSvcTool.writeSqlToFile", vOdbcName + " # " + queryString);
                return null;
            }

        }  

        public static DataTable ConvertCSVtoDataTable(string strFilePath, char vSplitChar)
        {   DataTable csvData = new DataTable(); 
                using (TextFieldParser csvReader = new TextFieldParser(strFilePath))
                {
                    csvReader.SetDelimiters(new string[] { vSplitChar.ToString() });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            
            return csvData;
        }

        public static void  deleteFile(string vTempFile)
        {
            try
            {
                File.Delete(vTempFile);
            }
            catch 
            {
                vTempFile = "";
            }
        }

       public struct CurrentTableRecords 
        {  
            public string  TypeConnection, TableName , CurrCloudName, CurrCloudExTName, Sql;
        } 

        public static CurrentTableRecords getCurrentSql ()
        {           
            var vCurrTable = SqlEngine.currExcelApp.ActiveCell.ListObject;
            CurrentTableRecords vCTR = new CurrentTableRecords();
            vCTR.Sql = "";
            vCTR.TypeConnection = "";
            vCTR.TableName = "";
            vCTR.CurrCloudName = "";
            vCTR.CurrCloudExTName = "";

            if (vCurrTable == null)
                return vCTR;

            if (vCurrTable.Comment.Contains("CLOUD"))
            {               
                string[] vTemp1 = vCurrTable.Comment.Split('|');
                if (vTemp1.Count() < 2)
                    return vCTR;
                vCTR.TypeConnection = "CLOUD";
                vCTR.Sql = vTemp1[2];
                vCTR.CurrCloudName = vTemp1[1];

                string[] vTemp2 = vCurrTable.Name.Split('|');
                vCTR.CurrCloudExTName = vCurrTable.Name;
                vCTR.TableName = vTemp2[1];
            }
            else
            {
                vCTR.TypeConnection = "ODBC";
                vCTR.Sql = vCurrTable.QueryTable.CommandText;
            }

            vCTR.Sql = intSqlVBAEngine.RemoveBetween(vCTR.Sql, '`', '`');
            vCTR.Sql = vCTR.Sql.Replace("/**/", "");

            return vCTR;
        }

    }
}
