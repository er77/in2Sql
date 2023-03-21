using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlEngine
{
    class svcCsv
    {

        public struct CloudObjects
        {
            public String Name;
            public int idTbl;
        }

        public static int vIdtbl = 0;

        public struct FilesAndProperties
        {
            public List<String> objColumns;
            public String ObjName;
        }

        public struct FolderProperties
        {
            public string FolderName, Path ; 
            public List<CloudObjects> Files;

        }

        public static List<FolderProperties> vFolderList = FolderList();

        public static List<FilesAndProperties> vFileObjProp = new List<FilesAndProperties>();

        public static string getFirstFolder ()
        {
            if (vFolderList != null )
              return vFolderList[0].Path + "\\";

            return "c:\\Temp\\";
        }

        public static List<FolderProperties> FolderList()
        {
            try
            {
                List<FolderProperties> listClooudProperties = new List<FolderProperties>();
                listClooudProperties.AddRange(getCsvList());
                return listClooudProperties;
            }
            catch (Exception e)
            {
                svcTool.ExpHandler(e, "CloudList");
                return null;
            }
        } 

        public static IEnumerable<CloudObjects> getFileList(string vCurrFolderName)
        {
            FolderProperties vCurrFolderN = vFolderList.Find(item => item.FolderName == vCurrFolderName);       

            return getFiesinFolderList(vCurrFolderN.Path);

        } 

        private static IEnumerable<CloudObjects> getFiesinFolderList(string vFolderPath)
        {
            DirectoryInfo d = new DirectoryInfo(@vFolderPath);
            FileInfo[] Files = d.GetFiles("*.csv"); 
            string str = "";
            foreach (FileInfo file in Files)
            {
                str = str + ", " + file.Name;
                CloudObjects vObj = new CloudObjects();
                vObj.Name = file.Name;
                vObj.idTbl = vIdtbl;
                vIdtbl = vIdtbl + 1;
                yield return vObj;
            } 
        }


        public static IEnumerable<FilesAndProperties> getCsvFileColumn(string vCurrFolderName, string vObjName)
        {
            FolderProperties vCurrFolderN = vFolderList.Find(item => item.FolderName == vCurrFolderName);

            FilesAndProperties vObject = new FilesAndProperties();
            vObject.ObjName = vCurrFolderName + '.' + vObjName;
            vObject.objColumns = new List<string>();

            using (TextFieldParser csvReader = new TextFieldParser( vCurrFolderN.Path + "\\" + vObjName ))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();
                foreach (string column in colFields)
                {
                    vObject.objColumns.Add(column.ToString().Replace('"', ' ').Trim());
                }           
            }

            yield return vObject; 

        }


        public static IEnumerable<FolderProperties> getCsvList()
        {
            RegistryKey vCurrRegKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
            string vCurrName = "";
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
                        vCurrName = vNameDetails[1];

                        if (!vCurrName.Equals(vPrevName))
                        {
                            if (vPrevName.Length > 2)
                                yield return vFolderProp;

                            vFolderProp = new FolderProperties();
                        }

                        vPrevName = vCurrName;

                        vFolderProp.FolderName = vCurrName; 

                        string vCurrRegValue = svcRegistry.getLocalRegValue(vCurrRegKey, name);

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
