using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlEngine
{
    class svcSqlLibrary
    {
        public static string getDBType(string vDataSource, string vDriver)
        {
            string vDBType = "";
            if (vDataSource.ToUpper().Contains("MSSQL")) vDBType = "MSSQL";
            else if (vDriver.ToUpper().Contains("SQLSRV")) vDBType = "MSSQL";
            else if (vDriver.ToUpper().Contains("VERTICA")) vDBType = "VERTICA";
            else if (vDriver.ToUpper().Contains("ORACLE")) vDBType = "ORACLE";
            else if (vDriver.ToUpper().Contains("SQORA")) vDBType = "ORACLE";
            else if (vDriver.ToUpper().Contains("PSQL")) vDBType = "PGSQL";
            else if (vDriver.ToUpper().Contains("MYSQL")) vDBType = "MYSQL";
            else if (vDriver.ToUpper().Contains("MYODBC")) vDBType = "MYSQL";
            else if (vDriver.ToUpper().Contains("IBM")) vDBType = "DB2";
            else if (vDriver.ToUpper().Contains("DB2")) vDBType = "DB2";
            else if (vDriver.ToUpper().Contains("CLICKHOUSE")) vDBType = "CH";
            else if (vDriver.ToUpper().Contains("SNOWFLAKE")) vDBType = "SNOWFLAKE";

            return vDBType;
        }


        public static string getSqlViews(string vTypeDB)  //In2SqlLibrary.getSqlViews
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = getMsSqlViews();
            else if (vTypeDB.Contains("VERTICA")) vResult = getVerticaViews();
            else if (vTypeDB.Contains("ORACLE")) vResult = getOracleViews();
            else if (vTypeDB.Contains("PGSQL")) vResult = getPgSqlViews();
            else if (vTypeDB.Contains("MYSQL")) vResult = getMySqlViews();
            else if (vTypeDB.Contains("DB2")) vResult = getDb2Views();
            else if (vTypeDB.Contains("CH")) vResult = getCHViews();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = getSnowViews();
            return vResult;
        }

        public static string getSqlTables(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = getMsSqlTables();
            else if (vTypeDB.Contains("VERTICA")) vResult = getVerticaTables();
            else if (vTypeDB.Contains("ORACLE")) vResult = getOracleTables();
            else if (vTypeDB.Contains("PGSQL")) vResult = getPgSqlTables();
            else if (vTypeDB.Contains("MYSQL")) vResult = getMySqlTables();
            else if (vTypeDB.Contains("DB2")) vResult = getDb2Tables();
            else if (vTypeDB.Contains("CH")) vResult = getCHTables();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = getSnowTables();
            return vResult;
        }

        public static string getSQLProgramms(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = getMsSqlProcedurees();
            else if (vTypeDB.Contains("VERTICA")) vResult = getVerticaDummy();
            else if (vTypeDB.Contains("ORACLE")) vResult = getOracleProcedurees();
            else if (vTypeDB.Contains("PGSQL")) vResult = getPgSqlProcedures();
            else if (vTypeDB.Contains("MYSQL")) vResult = getMySqlProcedurees();
            else if (vTypeDB.Contains("DB2")) vResult = getDb2Procedurees();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = getSnowProcedures();
            else if (vTypeDB.Contains("CH")) vResult = getMsSqlDummy();
            return vResult;
        }

        public static string getSQLFunctions(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = getMsSqlFuctions();
            else if (vTypeDB.Contains("VERTICA")) vResult = getVerticaDummy();
            else if (vTypeDB.Contains("ORACLE")) vResult = getOracleFuctions();
            else if (vTypeDB.Contains("PGSQL")) vResult = getPgSqlFuctions();
            else if (vTypeDB.Contains("MYSQL")) vResult = getMySqlFuctions();
            else if (vTypeDB.Contains("DB2")) vResult = getDb2Fuctions();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = getSnowFuctions();
            else if (vTypeDB.Contains("CH")) vResult = getMsSqlDummy();
            return vResult;
        }

        public static string getSQLTableColumn(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = getMsSqlColumns();
            else if (vTypeDB.Contains("VERTICA")) vResult = getVerticaColumns();
            else if (vTypeDB.Contains("ORACLE")) vResult = getOracleColumns();
            else if (vTypeDB.Contains("PGSQL")) vResult = getPgSqlColumns();
            else if (vTypeDB.Contains("MYSQL")) vResult = getMySqlColumns();
            else if (vTypeDB.Contains("DB2")) vResult = getDb2Columns();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = getSnowColumns();
            else if (vTypeDB.Contains("CH")) vResult = getCHTablesColumns();
            return vResult;
        }

        public static string getSQLIndexes(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = getMsSqlIndexes();
            else if (vTypeDB.Contains("VERTICA")) vResult = getVerticaIndexes();
            else if (vTypeDB.Contains("ORACLE")) vResult = getOracleIndexes();
            else if (vTypeDB.Contains("PGSQL")) vResult = getPgSqlIndexes();
            else if (vTypeDB.Contains("MYSQL")) vResult = getMySqlIndexes();
            else if (vTypeDB.Contains("DB2")) vResult = getDb2Indexes();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = getSnowIndexes();
            else if (vTypeDB.Contains("CH")) vResult = getMsSqlDummy();
            return vResult;
        }

        public static string getSQLDependencies(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = getMsSqlDummy();
            else if (vTypeDB.Contains("VERTICA")) vResult = getVerticaDummy();
            else if (vTypeDB.Contains("ORACLE")) vResult = getOracleDependenies();
            else if (vTypeDB.Contains("PGSQL")) vResult = getVerticaDummy();
            else if (vTypeDB.Contains("MYSQL")) vResult = getVerticaDummy();
            else if (vTypeDB.Contains("DB2")) vResult = getVerticaDummy();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = getSnowDummy();
            else if (vTypeDB.Contains("CH")) vResult = getMsSqlDummy();
            return vResult;
        }


        public static string getCloudSqlCheck(string vTypeCloudB)
        {
            string vResult = "";
            if (vTypeCloudB.Contains("CloudCH")) vResult = "select * from system.databases";
            return vResult;
        }

        public static string getCloudSqlView(string vTypeCloudB)
        {
            string vResult = "";
            if (vTypeCloudB.Contains("CloudCH")) vResult = getCHViews();
            return vResult;
        }

        public static string getCHViews()
        {
            return @"SELECT distinct database ||'.'|| name value FROM system.tables where engine = 'View' ";
        }


        public static string getCloudSqlTable(string vTypeCloudB)
        {
            string vResult = "";
            if (vTypeCloudB.Contains("CloudCH")) vResult = getCHTables();
            return vResult;
        }

        public static string getCHTables()
        {
            return @"SELECT distinct database ||'.'|| name value FROM system.tables where engine <> 'View' ";
        }

        public static string getCHTablesColumns()
        {
            return @"SELECT name value  FROM system.columns where  database in ('%TOWNER%') and table  = '%TNAME%'";
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


        public static string getErrConType(string vErroMsg)
        {
            string vResult = "";
            if (vErroMsg.Contains("Login fails")) vResult = "LoginErr";
            return vResult;
        }

        public static string getVerticaDummy()
        {
            return @" select ''  as  value from dual ";
        }

        public static string getSnowDummy()
        {
            return @" select ''  as   value  ";
        }

        public static string getMsSqlDummy()
        {
            return @" select ''  as  value  ";
        }

        public static string getMsSqlViews()
        {
            return @"SELECT distinct d.[name] + '.' + v.[name] value 
                         FROM  sys.objects as v  
                       left join sys.schemas d on 1=1
                            and d.schema_id = v.schema_id   
                       where type = 'V'   
                      order by 1 ";
        }

        public static string getMsSqlTables()
        {
            return @"SELECT distinct d.[name] + '.' + v.[name] value 
                         FROM  sys.objects as v  
                       left join sys.schemas d on 1=1
                            and d.schema_id = v.schema_id   
                       where type = 'U'   
                      order by 1 ";
        }

        public static string getMsSqlColumns()
        {
            return @"SELECT 
                           Column_Name + ' | '+ Data_type value 
                        FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE TABLE_SCHEMA +'.'+ TABLE_NAME = N'%TNAME%'  ";
        }

        public static string getMsSqlIndexes()
        {
            return @" SELECT 
                            ind.name +' ('+ STRING_AGG ( col.name ,', ') +')'  value
                        FROM 
                             sys.indexes ind  
                       INNER JOIN 
                           sys.index_columns ic ON  ind.object_id = ic.object_id and ind.index_id = ic.index_id 
                       INNER JOIN 
                           sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id 
                        INNER JOIN 
                             sys.tables t ON ind.object_id = t.object_id 
                        left join sys.schemas d on 1=1
                             and d.schema_id = t.schema_id
                        WHERE 
                            1=1 
	                        and ind.name  is not null 
                           and d.name + '.' +t.name  in ('%TNAME%')  
							group by  ind.name

                      union all 
                      SELECT
                            c.CONSTRAINT_NAME + ' : '+ cu.COLUMN_NAME +' ('+ ku.TABLE_NAME +' -> ' + ku.COLUMN_NAME + ')' value
	                    FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
	                      INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
                            ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                          INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
                            ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
	                    where 1=1 
	                     and c.CONSTRAINT_SCHEMA + '.' +cu.TABLE_NAME   in ('%TNAME%')
                     order by 1  
                        ";
        }


        public static string getMsSqlProcedurees()
        {
            return @"SELECT distinct type ,  d.[name] + '.' + v.[name] value 
                         FROM  sys.objects as v  
                       left join sys.schemas d on 1=1
                            and d.schema_id = v.schema_id   
                       where type in ( 'P'   )
                      order by 1 desc,2 asc ";
        }

        public static string getMsSqlFuctions()
        {
            return @"SELECT distinct type ,  d.[name] + '.' + v.[name] value 
                         FROM  sys.objects as v  
                       left join sys.schemas d on 1=1
                            and d.schema_id = v.schema_id   
                       where type in (   'TF','FN'  )
                      order by 1 desc,2 asc ";
        }

        public static string getMsSqlReserved()
        {
            return @"\b(ADD|ALL|ALTER|AND|ANY|AS|ASC|AUTHORIZATION|BACKUP|BEGIN|BETWEEN|BREAK|BROWSE|BULK|BY|CASCADE|CASE|CHECK|CHECKPOINT|CLOSE|CLUSTERED|COALESCE|COLLATE|COLUMN|COMMIT|COMPUTE|CONSTRAINT|CONTAINS|CONTAINSTABLE|CONTINUE|CONVERT|CREATE|CROSS|CURRENT|CURRENT_DATE|CURRENT_TIME|CURRENT_TIMESTAMP|CURRENT_USER|CURSOR|DATABASE|DBCC|DEALLOCATE|DECLARE|DEFAULT|DELETE|DENY|DESC|DISK|DISTINCT|DISTRIBUTED|DOUBLE|DROP|DUMP|ELSE|END|ERRLVL|ESCAPE|EXCEPT|EXEC|EXECUTE|EXISTS|EXIT|EXTERNAL|FETCH|FILE|FILLFACTOR|FOR|FOREIGN|FREETEXT|FREETEXTTABLE|FROM|FULL|FUNCTION|GOTO|GRANT|GROUP|HAVING|HOLDLOCK|IDENTITY|IDENTITY_INSERT|IDENTITYCOL|IF|IN|INDEX|INNER|INSERT|INTERSECT|INTO|IS|JOIN|KEY|KILL|LEFT|LIKE|LINENO|LOAD|MERGE|NATIONAL|NOCHECK|NONCLUSTERED|NOT|NULL|NULLIF|OF|OFF|OFFSETS|ON|OPEN|OPENDATASOURCE|OPENQUERY|OPENROWSET|OPENXML|OPTION|OR|ORDER|OUTER|OVER|PERCENT|PIVOT|PLAN|PRECISION|PRIMARY|PRINT|PROC|PROCEDURE|PUBLIC|RAISERROR|READ|READTEXT|RECONFIGURE|REFERENCES|REPLICATION|RESTORE|RESTRICT|RETURN|REVERT|REVOKE|RIGHT|ROLLBACK|ROWCOUNT|ROWGUIDCOL|RULE|SAVE|SCHEMA|SECURITYAUDIT|SELECT|SEMANTICKEYPHRASETABLE|SEMANTICSIMILARITYDETAILSTABLE|SEMANTICSIMILARITYTABLE|SESSION_USER|SET|SETUSER|SHUTDOWN|SOME|STATISTICS|SYSTEM_USER|TABLE|TABLESAMPLE|TEXTSIZE|THEN|TO|TOP|TRAN|TRANSACTION|TRIGGER|TRUNCATE|TRY_CONVERT|TSEQUAL|UNION|UNIQUE|UNPIVOT|UPDATE|UPDATETEXT|USE|USER|VALUES|VARYING|VIEW|WAITFOR|WHEN|WHERE|WHILE|WITH|WITHINGROUP|WRITETEXT)\b";
        }

        public static string getVerticaTables()
        {
            return @"select distinct table_schema || '.' || table_name value                                                          
                        from v_catalog.tables 
                        order by 1 ";
        }

        public static string getVerticaViews()
        {
            return @"select distinct table_schema || '.' || table_name value                                                          
                        from v_catalog.views 
                            order by 1 ";
        }


        public static string getVerticaColumns()
        {
            return @" select  column_name || ' | ' ||  data_type value
                         from v_catalog.columns
                    where  table_schema || '.' ||  table_name  in (  '%TNAME%' )
                       order by 1 ";
        }

        public static string getVerticaIndexes()
        {
            return @" select  column_name  value
                         from v_catalog.primary_keys
                    where  table_schema || '.' ||  table_name  in ('%TNAME%') 
                         order by 1  ";
        }


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

        public static string getPgSqlViews()
        {
            return @" SELECT distinct schemaname || '.' || viewname as  value FROM pg_catalog.pg_views WHERE 1=1  order by 1 ";
        }

        public static string getSnowViews()
        {
            return @"  SELECT distinct TABLE_SCHEMA || '.' || TABLE_NAME as  value FROM INFORMATION_SCHEMA.views WHERE 1=1  order by 1 ";
        }


        public static string getPgSqlTables()
        {
            return @" SELECT distinct schemaname || '.' || tablename as  value FROM pg_catalog.pg_tables WHERE 1=1 order by 1 ";
        }

        public static string getSnowTables()
        {
            return @"  SELECT distinct TABLE_SCHEMA || '.' || TABLE_NAME as  value FROM INFORMATION_SCHEMA.tables WHERE TABLE_TYPE NOT IN ('VIEW')   order by 1 ";
        }


        public static string getPgSqlColumns()
        {
            return @" SELECT  Column_Name || ' | ' || Data_type as  value FROM information_schema.columns  WHERE table_schema || '.' ||  table_name  in (  '%TNAME%' )   ";
        }

        public static string getSnowColumns()
        {
            return @" SELECT  Column_Name || ' | ' || Data_type as  value FROM information_schema.columns  WHERE table_schema || '.' ||  table_name  in (  '%TNAME%' )   ";
        }

        public static string getSnowIndexes()
        {
            return @" SELECT  '' as  value   ";
        }

        public static string getPgSqlIndexes()
        {
            return @" SELECT  indexname as  value FROM pg_indexes WHERE schemaname || '.' ||  tablename  in ('%TNAME%')   ";
        }


        public static string getPgSqlProcedures()
        {
            return @" select distinct  n.nspname || '.' ||   p.proname as value   from pg_proc p left join pg_namespace n on p.pronamespace = n.oid order by 1 ";
        }

        public static string getSnowProcedures()
        {
            return @"  SELECT distinct PROCEDURE_SCHEMA || '.' || PROCEDURE_NAME  AS value   FROM INFORMATION_SCHEMA.PROCEDURES  order by 1 ";
        }


        public static string getDb2Fuctions()
        {
            return @"  SELECT distinct FUNCTION_SCHEMA  || '.' || FUNCTION_NAME  AS value   FROM  INFORMATION_SCHEMA.FUNCTIONS order by 1  ";
        }


        public static string getPgSqlFuctions()
        {
            return @" select distinct specific_schema || '.' || specific_name as value from information_schema.routines order by 1 ";
        }



        public static string getSnowFuctions()
        {
            return @" SELECT distinct FUNCTION_SCHEMA  || '.' || FUNCTION_NAME  AS value   FROM  INFORMATION_SCHEMA.FUNCTIONS  order by 1 ";
        }

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

        public static string getCloudColumns(string vTypeCloudB)
        {
            string vResult = "";
            if (vTypeCloudB.Contains("CloudCH")) vResult = getCHTablesColumns();
            return vResult;
        }
 
 

 




        /*    public static string getDb2Fuctions()
            {
                return @"  select distinct specific_schema || '.' || specific_name value from SYSIBM.ROUTINES  where routine_type='FUNCTION' order by 1";
            }
        */

    }
}
