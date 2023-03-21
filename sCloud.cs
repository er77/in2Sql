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

        //This code is a method that returns a list of cloud properties. The method first creates a new list and then adds the range of cloud properties to it. It then returns
        // the list of cloud properties. If an exception is thrown, it is handled by the sTool.ExpHandler method.

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

        //This function is used to prepare a cloud query. It takes in a URL, a SQL query, a login, and a password as parameters. It then replaces the placeholders in the URL
        // with the SQL query, login, and password. Finally, it returns the modified URL.
        public static string prepareCloudQuery_init(string Url, string vCurrSql, string vLogin, string vPassword)
        {
            string vResult = Url;
            vResult = vResult.Replace("%SQL%", vCurrSql);
            vResult = vResult.Replace("%LOGIN%", vLogin);
            vResult = vResult.Replace("%PASSWORD%", vPassword);
            return vResult;
        }

        //This code is used to prepare a query for a cloud database. It first checks if the parameters passed in (vCloudName and vCurrSql)
        ///are not null or empty strings. If they are, it returns an empty string. It then searches for the cloud name in the list of clouds 
        ///and if it is not found, it returns an empty string. It then sets  the SQL limit based on the cloud type and adds the SQL log.
        ///Finally, it calls the prepareCloudQuery_init function to prepare the query for the cloud database.

        public static string prepareCloudQuery(string vCloudName, string vCurrSql)
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

            return prepareCloudQuery_init(vCurrCloud.Url, vCurrSql, vCurrCloud.Login, vCurrCloud.Password);

        }

        //This method is used to check the state of a cloud. It takes in a string parameter, vCurrCloudName, which is the name of the cloud to be checked. It then searches
        // the vCloudList for the cloud with the given name and stores it in the variable vCurrCloud. It then prepares a query to check the cloud's state using the getCloud
        //SqlCheck method and stores it in the variable vSqlURL. It then uses the sTool.HttpGet method to send the query and store the response in vSqlURL. If the response
        // is less than 2 characters, it returns -1, otherwise it returns 1.
        public static int checkCloudState(string vCurrCloudName)
        {
            CloudProperties vCurrCloud = vCloudList.Find(item => item.CloudName == vCurrCloudName);

            string vSqlURL = prepareCloudQuery(vCurrCloudName, sLibrary.getCloudSqlCheck(vCurrCloud.CloudType));
            vSqlURL = sTool.HttpGet(vSqlURL);

            if (vSqlURL.Length < 2)
            {
                return -1;
            }

            return 1;
        }

        //This is a static method that takes a string as an argument and returns a string. The method searches a list of CloudProperties objects (vCloudList) for an object
        // with a CloudName property that matches the argument (vCurrCloudName). If a match is found, the method returns the CloudType property of the matching object.
        public static string getCloudType(string vCurrCloudName)
        {
            CloudProperties vCurrCloud = vCloudList.Find(item => item.CloudName == vCurrCloudName);
            return vCurrCloud.CloudType;
        }

        //This method is used to get a list of cloud tables from a given cloud name. It takes in a string parameter, vCurrCloudName, which is the name of the cloud. It then
        // finds the CloudProperties object associated with the given cloud name in the vCloudList list. It then uses the prepareCloudQuery method to create a SQL URL for the
        // given cloud type. Finally, it returns a list of cloud objects from the given SQL URL using the getCloudObjectList method.
        public static IEnumerable<CloudObjects> getCloudTableList(string vCurrCloudName)
        {
            CloudProperties vCurrCloud = vCloudList.Find(item => item.CloudName == vCurrCloudName);
            string vSqlURL;

            vSqlURL = prepareCloudQuery(vCurrCloudName, sLibrary.getCloudSqlTable(vCurrCloud.CloudType));

            return getCloudObjectList(vSqlURL);

        }

        //This method is used to get a list of cloud views from a given cloud name. It first finds the cloud properties of the given cloud name from the vCloudList. Then it
        // prepares a cloud query using the cloud name and the cloud type from the cloud properties. Finally, it returns a list of cloud objects from the cloud query.
        public static IEnumerable<CloudObjects> getCloudViewList(string vCurrCloudName)
        {
            CloudProperties vCurrCloud = vCloudList.Find(item => item.CloudName == vCurrCloudName);
            string vSqlURL;

            vSqlURL = prepareCloudQuery(vCurrCloudName, sLibrary.getCloudSqlView(vCurrCloud.CloudType));

            return getCloudObjectList(vSqlURL);

        }

        //This code is a private static method that returns an IEnumerable of CloudObjects. It takes a string parameter, vSqlURL, which is used to get an array of objects from
        // an HTTP request. The code then iterates through the array of objects, creating a new CloudObjects object for each one. The Name property of the CloudObjects object
        // is set to the object from the array, with any quotation marks removed and trimmed. The idTbl property is set to a variable, vIdtbl, which is incremented each time
        // the loop runs. Finally, the CloudObjects object is yielded, which adds it to the IEnumerable that is returned.
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
                vObj.Name = vCurrObj.ToString().Replace('"', ' ').Trim();
                vObj.idTbl = vIdtbl;
                vIdtbl = vIdtbl + 1;
                yield return vObj;
            }
        } 

        //This is a method that returns an IEnumerable of ObjectsAndProperties. It takes two parameters, a cloud name and an object name. It first finds the CloudProperties
        // object associated with the cloud name, then prepares a query to get the columns associated with the object name. It then creates a new ObjectsAndProperties object
        // and adds the columns to it. Finally, it yields the ObjectsAndProperties object.
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


        // This code is a method that is used to retrieve a list of cloud properties from the registry. It iterates through the registry values and checks if the
        // name contains the word "Cloud". If it does, it splits the name into an array of strings and checks if the length of the array is greater than 2. If it is, it yields
        // the cloud properties.  

        public static IEnumerable<CloudProperties> getCloudList()
        {
            RegistryKey vCurrRegKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
            string vCurrName = "";
            string vPrevName = "";
            if (vCurrRegKey != null)
            {
                CloudProperties vCloudProperties = new CloudProperties();

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

                        if (vCloudProperties.Url != null)
                            if (vCloudProperties.Password != null)
                                if (vCloudProperties.Login.Length > 0)
                                {
                                    vPrevName = "";
                                    yield return vCloudProperties;
                                }
                    }
                    else
                    {
                        if (vPrevName.Length > 2)
                        {
                            vPrevName = "";
                        }
                    }

                }
            }
        } 
    }
}
