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
    class sCsv
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
            public string FolderName, Path;
            public List<CloudObjects> Files;

        }

        public static List<FolderProperties> vFolderList = FolderList();

        public static List<FilesAndProperties> vFileObjProp = new List<FilesAndProperties>();

        public static string getFirstFolder()
        {
            if (vFolderList != null)
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
                sTool.ExpHandler(e, "CloudList");
                return null;
            }
        }
        //Bug: The method does not have a return statement in the catch block. If an exception is thrown, the method will return null, which may not be the desired behavior
        //.

        public static IEnumerable<CloudObjects> getFileList(string vCurrFolderName)
        {
            FolderProperties vCurrFolderN = vFolderList.Find(item => item.FolderName == vCurrFolderName);

            return getFilesinFolderList(vCurrFolderN.Path);

        }

        //This code snippet is a method that returns a list of files in a given folder path. It uses the DirectoryInfo class to get the list of files in the folder path, and
        // then iterates through the list of files using a foreach loop. For each file, it creates a CloudObjects object and assigns the file name to the Name property of the
        // object. It also assigns a unique idTbl value to the object. Finally, it yields the object, which adds it to the list of files.
        private static IEnumerable<CloudObjects> getFilesinFolderList(string vFolderPath)
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


        //This code snippet is a method that reads a CSV file and returns the columns of the file. It takes two parameters, the current folder name and the object name. It
        // then creates a FolderProperties object and a FilesAndProperties object. It then uses a TextFieldParser to read the fields of the CSV file and adds each column to
        // the objColumns list of the FilesAndProperties object. Finally, it returns the FilesAndProperties object.
        public static IEnumerable<FilesAndProperties> getCsvFileColumn(string vCurrFolderName, string vObjName)
        {
            FolderProperties vCurrFolderN = vFolderList.Find(item => item.FolderName == vCurrFolderName);

            FilesAndProperties vObject = new FilesAndProperties();
            vObject.ObjName = vCurrFolderName + '.' + vObjName;
            vObject.objColumns = new List<string>();

            using (TextFieldParser csvReader = new TextFieldParser(vCurrFolderN.Path + "\\" + vObjName))
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
        //Possible bug: The variable vFolderList is not declared or initialized anywhere in the code.


        //This code is a method that retrieves a list of CSV files from the registry. It iterates through the registry values and checks if the name contains "Csv". If it does
        //, it splits the name into an array and checks if the array has two elements. If it does, it yields the folder properties. It then sets the folder name and retrie
        //ves the registry value for the current name. If the path is not null, it yields the folder properties and sets the previous name to an empty string. Otherwise, it
        // sets the previous name to an empty string if the previous name is greater than two characters.
        //Optimized

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
