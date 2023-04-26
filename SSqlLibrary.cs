namespace SqlEngine
{
    class SSqlLibrary
    {
        public static string GetDbType(string vDataSource, string vDriver)
        {
            var dbType = "";
            if (vDataSource.ToUpper().Contains("MSSQL")) dbType = "MSSQL";
            else if (vDriver.ToUpper().Contains("SQLSRV")) dbType = "MSSQL";
            else if (vDriver.ToUpper().Contains("VERTICA")) dbType = "VERTICA";
            else if (vDriver.ToUpper().Contains("ORACLE")) dbType = "ORACLE";
            else if (vDriver.ToUpper().Contains("SQORA")) dbType = "ORACLE";
            else if (vDriver.ToUpper().Contains("PSQL")) dbType = "PGSQL";
            else if (vDriver.ToUpper().Contains("MYSQL")) dbType = "MYSQL";
            else if (vDriver.ToUpper().Contains("MYODBC")) dbType = "MYSQL";
            else if (vDriver.ToUpper().Contains("IBM")) dbType = "DB2";
            else if (vDriver.ToUpper().Contains("DB2")) dbType = "DB2";
            else if (vDriver.ToUpper().Contains("CLICKHOUSE")) dbType = "CH";
            else if (vDriver.ToUpper().Contains("SNOWFLAKE")) dbType = "SNOWFLAKE";

            return dbType;
        }


        public static string GetSqlViews(string vTypeDb)  //In2SqlLibrary.getSqlViews
        {
            var result = "";
            if (vTypeDb.Contains("MSSQL")) result = GetMsSqlViews();
            else if (vTypeDb.Contains("VERTICA")) result = GetVerticaViews();
            else if (vTypeDb.Contains("ORACLE")) result = GetOracleViews();
            else if (vTypeDb.Contains("PGSQL")) result = GetPgSqlViews();
            else if (vTypeDb.Contains("MYSQL")) result = GetMySqlViews();
            else if (vTypeDb.Contains("DB2")) result = GetDb2Views();
            else if (vTypeDb.Contains("CH")) result = GetChViews();
            else if (vTypeDb.Contains("SNOWFLAKE")) result = GetSnowViews();
            return result;
        }

        public static string GetSqlTables(string vTypeDb)
        {
            var result = "";
            if (vTypeDb.Contains("MSSQL")) result = GetMsSqlTables();
            else if (vTypeDb.Contains("VERTICA")) result = GetVerticaTables();
            else if (vTypeDb.Contains("ORACLE")) result = GetOracleTables();
            else if (vTypeDb.Contains("PGSQL")) result = GetPgSqlTables();
            else if (vTypeDb.Contains("MYSQL")) result = GetMySqlTables();
            else if (vTypeDb.Contains("DB2")) result = GetDb2Tables();
            else if (vTypeDb.Contains("CH")) result = GetChTables();
            else if (vTypeDb.Contains("SNOWFLAKE")) result = GetSnowTables();
            return result;
        }

        public static string GetSqlProgramms(string vTypeDb)
        {
            var result = "";
            if (vTypeDb.Contains("MSSQL")) result = GetMsSqlProcedurees();
            else if (vTypeDb.Contains("VERTICA")) result = GetVerticaDummy();
            else if (vTypeDb.Contains("ORACLE")) result = GetOracleProcedurees();
            else if (vTypeDb.Contains("PGSQL")) result = GetPgSqlProcedures();
            else if (vTypeDb.Contains("MYSQL")) result = GetMySqlProcedurees();
            else if (vTypeDb.Contains("DB2")) result = GetDb2Procedurees();
            else if (vTypeDb.Contains("SNOWFLAKE")) result = GetSnowProcedures();
            else if (vTypeDb.Contains("CH")) result = GetMsSqlDummy();
            return result;
        }

        public static string GetSqlFunctions(string vTypeDb)
        {
            var result = "";
            if (vTypeDb.Contains("MSSQL")) result = GetMsSqlFuctions();
            else if (vTypeDb.Contains("VERTICA")) result = GetVerticaDummy();
            else if (vTypeDb.Contains("ORACLE")) result = GetOracleFuctions();
            else if (vTypeDb.Contains("PGSQL")) result = GetPgSqlFuctions();
            else if (vTypeDb.Contains("MYSQL")) result = GetMySqlFuctions();
            else if (vTypeDb.Contains("DB2")) result = GetDb2Fuctions();
            else if (vTypeDb.Contains("SNOWFLAKE")) result = GetSnowFuctions();
            else if (vTypeDb.Contains("CH")) result = GetMsSqlDummy();
            return result;
        }

        public static string GetSqlTableColumn(string vTypeDb)
        {
            var result = "";
            if (vTypeDb.Contains("MSSQL")) result = GetMsSqlColumns();
            else if (vTypeDb.Contains("VERTICA")) result = GetVerticaColumns();
            else if (vTypeDb.Contains("ORACLE")) result = GetOracleColumns();
            else if (vTypeDb.Contains("PGSQL")) result = GetPgSqlColumns();
            else if (vTypeDb.Contains("MYSQL")) result = GetMySqlColumns();
            else if (vTypeDb.Contains("DB2")) result = GetDb2Columns();
            else if (vTypeDb.Contains("SNOWFLAKE")) result = GetSnowColumns();
            else if (vTypeDb.Contains("CH")) result = GetChTablesColumns();
            return result;
        }

        public static string GetSqlIndexes(string vTypeDb)
        {
            var result = "";
            if (vTypeDb.Contains("MSSQL")) result = GetMsSqlIndexes();
            else if (vTypeDb.Contains("VERTICA")) result = GetVerticaIndexes();
            else if (vTypeDb.Contains("ORACLE")) result = GetOracleIndexes();
            else if (vTypeDb.Contains("PGSQL")) result = GetPgSqlIndexes();
            else if (vTypeDb.Contains("MYSQL")) result = GetMySqlIndexes();
            else if (vTypeDb.Contains("DB2")) result = GetDb2Indexes();
            else if (vTypeDb.Contains("SNOWFLAKE")) result = GetSnowIndexes();
            else if (vTypeDb.Contains("CH")) result = GetMsSqlDummy();
            return result;
        }

        public static string GetSqlDependencies(string vTypeDb)
        {
            var result = "";
            if (vTypeDb.Contains("MSSQL")) result = GetMsSqlDummy();
            else if (vTypeDb.Contains("VERTICA")) result = GetVerticaDummy();
            else if (vTypeDb.Contains("ORACLE")) result = GetOracleDependenies();
            else if (vTypeDb.Contains("PGSQL")) result = GetVerticaDummy();
            else if (vTypeDb.Contains("MYSQL")) result = GetVerticaDummy();
            else if (vTypeDb.Contains("DB2")) result = GetVerticaDummy();
            else if (vTypeDb.Contains("SNOWFLAKE")) result = GetSnowDummy();
            else if (vTypeDb.Contains("CH")) result = GetMsSqlDummy();
            return result;
        }


        public static string GetCloudSqlCheck(string vTypeCloudB)
        {
            var result = "";
            if (vTypeCloudB.Contains("CloudCH")) result = @"select * from system.databases";
            return result;
        }

        public static string GetCloudSqlView(string vTypeCloudB)
        {
            var result = "";
            if (vTypeCloudB.Contains("CloudCH")) result = GetChViews();
            return result;
        }

        public static string GetChViews()
        {
            return @"SELECT distinct database ||'.'|| name value FROM system.tables where engine = 'View' ";
        }


        public static string GetCloudSqlTable(string vTypeCloudB)
        {
            var result = "";
            if (vTypeCloudB.Contains("CloudCH")) result = GetChTables();
            return result;
        }

        public static string GetChTables()
        {
            return @"SELECT distinct database ||'.'|| name value FROM system.tables where engine <> 'View' ";
        }

        public static string GetChTablesColumns()
        {
            return @"SELECT name value  FROM system.columns where  database in ('%TOWNER%') and table  = '%TNAME%'";
        }

        public static string GetOracleDependenies()
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


        public static string GetErrConType(string vErroMsg)
        {
            var result = "";
            if (vErroMsg.Contains("Login fails")) result = "LoginErr";
            return result;
        }

        public static string GetVerticaDummy()
        {
            return @" select ''  as  value from dual ";
        }

        public static string GetSnowDummy()
        {
            return @" select ''  as   value  ";
        }

        public static string GetMsSqlDummy()
        {
            return @" select ''  as  value  ";
        }

        public static string GetMsSqlViews()
        {
            return @"SELECT distinct d.[name] + '.' + v.[name] value 
                         FROM  sys.objects as v  
                       left join sys.schemas d on 1=1
                            and d.schema_id = v.schema_id   
                       where type = 'V'   
                      order by 1 ";
        }

        public static string GetMsSqlTables()
        {
            return @"SELECT distinct d.[name] + '.' + v.[name] value 
                         FROM  sys.objects as v  
                       left join sys.schemas d on 1=1
                            and d.schema_id = v.schema_id   
                       where type = 'U'   
                      order by 1 ";
        }

        public static string GetMsSqlColumns()
        {
            return @"SELECT 
                           Column_Name + ' | '+ Data_type value 
                        FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE TABLE_SCHEMA +'.'+ TABLE_NAME = N'%TNAME%'  ";
        }

        public static string GetMsSqlIndexes()
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


        public static string GetMsSqlProcedurees()
        {
            return @"SELECT distinct type ,  d.[name] + '.' + v.[name] value 
                         FROM  sys.objects as v  
                       left join sys.schemas d on 1=1
                            and d.schema_id = v.schema_id   
                       where type in ( 'P'   )
                      order by 1 desc,2 asc ";
        }

        public static string GetMsSqlFuctions()
        {
            return @"SELECT distinct type ,  d.[name] + '.' + v.[name] value 
                         FROM  sys.objects as v  
                       left join sys.schemas d on 1=1
                            and d.schema_id = v.schema_id   
                       where type in (   'TF','FN'  )
                      order by 1 desc,2 asc ";
        }

        public static string GetMsSqlReserved()
        {
            return @"\b(ADD|ALL|ALTER|AND|ANY|AS|ASC|AUTHORIZATION|BACKUP|BEGIN|BETWEEN|BREAK|BROWSE|BULK|BY|CASCADE|CASE|CHECK|CHECKPOINT|CLOSE|CLUSTERED|COALESCE|COLLATE|COLUMN|COMMIT|COMPUTE|CONSTRAINT|CONTAINS|CONTAINSTABLE|CONTINUE|CONVERT|CREATE|CROSS|CURRENT|CURRENT_DATE|CURRENT_TIME|CURRENT_TIMESTAMP|CURRENT_USER|CURSOR|DATABASE|DBCC|DEALLOCATE|DECLARE|DEFAULT|DELETE|DENY|DESC|DISK|DISTINCT|DISTRIBUTED|DOUBLE|DROP|DUMP|ELSE|END|ERRLVL|ESCAPE|EXCEPT|EXEC|EXECUTE|EXISTS|EXIT|EXTERNAL|FETCH|FILE|FILLFACTOR|FOR|FOREIGN|FREETEXT|FREETEXTTABLE|FROM|FULL|FUNCTION|GOTO|GRANT|GROUP|HAVING|HOLDLOCK|IDENTITY|IDENTITY_INSERT|IDENTITYCOL|IF|IN|INDEX|INNER|INSERT|INTERSECT|INTO|IS|JOIN|KEY|KILL|LEFT|LIKE|LINENO|LOAD|MERGE|NATIONAL|NOCHECK|NONCLUSTERED|NOT|NULL|NULLIF|OF|OFF|OFFSETS|ON|OPEN|OPENDATASOURCE|OPENQUERY|OPENROWSET|OPENXML|OPTION|OR|ORDER|OUTER|OVER|PERCENT|PIVOT|PLAN|PRECISION|PRIMARY|PRINT|PROC|PROCEDURE|PUBLIC|RAISERROR|READ|READTEXT|RECONFIGURE|REFERENCES|REPLICATION|RESTORE|RESTRICT|RETURN|REVERT|REVOKE|RIGHT|ROLLBACK|ROWCOUNT|ROWGUIDCOL|RULE|SAVE|SCHEMA|SECURITYAUDIT|SELECT|SEMANTICKEYPHRASETABLE|SEMANTICSIMILARITYDETAILSTABLE|SEMANTICSIMILARITYTABLE|SESSION_USER|SET|SETUSER|SHUTDOWN|SOME|STATISTICS|SYSTEM_USER|TABLE|TABLESAMPLE|TEXTSIZE|THEN|TO|TOP|TRAN|TRANSACTION|TRIGGER|TRUNCATE|TRY_CONVERT|TSEQUAL|UNION|UNIQUE|UNPIVOT|UPDATE|UPDATETEXT|USE|USER|VALUES|VARYING|VIEW|WAITFOR|WHEN|WHERE|WHILE|WITH|WITHINGROUP|WRITETEXT)\b";
        }

        public static string GetVerticaTables()
        {
            return @"select distinct table_schema || '.' || table_name value                                                          
                        from v_catalog.tables 
                        order by 1 ";
        }

        public static string GetVerticaViews()
        {
            return @"select distinct table_schema || '.' || table_name value                                                          
                        from v_catalog.views 
                            order by 1 ";
        }


        public static string GetVerticaColumns()
        {
            return @" select  column_name || ' | ' ||  data_type value
                         from v_catalog.columns
                    where  table_schema || '.' ||  table_name  in (  '%TNAME%' )
                       order by 1 ";
        }

        public static string GetVerticaIndexes()
        {
            return @" select  column_name  value
                         from v_catalog.primary_keys
                    where  table_schema || '.' ||  table_name  in ('%TNAME%') 
                         order by 1  ";
        }


        public static string GetOracleViews()
        {
            return @" SELECT (SELECT SYS_CONTEXT('USERENV','CURRENT_SCHEMA') FROM DUAL) ||'.'|| view_name value FROM user_views order by 1 ";
        }

        public static string GetOracleTables()
        {
            return @" SELECT (SELECT SYS_CONTEXT('USERENV','CURRENT_SCHEMA') FROM DUAL) ||'.'|| table_name value FROM user_tables order by 1";
        }

        public static string GetOracleColumns()
        {
            return @" SELECT Column_Name || ' | ' || Data_type value FROM user_tab_cols WHERE   TABLE_NAME =  '%TNAME%' ";
        }

        public static string GetOracleIndexes()
        {
            return @"  SELECT  index_name value FROM user_indexes  WHERE   TABLE_NAME in ('%TNAME%') order by 1 ";
        }


        public static string GetOracleProcedurees()
        {
            return @" SELECT object_name value FROM USER_OBJECTS WHERE OBJECT_TYPE IN ( 'PROCEDURE','PACKAGE') order by 1 ";
        }

        public static string GetOracleFuctions()
        {
            return @" SELECT object_name value FROM USER_OBJECTS WHERE OBJECT_TYPE IN ('FUNCTION') order by 1 ";
        }

        public static string GetPgSqlViews()
        {
            return @" SELECT distinct schemaname || '.' || viewname as  value FROM pg_catalog.pg_views WHERE 1=1  order by 1 ";
        }

        public static string GetSnowViews()
        {
            return @"  SELECT distinct TABLE_SCHEMA || '.' || TABLE_NAME as  value FROM INFORMATION_SCHEMA.views WHERE 1=1  order by 1 ";
        }


        public static string GetPgSqlTables()
        {
            return @" SELECT distinct schemaname || '.' || tablename as  value FROM pg_catalog.pg_tables WHERE 1=1 order by 1 ";
        }

        public static string GetSnowTables()
        {
            return @"  SELECT distinct TABLE_SCHEMA || '.' || TABLE_NAME as  value FROM INFORMATION_SCHEMA.tables WHERE TABLE_TYPE NOT IN ('VIEW')   order by 1 ";
        }


        public static string GetPgSqlColumns()
        {
            return @" SELECT  Column_Name || ' | ' || Data_type as  value FROM information_schema.columns  WHERE table_schema || '.' ||  table_name  in (  '%TNAME%' )   ";
        }

        public static string GetSnowColumns()
        {
            return @" SELECT  Column_Name || ' | ' || Data_type as  value FROM information_schema.columns  WHERE table_schema || '.' ||  table_name  in (  '%TNAME%' )   ";
        }

        public static string GetSnowIndexes()
        {
            return @" SELECT  '' as  value   ";
        }

        public static string GetPgSqlIndexes()
        {
            return @" SELECT  indexname as  value FROM pg_indexes WHERE schemaname || '.' ||  tablename  in ('%TNAME%')   ";
        }


        public static string GetPgSqlProcedures()
        {
            return @" select distinct  n.nspname || '.' ||   p.proname as value   from pg_proc p left join pg_namespace n on p.pronamespace = n.oid order by 1 ";
        }

        public static string GetSnowProcedures()
        {
            return @"  SELECT distinct PROCEDURE_SCHEMA || '.' || PROCEDURE_NAME  AS value   FROM INFORMATION_SCHEMA.PROCEDURES  order by 1 ";
        }


        public static string GetDb2Fuctions()
        {
            return @"  SELECT distinct FUNCTION_SCHEMA  || '.' || FUNCTION_NAME  AS value   FROM  INFORMATION_SCHEMA.FUNCTIONS order by 1  ";
        }


        public static string GetPgSqlFuctions()
        {
            return @" select distinct specific_schema || '.' || specific_name as value from information_schema.routines order by 1 ";
        }



        public static string GetSnowFuctions()
        {
            return @" SELECT distinct FUNCTION_SCHEMA  || '.' || FUNCTION_NAME  AS value   FROM  INFORMATION_SCHEMA.FUNCTIONS  order by 1 ";
        }

        public static string GetMySqlViews()
        {
            return @" SELECT  distinct  CONCAT(table_schema ,  '.' , table_name) as value   FROM information_schema.VIEWS order by 1 ";
        }

        public static string GetMySqlTables()
        {
            return @" SELECT distinct  CONCAT(table_schema ,  '.' , table_name) as value FROM information_schema.tables order by 1 ;";
        }

        public static string GetMySqlColumns()
        {
            return @" SELECT CONCAT(column_name ,  ' | ' , data_type )  as value    FROM information_schema.COLUMNS  where CONCAT(table_schema ,  '.' , table_name)   in (  '%TNAME%' )  ";
        }

        public static string GetMySqlIndexes()
        {
            return @" SELECT distinct index_name as  value FROM information_schema.STATISTICS  where CONCAT(table_schema ,  '.' , table_name)   in ('%TNAME%') order by 1   ";
        }

        public static string GetMySqlProcedurees()
        {
            return @" SELECT    distinct  CONCAT(routine_schema ,  '.' , routine_name )  as value  FROM     information_schema.routines     where routine_type='PROCEDURE' order by 1 ";
        }

        public static string GetMySqlFuctions()
        {
            return @" SELECT    distinct  CONCAT(routine_schema ,  '.' , routine_name ) as value FROM     information_schema.routines     where routine_type='FUNCTION' order by 1";
        }

        public static string GetDb2Views()
        {
            return @"  SELECT  distinct table_schema || '.' || table_name as value  FROM   SYSIBM.tables   WHERE 1=1  and table_type = 'VIEW'  order by 1 ";
        }

        public static string GetDb2Tables()
        {
            return @" SELECT  distinct table_schema || '.' || table_name  as value  FROM   SYSIBM.tables   WHERE 1=1  and table_type = 'BASE TABLE'  order by 1;";
        }

        public static string GetDb2Columns()
        {
            return @"   select name || ' | ' coltype  as value FROM   SYSIBM.SYSCOLUMNS  where tbcreator ||  '.' || tbname  in (  '%TNAME%' )  order by 1  ";
        }

        public static string GetDb2Indexes()
        {
            return @"    select indname as value from syscat.indexes where tabname  ||'.'|| tabschema in ('%TNAME%') order by 1   ";
        }


        public static string GetDb2Procedurees()
        {
            return @" select distinct specific_schema || '.' || specific_name value from SYSIBM.ROUTINES   where routine_type='PROCEDURE' order by 1   ";
        }

        public static string GetCloudColumns(string vTypeCloudB)
        {
            var result = "";
            if (vTypeCloudB.Contains("CloudCH")) result = GetChTablesColumns();
            return result;
        }
  

    }
}
