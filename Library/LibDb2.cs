
namespace SqlEngine
{
    class LibDb2
    { 
        public static string getDb2Fuctions()
        {
            return @"  SELECT distinct FUNCTION_SCHEMA  || '.' || FUNCTION_NAME  AS value   FROM  INFORMATION_SCHEMA.FUNCTIONS order by 1  ";
        }
         
        public static string getDb2Views()
        {
            return @"  SELECT  distinct table_schema || '.' || table_name as value  FROM   SYSIBM.tables   WHERE 1=1  and table_type = 'VIEW'  order by 1 ";
        }

        public static string getDb2Tables()
        {
            return @" SELECT  distinct table_schema || '.' || table_name  as value  FROM   SYSIBM.tables   WHERE 1=1  and table_type = 'BASE TABLE'  order by 1;";
        }

        public static string getDb2Columns()
        {
            return @"   select name || ' | ' coltype  as value FROM   SYSIBM.SYSCOLUMNS  where tbcreator ||  '.' || tbname  in (  '%TNAME%' )  order by 1  ";
        }

        public static string getDb2Indexes()
        {
            return @"    select indname as value from syscat.indexes where tabname  ||'.'|| tabschema in ('%TNAME%') order by 1   ";
        }


        public static string getDb2Procedurees()
        {
            return @" select distinct specific_schema || '.' || specific_name value from SYSIBM.ROUTINES   where routine_type='PROCEDURE' order by 1   ";
        }
 

    }
}
