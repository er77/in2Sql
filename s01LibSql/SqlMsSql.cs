using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlEngine
{
    internal class SqlMsSql
    {
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

    }
}
