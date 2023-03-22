using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlEngine
{
    internal class SqlOracle
    {

        public static string getOracleViews()
        {
            return @" SELECT (SELECT SYS_CONTEXT('USERENV','CURRENT_SCHEMA') FROM DUAL) ||'.'|| view_name value FROM user_views order by 1 ";
        }

        public static string getOracleTables()
        {
            return @" SELECT (SELECT SYS_CONTEXT('USERENV','CURRENT_SCHEMA') FROM DUAL) ||'.'|| table_name value FROM user_tables order by 1";
        }

        public static string getOracleColumns()
        {
            return @" SELECT Column_Name || ' | ' || Data_type value FROM user_tab_cols WHERE   TABLE_NAME =  '%TNAME%' ";
        }

        public static string getOracleIndexes()
        {
            return @"  SELECT  index_name value FROM user_indexes  WHERE   TABLE_NAME in ('%TNAME%') order by 1 ";
        }


        public static string getOracleProcedurees()
        {
            return @" SELECT object_name value FROM USER_OBJECTS WHERE OBJECT_TYPE IN ( 'PROCEDURE','PACKAGE') order by 1 ";
        }

        public static string getOracleFuctions()
        {
            return @" SELECT object_name value FROM USER_OBJECTS WHERE OBJECT_TYPE IN ('FUNCTION') order by 1 ";
        }


        public static string getOracleDependenies()
        {
            return @" select 
                        o1.owner ||'.' || o1.object_name value 
                      from public_dependency d 
                        left join all_objects o1 on 1=1 
                          and d.object_id = o1.object_id 
                        left join  all_objects o2 on 1=1 
                          and d.referenced_object_id = o2.object_id
                      where 1=1 
                         and o2.owner in ('%TOWNER%')
                         and o2.object_name in ('%TNAME%')
                       ";
        }

    }
}
