using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SqlEngine
{
    class SCloud
    {

        public struct CloudObjects
        {
            public String Name;
            public int IdTbl;
        }

        private static int _idtbl;

        public struct ObjectsAndProperties
        {
            public List<String> ObjColumns;
           // public List<String> ObjIndexes;
          //  public List<String> objDependencies;
            public String ObjName;
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

        public static List<ObjectsAndProperties> CloudObjectsAndPropertiesList = new List<ObjectsAndProperties>();

        public static List<CloudProperties> CloudList()
        {
            try
            {                
                List<CloudProperties> listClooudProperties = new List<CloudProperties>();               
                listClooudProperties.AddRange(GetCloudList());
                return listClooudProperties;
            }
            catch (Exception e)
            {
                sTool.ExpHandler(e, "CloudList");
                return null;
            }
        }

        public static string prepareCloudQuery_int(string url, string vCurrSql , string vLogin, string vPassword)
        {
            string vResult = url;
            vResult = vResult.Replace("%SQL%", vCurrSql);
            vResult = vResult.Replace("%LOGIN%", vLogin);
            vResult = vResult.Replace("%PASSWORD%", vPassword);
            return vResult;
        }

        public static string PrepareCloudQuery ( string vCloudName, string vCurrSql)
        {

            if (vCurrSql == null | vCloudName == null | vCurrSql == "" | vCloudName == "")
                return "";

            CloudProperties vCurrCloud = SCloud.CloudPropertiesList.Find(item => item.CloudName == vCloudName);

            if (vCurrCloud.CloudName == null)
                return "";

            vCurrSql = SVbaEngineCloud.SetSqlLimit(vCurrCloud.CloudType, vCurrSql);

            if (vCurrCloud.CloudType.Contains("CloudCH"))
                vCurrSql = vCurrSql.Replace("FORMAT CSVWithNames", "") + " FORMAT CSVWithNames";

            
            sTool.addSqlLog(vCloudName, vCurrSql);                       

            return prepareCloudQuery_int(vCurrCloud.Url, vCurrSql, vCurrCloud.Login, vCurrCloud.Password) ;
        }

        public static int CheckCloudState (string  vCurrCloudName)
        {

            CloudProperties vCurrCloud = CloudPropertiesList.Find(item => item.CloudName == vCurrCloudName);

            string sqlUrl  = PrepareCloudQuery(vCurrCloudName, SSqlLibrary.GetCloudSqlCheck(vCurrCloud.CloudType) );
            sqlUrl = sTool.HttpGet(sqlUrl);

            if (sqlUrl.Length < 2)
            {                 
                return -1 ;
            }

            return 1;
        }

        public static string GetCloudType(string vCurrCloudName)
        {
            CloudProperties vCurrCloud = CloudPropertiesList.Find(item => item.CloudName == vCurrCloudName);
            return vCurrCloud.CloudType;
        }

 
        public static IEnumerable<CloudObjects> GetCloudTableList(string vCurrCloudName)
            {
            CloudProperties vCurrCloud = CloudPropertiesList.Find(item => item.CloudName == vCurrCloudName);
            string sqlUrl;

            sqlUrl = PrepareCloudQuery(vCurrCloudName, SSqlLibrary.GetCloudSqlTable(vCurrCloud.CloudType));

             return GetCloudObjectList(sqlUrl);
               
            }

        public static IEnumerable<CloudObjects> GetCloudViewList(string vCurrCloudName)
        {
            CloudProperties vCurrCloud = CloudPropertiesList.Find(item => item.CloudName == vCurrCloudName);
            string sqlUrl;
            sqlUrl = PrepareCloudQuery(vCurrCloudName, SSqlLibrary.GetCloudSqlView(vCurrCloud.CloudType));
            return GetCloudObjectList(sqlUrl);
        }

        private static IEnumerable<CloudObjects> GetCloudObjectList(string sqlUrl)
        {
            var vObjects = new List<String>();
            vObjects.AddRange(sTool.HttpGetArray(sqlUrl));
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
             
            var vObject = new ObjectsAndProperties();
            vObject.ObjName = vCurrCloudName + '.' + vObjName;
            vObject.ObjColumns = new List<string>(); 

            var vObjects = new List<String>();
            vObjects.AddRange(sTool.HttpGetArray(sqlUrl));
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
         

        public static IEnumerable<CloudProperties> GetCloudList()
        {
            RegistryKey vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
            string vPrevName = "";
            if (vCurrRegKey != null)
            {
                CloudProperties vCloudProperties= new CloudProperties();  

                foreach (var name in vCurrRegKey.GetValueNames())
                {
                    if (name.Contains("Cloud"))
                    {
                        string[] vNameDetails = name.Split('.');

                        if (vNameDetails.Count() < 2)
                        {
                            MessageBox.Show("Error in reading registry getCloudList ");
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

                        string vCurrRegValue = sRegistry.getLocalRegValue(vCurrRegKey, name);

                        if (name.Contains("Url"))
                            vCloudProperties.Url = vCurrRegValue;

                        if (name.Contains("Password"))
                            vCloudProperties.Password = vCurrRegValue;

                        if (name.Contains("Login"))
                            vCloudProperties.Login = vCurrRegValue;

                        if (vCloudProperties.Url != null )  
                            if (vCloudProperties.Password != null) 
                                if (vCloudProperties.Login.Length  > 0 )
                                {
                                  vPrevName = "";
                                    yield return vCloudProperties;
                                 }
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
}
