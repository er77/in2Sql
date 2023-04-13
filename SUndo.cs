using System.Collections.Generic;

namespace SqlEngine
{
    internal abstract class SUndo
    {
        private struct SqlActionTableList
        {            
            public List<string> UndoList;
            public string TableName;
        }

        private static readonly List<SqlActionTableList> UndoTableLists  = new List<SqlActionTableList>();
        private static readonly List<SqlActionTableList> RedoTableLists = new List<SqlActionTableList>();


        public static List<string>  GetUndoList (string vTableName )
        {    
            var i = UndoTableLists.FindIndex(item => item.TableName == vTableName);
               return UndoTableLists[i].UndoList  ;           
        }

        private static SqlActionTableList NewUndoRecord (string vTableName, string vSqlCommand)
        {
            SqlActionTableList actionTableList;
            actionTableList.TableName = vTableName;
            actionTableList.UndoList = new List<string>
                {
                    vSqlCommand
                };
            return actionTableList;
        }

        private static void AddToRedoList (string vTableName,string vSqlCommand )
        { 

            if (RedoTableLists.Count < 0)
            {       
                RedoTableLists.Add(NewUndoRecord(vTableName, vSqlCommand));                 
                    return ;
            }

            var  vintRedo = RedoTableLists.FindIndex(item => item.TableName == vTableName);
            if (vintRedo < 0 )
            {
                RedoTableLists.Add(NewUndoRecord(vTableName, vSqlCommand));
                return;
            }
            RedoTableLists[vintRedo].UndoList.Add(vSqlCommand);           
        }

        public static void AddToUndoList(string vTableName, string vSqlCommand)
        {

            if (UndoTableLists.Count < 0)
            {
                UndoTableLists.Add(NewUndoRecord(vTableName, vSqlCommand));
                return;
            }

            var vintRedo = UndoTableLists.FindIndex(item => item.TableName == vTableName);
            if (vintRedo < 0)
            {
                UndoTableLists.Add(NewUndoRecord(vTableName, vSqlCommand));
                return;
            }
            UndoTableLists[vintRedo].UndoList.Add(vSqlCommand);
        }
         

        public static string GetLastSqlActionUndo (string vTableName)
        {
            int vintUndo;

            if (UndoTableLists.Count > 0)
            {
                vintUndo = UndoTableLists.FindIndex(item => item.TableName == vTableName);
                if (vintUndo < 0)
                    return null;
            }
            else
                return null; 

            var vIdLastSql = UndoTableLists[vintUndo].UndoList.Count - 1;
            if (vIdLastSql < 0 )
                return null;

            var vResult = UndoTableLists[vintUndo].UndoList[vIdLastSql];
            UndoTableLists[vintUndo].UndoList.RemoveAt(vIdLastSql);
            AddToRedoList(vTableName, vResult);

            return vResult;
        }

        public static string GetLastSqlActionRedo(string vTableName)
        {
            int vintRedo;

            if (RedoTableLists.Count > 0)
            {
                vintRedo = RedoTableLists.FindIndex(item => item.TableName == vTableName);
                if (vintRedo < 0)
                    return null;
            }
            else
                return null;

            var vIdLastSql = RedoTableLists[vintRedo].UndoList.Count-1 ;
            if (vIdLastSql < 0)
                return null;

            var vResult = RedoTableLists[vintRedo].UndoList[vIdLastSql];
            RedoTableLists[vintRedo].UndoList.RemoveAt(vIdLastSql);        

            return vResult;
        }
    }
}
