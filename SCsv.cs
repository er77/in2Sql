using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SqlEngine
{
    class SCsv
    {

        public struct CloudObjects
        {
            public String Name;
            public int IdTbl;
        }

        private static int _idtbl ;

        public struct FilesAndProperties
        {
            public List<String> ObjColumns;
            public String ObjName;
        }

        public struct FolderProperties
        {
            public string FolderName, Path ; 
            public List<CloudObjects> Files;

        }

        public static List<FolderProperties> FolderPropertiesList = FolderList();

        public static readonly List<FilesAndProperties> FilesAndPropertiesList = new List<FilesAndProperties>();

        public static string GetFirstFolder ()
        {
            if (FolderPropertiesList != null )
              return FolderPropertiesList[0].Path + "\\";

            return "c:\\Temp\\";
        }

        public static List<FolderProperties> FolderList()
        {
            try
            {
                List<FolderProperties> listClooudProperties = new List<FolderProperties>();
                listClooudProperties.AddRange(GetCsvList());
                return listClooudProperties;
            }
            catch (Exception e)
            {
                sTool.ExpHandler(e, "CloudList");
                return null;
            }
        } 

        public static IEnumerable<CloudObjects> GetFileList(string vCurrFolderName)
        {
            FolderProperties vCurrFolderN = FolderPropertiesList.Find(item => item.FolderName == vCurrFolderName);       

            return GetFiesInFolderList(vCurrFolderN.Path);

        } 

        private static IEnumerable<CloudObjects> GetFiesInFolderList(string vFolderPath)
        {
            DirectoryInfo d = new DirectoryInfo(@vFolderPath);
            FileInfo[] files = d.GetFiles("*.csv"); 
            string str = "";
            foreach (FileInfo file in files)
            {
                str = str + ", " + file.Name;
                CloudObjects vObj = new CloudObjects();
                vObj.Name = file.Name;
                vObj.IdTbl = _idtbl;
                _idtbl = _idtbl + 1;
                yield return vObj;
            } 
        }


        public static IEnumerable<FilesAndProperties> GetCsvFileColumn(string vCurrFolderName, string vObjName)
        {
            FolderProperties vCurrFolderN = FolderPropertiesList.Find(item => item.FolderName == vCurrFolderName);

            FilesAndProperties vObject = new FilesAndProperties();
            vObject.ObjName = vCurrFolderName + '.' + vObjName;
            vObject.ObjColumns = new List<string>();

            using (TextFieldParser csvReader = new TextFieldParser( vCurrFolderN.Path + "\\" + vObjName ))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();
                if (colFields != null)
                    foreach (string column in colFields)
                    {
                        vObject.ObjColumns.Add(column.Replace('"', ' ').Trim());
                    }
            }

            yield return vObject; 

        }


        public static IEnumerable<FolderProperties> GetCsvList()
        {
            RegistryKey vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
            string vPrevName = "";
            if (vCurrRegKey != null)
            {
                FolderProperties vFolderProp = new FolderProperties();

                foreach (string name in vCurrRegKey.GetValueNames())
                {
                    if (name.Contains("Csv"))
                    {
                        string[] vNameDetails = name.Split('.');

                        if (vNameDetails.Count() < 2)
                        {
                            MessageBox.Show("Error in reading registry getCsvList ");
                            yield return new FolderProperties();
                            break;
                        }
                        var vCurrName = vNameDetails[1];

                        if (!vCurrName.Equals(vPrevName))
                        {
                            if (vPrevName.Length > 2)
                                yield return vFolderProp;

                            vFolderProp = new FolderProperties();
                        }

                        vPrevName = vCurrName;

                        vFolderProp.FolderName = vCurrName; 

                        string vCurrRegValue = sRegistry.getLocalRegValue(vCurrRegKey, name);

                        if (name.Contains("Path"))
                            vFolderProp.Path = vCurrRegValue;

                        if (vFolderProp.Path != null) 
                                {
                                    vPrevName = "";
                                    yield return vFolderProp;
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
