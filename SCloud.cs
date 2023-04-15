using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SqlEngine
{
    internal abstract class SCloud
    {

        public struct CloudObjects
        {
            public string Name;
            public int IdTbl;
        }

        private static int _idtbl;

        public struct ObjectsAndProperties
        {
            public List<string> ObjColumns;
            public List<string> ObjIndexes;
          //  public List<String> objDependencies;
            public string ObjName;

            public static List<ObjectsAndProperties> VCloudObjProp = new List<ObjectsAndProperties>();
        }

        public struct CloudProperties
        {
            public string CloudName, CloudType, Url, Login, Password;
          //  public int ConnStatus;
            public List<CloudObjects> Tables;
            public List<CloudObjects> Views;
            public List<CloudObjects> SqlPrograms;
            public List<CloudObjects> SqlFunctions;
        }

        public static List<CloudProperties> CloudPropertiesList = CloudList();

        public static readonly List<ObjectsAndProperties> CloudObjectsAndPropertiesList = new List<ObjectsAndProperties>();

        public static List<CloudProperties> CloudList()
        {
            try
            {                
                var cloudPropertiesList = new List<CloudProperties>();               
                cloudPropertiesList.AddRange(GetCloudList());
                return cloudPropertiesList;
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "CloudList");
                return null;
            }
        }

        public static string prepareCloudQuery_int(string url, string vCurrSql , string vLogin, string vPassword)
        {
            var vResult = url;
            vResult = vResult.Replace("%SQL%", vCurrSql);
            vResult = vResult.Replace("%LOGIN%", vLogin);
            vResult = vResult.Replace("%PASSWORD%", vPassword);
            return vResult;
        }

        public static string PrepareCloudQuery ( string vCloudName, string vCurrSql)
        {

            if (vCurrSql == null | vCloudName == null | vCurrSql == "" | vCloudName == "")
                return "";

            var vCurrCloud = SCloud.CloudPropertiesList.Find(item => item.CloudName == vCloudName);

            if (vCurrCloud.CloudName == null)
                return "";

            vCurrSql = SVbaEngineCloud.SetSqlLimit(vCurrCloud.CloudType, vCurrSql);

            if (vCurrCloud.CloudType.Contains("CloudCH"))
                vCurrSql = vCurrSql.Replace("FORMAT CSVWithNames", "") + " FORMAT CSVWithNames";

            
            STool.AddSqlLog(vCloudName, vCurrSql);                       

            return prepareCloudQuery_int(vCurrCloud.Url, vCurrSql, vCurrCloud.Login, vCurrCloud.Password) ;
        }

        public static int CheckCloudState (string  vCurrCloudName)
        {

            CloudProperties vCurrCloud = CloudPropertiesList.Find(item => item.CloudName == vCurrCloudName);

            string sqlUrl  = PrepareCloudQuery(vCurrCloudName, SSqlLibrary.GetCloudSqlCheck(vCurrCloud.CloudType) );
            sqlUrl = STool.HttpGet(sqlUrl);

            if (sqlUrl.Length < 2)
            {                 
                return -1 ;
            }

            return 1;
        }

        public static string GetCloudType(string vCurrCloudName)
        {
            var vCurrCloud = CloudPropertiesList.Find(item => item.CloudName == vCurrCloudName);
            return vCurrCloud.CloudType;
        }

 
        public static IEnumerable<CloudObjects> GetCloudTableList(string vCurrCloudName)
            {
            var vCurrCloud = CloudPropertiesList.Find(item => item.CloudName == vCurrCloudName);

            var sqlUrl = PrepareCloudQuery(vCurrCloudName, SSqlLibrary.GetCloudSqlTable(vCurrCloud.CloudType));

             return GetCloudObjectList(sqlUrl);
               
            }

        public static IEnumerable<CloudObjects> GetCloudViewList(string vCurrCloudName)
        {
            var vCurrCloud = CloudPropertiesList.Find(item => item.CloudName == vCurrCloudName);
            var sqlUrl = PrepareCloudQuery(vCurrCloudName, SSqlLibrary.GetCloudSqlView(vCurrCloud.CloudType));
            return GetCloudObjectList(sqlUrl);
        }

        private static IEnumerable<CloudObjects> GetCloudObjectList(string sqlUrl)
        {
            var vObjects = new List<string>();
            vObjects.AddRange(STool.HttpGetArray(sqlUrl));
            var i = 0;
            foreach (var vCurrObj in vObjects)
            {
                i += 1;
                if (i < 2)
                    continue;
                var vObj = new CloudObjects
                {
                    Name = vCurrObj.Replace('"',' ').Trim(),
                    IdTbl = _idtbl
                };
                _idtbl = _idtbl + 1;
                yield return vObj;
            }
        }


        public static IEnumerable<ObjectsAndProperties> GetObjectProperties(string vCurrCloudName, string vObjName)
        {
            var vCurrCloud = CloudPropertiesList.Find(item => item.CloudName == vCurrCloudName);

            var sqlUrl = PrepareCloudQuery(vCurrCloudName, SSqlLibrary.GetCloudColumns(vCurrCloud.CloudType));
            var vTb1 = vObjName.Split('.');

            sqlUrl = sqlUrl.Replace("%TNAME%", vTb1[1]);
            sqlUrl = sqlUrl.Replace("%TOWNER%", vTb1[0]);           
             
            var vObject = new ObjectsAndProperties
            {
                ObjName = vCurrCloudName + '.' + vObjName,
                ObjColumns = new List<string>()
            };

            var vObjects = new List<string>();
            vObjects.AddRange(STool.HttpGetArray(sqlUrl));
            var i = 0;
            foreach (var vCurrObj in vObjects)
            {
                i += 1;
                if (i < 2)
                    continue;
                vObject.ObjColumns.Add(vCurrObj.Replace('"', ' ').Trim());
            }

            yield return vObject;

        }


        private static IEnumerable<CloudProperties> GetCloudList()
        {
            var vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
            var vPrevName = "";
            if (vCurrRegKey == null) yield break;
            var vCloudProperties= new CloudProperties();  

            foreach (var name in vCurrRegKey.GetValueNames())
            {
                if (name.Contains("Cloud"))
                {
                    var vNameDetails = name.Split('.');

                    if (vNameDetails.Length < 2)
                    {
                        MessageBox.Show(@"Error in reading registry getCloudList ");
                        yield return new CloudProperties();
                        break;
                    }
                    var vCurrName = vNameDetails[1];

                    if (!vCurrName.Equals(vPrevName))
                    {
                        if (vPrevName.Length > 2)
                            yield return vCloudProperties;

                        vCloudProperties = new CloudProperties();
                    }

                    vPrevName = vCurrName;

                    vCloudProperties.CloudName = vCurrName;
                    vCloudProperties.CloudType = vNameDetails[0];

                    var vCurrRegValue = SRegistry.GetLocalRegValue(vCurrRegKey, name);

                    if (name.Contains("Url"))
                        vCloudProperties.Url = vCurrRegValue;

                    if (name.Contains("Password"))
                        vCloudProperties.Password = vCurrRegValue;

                    if (name.Contains("Login"))
                        vCloudProperties.Login = vCurrRegValue;

                    if (vCloudProperties.Url == null) continue;

                    if (vCloudProperties.Password == null) continue;

                    if (vCloudProperties.Login.Length <= 0) continue;
                    
                    vPrevName = "";
                    yield return vCloudProperties;
                }
                else { 
                    if (vPrevName.Length > 2 )
                    {
                        vPrevName = "";                           
                    }
                }
               
            }
        }
    }
}
