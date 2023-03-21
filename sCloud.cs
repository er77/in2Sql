using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlEngine
{
    class sCloud
    {

        public struct CloudObjects
        {
            public String Name;
            public int idTbl;
        }

        public static int vIdtbl = 0;

        public struct ObjectsAndProperties
        {
            public List<String> objColumns;
            public List<String> objIndexes;
          //  public List<String> objDependencies;
            public String ObjName;
        }

        public struct CloudProperties
        {
            public string CloudName, CloudType, Url, Login, Password;
          //  public int ConnStatus;
            public List<CloudObjects> Tables;
            public List<CloudObjects> Views;
            public List<CloudObjects> SQLProgramms;
            public List<CloudObjects> SQLFunctions;
        }

        public static List<CloudProperties> vCloudList = CloudList();

        public static List<ObjectsAndProperties> vCloudObjProp = new List<ObjectsAndProperties>();

        public static List<CloudProperties> CloudList()
        {
            try
            {                
                List<CloudProperties> listClooudProperties = new List<CloudProperties>();               
                listClooudProperties.AddRange(getCloudList());
                return listClooudProperties;
            }
            catch (Exception e)
            {
                sTool.ExpHandler(e, "CloudList");
                return null;
            }
        }

        public static string prepareCloudQuery_int(string Url, string vCurrSql , string vLogin, string vPassword)
        {
            string vResult = Url;
            vResult = vResult.Replace("%SQL%", vCurrSql);
            vResult = vResult.Replace("%LOGIN%", vLogin);
            vResult = vResult.Replace("%PASSWORD%", vPassword);
            return vResult;
        }

        public static string prepareCloudQuery ( string vCloudName, string vCurrSql)
        {

            if (vCurrSql == null | vCloudName == null | vCurrSql == "" | vCloudName == "")
                return "";

            CloudProperties vCurrCloud = sCloud.vCloudList.Find(item => item.CloudName == vCloudName);

            if (vCurrCloud.CloudName == null)
                return "";

            vCurrSql = sVbaEngineCloud.setSqlLimit(vCurrCloud.CloudType, vCurrSql);

            if (vCurrCloud.CloudType.Contains("CloudCH"))
                vCurrSql = vCurrSql.Replace("FORMAT CSVWithNames", "") + " FORMAT CSVWithNames";

            
            sTool.addSqlLog(vCloudName, vCurrSql);                       

            return prepareCloudQuery_int(vCurrCloud.Url, vCurrSql, vCurrCloud.Login, vCurrCloud.Password) ;
        }

        public static int checkCloudState (string  vCurrCloudName)
        {

            CloudProperties vCurrCloud = vCloudList.Find(item => item.CloudName == vCurrCloudName);

            string vSqlURL  = prepareCloudQuery(vCurrCloudName, sLibrary.getCloudSqlCheck(vCurrCloud.CloudType) );
            vSqlURL = sTool.HttpGet(vSqlURL);

            if (vSqlURL.Length < 2)
            {                 
                return -1 ;
            }

            return 1;
        }

        public static string getCloudType(string vCurrCloudName)
        {
            CloudProperties vCurrCloud = vCloudList.Find(item => item.CloudName == vCurrCloudName);
            return vCurrCloud.CloudType;
        }

 
        public static IEnumerable<CloudObjects> getCloudTableList(string vCurrCloudName)
            {
            CloudProperties vCurrCloud = vCloudList.Find(item => item.CloudName == vCurrCloudName);
            string vSqlURL;

            vSqlURL = prepareCloudQuery(vCurrCloudName, sLibrary.getCloudSqlTable(vCurrCloud.CloudType));

             return getCloudObjectList(vSqlURL);
               
            }

        public static IEnumerable<CloudObjects> getCloudViewList(string vCurrCloudName)
        {
            CloudProperties vCurrCloud = vCloudList.Find(item => item.CloudName == vCurrCloudName);
            string vSqlURL;

            vSqlURL = prepareCloudQuery(vCurrCloudName, sLibrary.getCloudSqlView(vCurrCloud.CloudType));

            return getCloudObjectList(vSqlURL);

        }

        private static IEnumerable<CloudObjects> getCloudObjectList(string vSqlURL)
        {
             
            List<String> vObjects = new List<String>();
            vObjects.AddRange(sTool.HttpGetArray(vSqlURL));
            int i = 0;
            foreach (var vCurrObj in vObjects)
            {
                i += 1;
                if (i < 2)
                    continue;

                CloudObjects vObj = new CloudObjects();
                vObj.Name = vCurrObj.ToString().Replace('"',' ').Trim();
                vObj.idTbl = vIdtbl;
                vIdtbl = vIdtbl + 1;
                yield return vObj;
            }
        }


        public static IEnumerable<ObjectsAndProperties> getObjectProperties(string vCurrCloudName, string vObjName)
        {
            CloudProperties vCurrCloud = vCloudList.Find(item => item.CloudName == vCurrCloudName);
            string vSqlURL;

            vSqlURL = prepareCloudQuery(vCurrCloudName, sLibrary.getCloudColumns(vCurrCloud.CloudType));
            var vTb1 = vObjName.Split('.');

            vSqlURL = vSqlURL.Replace("%TNAME%", vTb1[1]);
            vSqlURL = vSqlURL.Replace("%TOWNER%", vTb1[0]);           
             
            ObjectsAndProperties vObject = new ObjectsAndProperties();
            vObject.ObjName = vCurrCloudName + '.' + vObjName;
            vObject.objColumns = new List<string>(); 

            List<String> vObjects = new List<String>();
            vObjects.AddRange(sTool.HttpGetArray(vSqlURL));
            int i = 0;
            foreach (var vCurrObj in vObjects)
            {
                i += 1;
                if (i < 2)
                    continue;
                vObject.objColumns.Add(vCurrObj.ToString().Replace('"', ' ').Trim());
            }

            yield return vObject;

        }
         

        public static IEnumerable<CloudProperties> getCloudList()
        {
            RegistryKey vCurrRegKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
            string vCurrName = "";
            string vPrevName = "";
            if (vCurrRegKey != null)
            {
                CloudProperties vCloudProperties= new CloudProperties();  

                foreach (string name in vCurrRegKey.GetValueNames())
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
                        vCurrName = vNameDetails[1];

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
