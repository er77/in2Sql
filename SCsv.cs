using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;

namespace SqlEngine
{
    internal abstract class SCsv
    {

        public struct CloudObjects
        {
            public string Name;
            public int IdTbl;
        }

        private static int _idtbl ;

        public struct FilesAndProperties
        {
            public List<string> ObjColumns;
            public string ObjName;
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

            return Path.GetTempPath() ;
        }

        public static List<FolderProperties> FolderList()
        {
            try
            {
                var cloudProperties = new List<FolderProperties>();
                cloudProperties.AddRange(GetCsvList());
                return cloudProperties;
            }
            catch (Exception e)
            {
                STool.ExpHandler(e, "CloudList");
                return null;
            }
        } 

        public static IEnumerable<CloudObjects> GetFileList(string vCurrFolderName)
        {
            var vCurrFolderN = FolderPropertiesList.Find(item => item.FolderName == vCurrFolderName);       

            return GetFiesInFolderList(vCurrFolderN.Path);

        } 

        private static IEnumerable<CloudObjects> GetFiesInFolderList(string vFolderPath)
        {
            var d = new DirectoryInfo(vFolderPath);
            var files = d.GetFiles("*.csv"); 
            var str = "";
            foreach (var file in files)
            {
                str = str + ", " + file.Name;
                var vObj = new CloudObjects
                {
                    Name = file.Name,
                    IdTbl = _idtbl
                };
                _idtbl += 1;
                yield return vObj;
            } 
        }


        public static IEnumerable<FilesAndProperties> GetCsvFileColumn(string vCurrFolderName, string vObjName)
        {
            var vCurrFolderN = FolderPropertiesList.Find(item => item.FolderName == vCurrFolderName);

            var vObject = new FilesAndProperties
            {
                ObjName = vCurrFolderName + '.' + vObjName,
                ObjColumns = new List<string>()
            };

            using (var csvReader = new TextFieldParser( vCurrFolderN.Path + "\\" + vObjName ))
            {
                csvReader.SetDelimiters(",");
                csvReader.HasFieldsEnclosedInQuotes = true;
                var colFields = csvReader.ReadFields();
                if (colFields != null)
                    foreach (var column in colFields)
                    {
                        vObject.ObjColumns.Add(column.Replace('"', ' ').Trim());
                    }
            }

            yield return vObject; 

        }


        private static IEnumerable<FolderProperties> GetCsvList()
        {
            var vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
            var vPrevName = "";
            if (vCurrRegKey == null) yield break;
            var vFolderProp = new FolderProperties();

            foreach (var name in vCurrRegKey.GetValueNames())
            {
                if (name.Contains("Csv"))
                {
                    var vNameDetails = name.Split('.');

                    if (vNameDetails.Length < 2)
                    {
                        MessageBox.Show(@"Error in reading registry getCsvList ");
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

                    var vCurrRegValue = SRegistry.GetLocalRegValue(vCurrRegKey, name);

                    if (name.Contains("Path"))
                        vFolderProp.Path = vCurrRegValue;

                    if (vFolderProp.Path == null) continue;
                        
                    vPrevName = "";
                    yield return vFolderProp;
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
