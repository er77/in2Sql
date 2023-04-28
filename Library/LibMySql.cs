 

namespace SqlEngine
{
    class LibMySql
    {
      
        public static string getMySqlViews()
        {
            return @" SELECT  distinct  CONCAT(table_schema ,  '.' , table_name) as value   FROM information_schema.VIEWS order by 1 ";
        }

        public static string getMySqlTables()
        {
            return @" SELECT distinct  CONCAT(table_schema ,  '.' , table_name) as value FROM information_schema.tables order by 1 ;";
        }

        public static string getMySqlColumns()
        {
            return @" SELECT CONCAT(column_name ,  ' | ' , data_type )  as value    FROM information_schema.COLUMNS  where CONCAT(table_schema ,  '.' , table_name)   in (  '%TNAME%' )  ";
        }

        public static string getMySqlIndexes()
        {
            return @" SELECT distinct index_name as  value FROM information_schema.STATISTICS  where CONCAT(table_schema ,  '.' , table_name)   in ('%TNAME%') order by 1   ";
        }

        public static string getMySqlProcedurees()
        {
            return @" SELECT    distinct  CONCAT(routine_schema ,  '.' , routine_name )  as value  FROM     information_schema.routines     where routine_type='PROCEDURE' order by 1 ";
        }

        public static string getMySqlFuctions()
        {
            return @" SELECT    distinct  CONCAT(routine_schema ,  '.' , routine_name ) as value FROM     information_schema.routines     where routine_type='FUNCTION' order by 1";
        } 
  

    }
}
