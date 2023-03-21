using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlEngine
{
    class sUndo
    {
        public struct SqlActionTableList
        {            
            public List<String> UndoList;
            public String TableName;
        }

        public static List<SqlActionTableList> vUndoTableList  = new List<SqlActionTableList>();
        public static List<SqlActionTableList> vRedoTableList = new List<SqlActionTableList>();


        public static List<String>  getUndoList (String vTableName )
        {    
            int vind = vUndoTableList.FindIndex(item => item.TableName == vTableName);
               return vUndoTableList[vind].UndoList  ;           
        }

        public static SqlActionTableList newUndoRecord (String vTableName, string vSqlCommand)
        {
            SqlActionTableList vnewRecord;
            vnewRecord.TableName = vTableName;
            vnewRecord.UndoList = new List<String>
                {
                    vSqlCommand
                };
            return vnewRecord;
        }

        private static void addToRedoList (String vTableName,string vSqlCommand )
        { 

            if (vRedoTableList.Count < 0)
            {       
                vRedoTableList.Add(newUndoRecord(vTableName, vSqlCommand));                 
                    return ;
            }

           int  vintRedo = vRedoTableList.FindIndex(item => item.TableName == vTableName);
            if (vintRedo < 0 )
            {
                vRedoTableList.Add(newUndoRecord(vTableName, vSqlCommand));
                return;
            }
            vRedoTableList[vintRedo].UndoList.Add(vSqlCommand);           
        }

        public static void addToUndoList(String vTableName, string vSqlCommand)
        {

            if (vUndoTableList.Count < 0)
            {
                vUndoTableList.Add(newUndoRecord(vTableName, vSqlCommand));
                return;
            }

            int vintRedo = vUndoTableList.FindIndex(item => item.TableName == vTableName);
            if (vintRedo < 0)
            {
                vUndoTableList.Add(newUndoRecord(vTableName, vSqlCommand));
                return;
            }
            vUndoTableList[vintRedo].UndoList.Add(vSqlCommand);
        }
         

        public static string getLastSqlActionUndo (String vTableName)
        {
            string vResult;
            int vintUndo;

            if (vUndoTableList.Count > 0)
            {
                vintUndo = vUndoTableList.FindIndex(item => item.TableName == vTableName);
                if (vintUndo < 0)
                    return null;
            }
            else
                return null; 

            int vIdLastSql = vUndoTableList[vintUndo].UndoList.Count - 1;
            if (vIdLastSql < 0 )
                return null;

            vResult = vUndoTableList[vintUndo].UndoList[vIdLastSql];
            vUndoTableList[vintUndo].UndoList.RemoveAt(vIdLastSql);
            addToRedoList(vTableName, vResult);

            return vResult;
        }

        public static string getLastSqlActionRedo(String vTableName)
        {
            string vResult;
            int vintRedo;

            if (vRedoTableList.Count > 0)
            {
                vintRedo = vRedoTableList.FindIndex(item => item.TableName == vTableName);
                if (vintRedo < 0)
                    return null;
            }
            else
                return null;

            int vIdLastSql = vRedoTableList[vintRedo].UndoList.Count-1 ;
            if (vIdLastSql < 0)
                return null;

            vResult = vRedoTableList[vintRedo].UndoList[vIdLastSql];
            vRedoTableList[vintRedo].UndoList.RemoveAt(vIdLastSql);        

            return vResult;
        }
    }
}
