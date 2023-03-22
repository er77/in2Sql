using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlEngine
{
    internal class libPgSql
    {

        public static string getPgSqlViews()
        {
            return @" SELECT distinct schemaname || '.' || viewname as  value FROM pg_catalog.pg_views WHERE 1=1  order by 1 ";
        }

        public static string getPgSqlTables()
        {
            return @" SELECT distinct schemaname || '.' || tablename as  value FROM pg_catalog.pg_tables WHERE 1=1 order by 1 ";
        }
        public static string getPgSqlColumns()
        {
            return @" SELECT  Column_Name || ' | ' || Data_type as  value FROM information_schema.columns  WHERE table_schema || '.' ||  table_name  in (  '%TNAME%' )   ";
        }

        public static string getPgSqlIndexes()
        {
            return @" SELECT  indexname as  value FROM pg_indexes WHERE schemaname || '.' ||  tablename  in ('%TNAME%')   ";
        }


        public static string getPgSqlProcedures()
        {
            return @" select distinct  n.nspname || '.' ||   p.proname as value   from pg_proc p left join pg_namespace n on p.pronamespace = n.oid order by 1 ";
        }

        public static string getPgSqlFuctions()
        {
            return @" select distinct specific_schema || '.' || specific_name as value from information_schema.routines order by 1 ";
        }

    }
}
