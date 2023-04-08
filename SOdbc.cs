using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace SqlEngine
{

    class SOdbc
    {
        private static int _idtbl;

        public struct SqlObjects
        {
            public String Name;
            public int IdTbl;
        }



        public struct ObjectsAndProperties
        {
            public List<string> ObjColumns;
            public List<string> ObjIndexes;
            public List<string> ObjDependencies;
            public string ObjName;
        }


        public struct OdbcProperties
        {
            public string OdbcName, Database, Description, Driver, LastUser, Server, ConnErrMsg, ConnErrType, DbType, Login, Password, DsnStr;
            public int ConnStatus;
            public List<SqlObjects> Tables;
            public List<SqlObjects> Views;
            public List<SqlObjects> SqlPrograms;
            public List<SqlObjects> SqlFunctions;
        }



        public static List<OdbcProperties> OdbcPropertiesList = OdbcList();

        public static List<ObjectsAndProperties> ObjectsAndPropertiesList = new List<ObjectsAndProperties>();

        public static void ChangeOdbcValue(String vOdbcName, OdbcProperties vNewOdbcValue)
        {
            try
            {
                var vOdbcIndex = SOdbc.OdbcPropertiesList.FindIndex(item => item.OdbcName == vOdbcName);
                OdbcPropertiesList[vOdbcIndex] = vNewOdbcValue;
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "ChangeOdbcValue");
            }

        }

        // var vCurrODBC = In2SqlSvcODBC.vODBCList.Find(item => item.OdbcName == vCurrvODBCList.OdbcName);

        public static string GetOdbcProperties(string odbcName, string vProperties)
        {
            try
            {
                var vCurrOdbc = OdbcPropertiesList.Find(item => item.OdbcName == odbcName);
                var vCurrProp = "";
                if (vProperties.Contains("Database")) vCurrProp = vCurrOdbc.Database;
                else if (vProperties.Contains("Description")) vCurrProp = vCurrOdbc.Description;
                else if (vProperties.Contains("Driver")) vCurrProp = vCurrOdbc.Driver;
                else if (vProperties.Contains("LastUser")) vCurrProp = vCurrOdbc.LastUser;
                else if (vProperties.Contains("ConnErrMsg")) vCurrProp = vCurrOdbc.ConnErrMsg;
                else if (vProperties.Contains("DBType")) vCurrProp = vCurrOdbc.DbType;
                else if (vProperties.Contains("Login")) vCurrProp = vCurrOdbc.Login;
                else if (vProperties.Contains("Password")) vCurrProp = vCurrOdbc.Password;
                else if (vProperties.Contains("DSNStr")) vCurrProp = vCurrOdbc.DsnStr;
                return vCurrProp;
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "getODBCProperties");
                return null;
            }
        }

        private static List<OdbcProperties> OdbcList()
        {
            try
            {
                _idtbl = 0;
                List<OdbcProperties> listOdbcProperties = new List<OdbcProperties>();
                listOdbcProperties.AddRange(GetOdbcList(Registry.CurrentUser));
                listOdbcProperties.AddRange(GetOdbcList(Registry.LocalMachine));
                return listOdbcProperties;
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "ODBCList");
                return null;
            }
        }



        public static IEnumerable<OdbcProperties> GetOdbcList(RegistryKey rootKey)
        {
            RegistryKey regKey = rootKey.OpenSubKey(@"Software\ODBC\ODBC.INI\ODBC Data Sources");
            if (regKey != null)
            {
                foreach (string name in regKey.GetValueNames())
                {

                    var vOdbcProperties = new OdbcProperties
                    {
                        OdbcName = regKey.GetValue(name, "").ToString()
                    };
                    var odbcProperties = vOdbcProperties;
                    if (odbcProperties.OdbcName.Contains("Microsoft ") == false)
                    {
                        try
                        {
                            odbcProperties.OdbcName = name;
                            var vCurrRegKey = rootKey.OpenSubKey(@"Software\ODBC\ODBC.INI\" + name);
                            odbcProperties.Database = SRegistry.GetLocalRegValue(vCurrRegKey, "Database");
                            odbcProperties.Description = SRegistry.GetLocalRegValue(vCurrRegKey, "Description");
                            odbcProperties.Driver = SRegistry.GetLocalRegValue(vCurrRegKey, "Driver");
                            odbcProperties.LastUser = SRegistry.GetLocalRegValue(vCurrRegKey, "LastUser");
                            odbcProperties.Server = SRegistry.GetLocalRegValue(vCurrRegKey, "Server");
                            odbcProperties.ConnStatus = 0;
                            vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
                            odbcProperties.Login = SRegistry.GetLocalRegValue(vCurrRegKey, name + '.' + "Login");
                            odbcProperties.Password = SRegistry.GetLocalRegValue(vCurrRegKey, name + '.' + "Password");
                        }
                        catch (Exception e)
                        {
                            STool.ExpHandler(e, "ODBCList");
                        }
                        yield return odbcProperties;
                    }
                }
            }
        }





        public static IEnumerable<String> SqlReadDataValue(string vOdbcName, string queryString = "")
        {
            var currOdbc = SOdbc.OdbcPropertiesList.Find(item => item.OdbcName == vOdbcName);

            using
                  (var conn = new OdbcConnection())
            {
                using (var cmnd = new OdbcCommand(queryString, conn))
                {
                    try
                    {
                        currOdbc.DsnStr = "DSN=" + vOdbcName;
                        if (currOdbc.Login != null)
                        {
                            currOdbc.DsnStr = currOdbc.DsnStr + ";Uid=" + currOdbc.Login + ";Pwd=" + currOdbc.Password + ";";
                        }

                        conn.ConnectionString = currOdbc.DsnStr;
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
                    OdbcDataReader rd = cmnd.ExecuteReader();
                    while (rd.Read())
                    {
                        yield return rd["value"].ToString();//.Split(',').ToList();  ;
                    }
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public static void CheckOdbcStatus(string vOdbcName)
        {

            var currOdbc = SOdbc.OdbcPropertiesList.Find(item => item.OdbcName == vOdbcName);
            try
            {
                currOdbc.ConnStatus = 0;
                currOdbc.ConnErrMsg = "";
                currOdbc.ConnErrType = "";
                currOdbc.DsnStr = "DSN=" + vOdbcName;
                if (currOdbc.Login != null)
                {
                    currOdbc.DsnStr = currOdbc.DsnStr + ";Uid=" + currOdbc.Login + ";Pwd=" + currOdbc.Password + ";";
                }

                using (OdbcConnection conn = new OdbcConnection())
                {
                    conn.ConnectionString = currOdbc.DsnStr;
                    conn.ConnectionTimeout = 2;
                    conn.Open();

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        currOdbc.ConnStatus = 1;
                        currOdbc.ConnErrMsg = "";
                        currOdbc.DbType = SSqlLibrary.GetDbType(conn.DataSource, conn.Driver);
                    }
                }
            }
            catch (Exception e)
            {
                currOdbc.ConnStatus = -3;
                currOdbc.ConnErrMsg = e.Message;
                currOdbc.ConnErrType = SSqlLibrary.GetErrConType(e.Message);
            }
            ChangeOdbcValue(vOdbcName, currOdbc);
        }

        public static IEnumerable<SqlObjects> GetViewList(string vOdbcName)
        {
            var vViews = SqlReadDataValue(vOdbcName, SSqlLibrary.GetSqlViews(GetOdbcProperties(vOdbcName, "DBType")));
            foreach (var vCurrView in vViews)
            {
                var vView = new SqlObjects
                {
                    Name = vCurrView,
                    IdTbl = _idtbl
                };
                _idtbl = _idtbl + 1;
                yield return vView;
            }
        }

        public static IEnumerable<SqlObjects> GetTableList(string vOdbcName)
        {
            var vTables = SqlReadDataValue(vOdbcName, SSqlLibrary.GetSqlTables(GetOdbcProperties(vOdbcName, "DBType")));
            foreach (var vCurrTable in vTables)
            {
                var vTable = new SqlObjects
                {
                    Name = vCurrTable,
                    IdTbl = _idtbl
                };
                _idtbl = _idtbl + 1;
                yield return vTable;
            }
        }


        public static IEnumerable<SqlObjects> GetSqlProgrammsList(string vOdbcName)
        {
            var vTables = SqlReadDataValue(vOdbcName, SSqlLibrary.GetSqlProgramms(GetOdbcProperties(vOdbcName, "DBType")));
            foreach (var vCurrTable in vTables)
            {
                var vTable = new SqlObjects
                {
                    Name = vCurrTable,
                    IdTbl = _idtbl
                };
                _idtbl = _idtbl + 1;
                yield return vTable;
            }
        }

        public static IEnumerable<SqlObjects> GetSqlFunctionsList(string vOdbcName)
        {
            var vTables = SqlReadDataValue(vOdbcName, SSqlLibrary.GetSqlFunctions(GetOdbcProperties(vOdbcName, "DBType")));
            foreach (var vCurrTable in vTables)
            {
                var vTable = new SqlObjects
                {
                    Name = vCurrTable,
                    IdTbl = _idtbl
                };
                _idtbl = _idtbl + 1;
                yield return vTable;
            }
        }

        public static IEnumerable<ObjectsAndProperties> GetObjectProperties(string vOdbcName, string vObjName)
        {
            var dbType = GetOdbcProperties(vOdbcName, "DBType");
            var sql = SSqlLibrary.GetSqlTableColumn(dbType);

            var vTb1 = vObjName.Split('.');

            if (dbType.Contains("ORACLE"))
            {                
                sql = sql.Replace("%TNAME%", vTb1[1]);
            }
            else if (dbType.Contains("CH"))
            {
                sql = sql.Replace("%TNAME%", vTb1[1]);
                sql = sql.Replace("%TOWNER%", vTb1[0]);
            }
            else
            {
                sql = sql.Replace("%TNAME%", vObjName);
            }

            var vObjects = SqlReadDataValue(vOdbcName, sql);
            var vObject = new ObjectsAndProperties
            {
                ObjName = vOdbcName + '.' + vObjName,
                ObjColumns = new List<string>()
            };

            foreach (var vCurrObject in vObjects)
            {
                vObject.ObjColumns.Add(vCurrObject);
            }

            sql = SSqlLibrary.GetSqlIndexes(dbType);

            if (dbType.Contains("ORACLE"))
            {
                sql = sql.Replace("%TNAME%", vTb1[1]);
            }
            else if (dbType.Contains("CH"))
            {
                sql = sql.Replace("%TNAME%", vTb1[1]);
                sql = sql.Replace("%TOWNER%", vTb1[0]);
            }
            {
                sql = sql.Replace("%TNAME%", vObjName);
            }

            vObjects = SqlReadDataValue(vOdbcName, sql);
            vObject.ObjIndexes = new List<string>();

            foreach (var vCurrObject in vObjects)
            {
                vObject.ObjIndexes.Add(vCurrObject);
            }


            sql = SSqlLibrary.GetSqlDependencies(dbType);
            if (dbType.Contains("ORACLE"))
            {
                sql = sql.Replace("%TOWNER%", vTb1[1]);
                sql = sql.Replace("%TNAME%", vTb1[1]);

                vObjects = SqlReadDataValue(vOdbcName, sql);
                vObject.ObjDependencies = new List<string>();

                foreach (var vCurrObject in vObjects)
                {
                    vObject.ObjDependencies.Add(vCurrObject);
                }

            }            
            

            yield return vObject;
        }

     public static void DumpOdbctoCsv(string vOdbcName, string vSqlCommand, string vCsvFile)
        {
            try
            {
                int i = 0;
                string dsnConn = SOdbc.GetOdbcProperties(vOdbcName, "DSNStr");

                if (dsnConn == null | dsnConn == "")
                {
                    MessageBox.Show("Please make the connection by expand list on the left pane ", @"sql run event",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using   (OdbcConnection conn = new OdbcConnection())
                {
                    using (OdbcCommand cmnd = new OdbcCommand(vSqlCommand, conn))
                    {  conn.ConnectionString = dsnConn;
                       conn.ConnectionTimeout = 5;
                       conn.Open();

                        STool.AddSqlLog(vOdbcName, vSqlCommand);

                        OdbcDataReader rd = cmnd.ExecuteReader();

                        object[] output = new object[rd.FieldCount];

                        using (var textWriter = new StreamWriter(@vCsvFile))
                        {
                            var writer = new CsvWriter(textWriter, CultureInfo.InvariantCulture);
                            writer.Configuration.Delimiter = ",";
                            writer.Configuration.ShouldQuote = (field, context) => true;

                            for (int j = 0; j < rd.FieldCount; j++)
                            {
                                output[j] = rd.GetName(j);
                                writer.WriteField(rd.GetName(j));
                            }

                            writer.NextRecord();
                           
                            while (rd.Read())
                            {
                                rd.GetValues(output);
                                writer.WriteField(output);
                                writer.NextRecord();
                                i++;
                            }
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                } 
                MessageBox.Show($"Export completed. \n\r File name is {vCsvFile} \n\r Row count:{i}", @"csv export",
                                                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                if (e.HResult != -2147024809) 
                    STool.ExpHandler(e, "dumpOdbctoCsv");
            }
        }



    }
}
