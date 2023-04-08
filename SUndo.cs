using System;
using System.Collections.Generic;

namespace SqlEngine
{
    class SUndo
    {
        public struct SqlActionTableList
        {            
            public List<string> UndoList;
            public string TableName;
        }

        private static List<SqlActionTableList> _undoTableLists  = new List<SqlActionTableList>();
        private static List<SqlActionTableList> _redoTableLists = new List<SqlActionTableList>();


        public static List<String>  GetUndoList (string vTableName )
        {    
            var vind = _undoTableLists.FindIndex(item => item.TableName == vTableName);
               return _undoTableLists[vind].UndoList  ;           
        }

        private static SqlActionTableList NewUndoRecord (String vTableName, string vSqlCommand)
        {
            SqlActionTableList actionTableList;
            actionTableList.TableName = vTableName;
            actionTableList.UndoList = new List<String>
                {
                    vSqlCommand
                };
            return actionTableList;
        }

        private static void AddToRedoList (string vTableName,string vSqlCommand )
        { 

            if (_redoTableLists.Count < 0)
            {       
                _redoTableLists.Add(NewUndoRecord(vTableName, vSqlCommand));                 
                    return ;
            }

           int  vintRedo = _redoTableLists.FindIndex(item => item.TableName == vTableName);
            if (vintRedo < 0 )
            {
                _redoTableLists.Add(NewUndoRecord(vTableName, vSqlCommand));
                return;
            }
            _redoTableLists[vintRedo].UndoList.Add(vSqlCommand);           
        }

        public static void AddToUndoList(String vTableName, string vSqlCommand)
        {

            if (_undoTableLists.Count < 0)
            {
                _undoTableLists.Add(NewUndoRecord(vTableName, vSqlCommand));
                return;
            }

            int vintRedo = _undoTableLists.FindIndex(item => item.TableName == vTableName);
            if (vintRedo < 0)
            {
                _undoTableLists.Add(NewUndoRecord(vTableName, vSqlCommand));
                return;
            }
            _undoTableLists[vintRedo].UndoList.Add(vSqlCommand);
        }
         

        public static string GetLastSqlActionUndo (String vTableName)
        {
            string vResult;
            int vintUndo;

            if (_undoTableLists.Count > 0)
            {
                vintUndo = _undoTableLists.FindIndex(item => item.TableName == vTableName);
                if (vintUndo < 0)
                    return null;
            }
            else
                return null; 

            int vIdLastSql = _undoTableLists[vintUndo].UndoList.Count - 1;
            if (vIdLastSql < 0 )
                return null;

            vResult = _undoTableLists[vintUndo].UndoList[vIdLastSql];
            _undoTableLists[vintUndo].UndoList.RemoveAt(vIdLastSql);
            AddToRedoList(vTableName, vResult);

            return vResult;
        }

        public static string GetLastSqlActionRedo(String vTableName)
        {
            string vResult;
            int vintRedo;

            if (_redoTableLists.Count > 0)
            {
                vintRedo = _redoTableLists.FindIndex(item => item.TableName == vTableName);
                if (vintRedo < 0)
                    return null;
            }
            else
                return null;

            int vIdLastSql = _redoTableLists[vintRedo].UndoList.Count-1 ;
            if (vIdLastSql < 0)
                return null;

            vResult = _redoTableLists[vintRedo].UndoList[vIdLastSql];
            _redoTableLists[vintRedo].UndoList.RemoveAt(vIdLastSql);        

            return vResult;
        }
    }
}
