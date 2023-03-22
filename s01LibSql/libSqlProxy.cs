using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlEngine
{
    class libSqlProxy
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
            if (vTypeDB.Contains("MSSQL")) vResult = SqlMsSql.getMsSqlViews();
            else if (vTypeDB.Contains("VERTICA")) vResult = SqlVertica.getVerticaViews();
            else if (vTypeDB.Contains("ORACLE")) vResult = SqlOracle.getOracleViews();
            else if (vTypeDB.Contains("PGSQL")) vResult = SqlPgSql.getPgSqlViews();
            else if (vTypeDB.Contains("MYSQL")) vResult = SqlMySql.getMySqlViews();
            else if (vTypeDB.Contains("DB2")) vResult = SqlDb2.getDb2Views();
            else if (vTypeDB.Contains("CH")) vResult = SqlCloud.getCHViews();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = SqlSnow.getSnowViews();
            return vResult;
        }

        public static string getSqlTables(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = SqlMsSql.getMsSqlTables();
            else if (vTypeDB.Contains("VERTICA")) vResult = SqlVertica.getVerticaTables();
            else if (vTypeDB.Contains("ORACLE")) vResult = SqlOracle.getOracleTables();
            else if (vTypeDB.Contains("PGSQL")) vResult = SqlPgSql.getPgSqlTables();
            else if (vTypeDB.Contains("MYSQL")) vResult = SqlMySql.getMySqlTables();
            else if (vTypeDB.Contains("DB2")) vResult = SqlDb2.getDb2Tables();
            else if (vTypeDB.Contains("CH")) vResult = SqlCloud.getCHTables();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = SqlSnow.getSnowTables();
            return vResult;
        }

        public static string getSQLProgramms(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = SqlMsSql.getMsSqlProcedurees();
            else if (vTypeDB.Contains("VERTICA")) vResult = SqlVertica.getVerticaDummy();
            else if (vTypeDB.Contains("ORACLE")) vResult = SqlOracle.getOracleProcedurees();
            else if (vTypeDB.Contains("PGSQL")) vResult = SqlPgSql.getPgSqlProcedures();
            else if (vTypeDB.Contains("MYSQL")) vResult = SqlMySql.getMySqlProcedurees();
            else if (vTypeDB.Contains("DB2")) vResult = SqlDb2.getDb2Procedurees();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = SqlSnow.getSnowProcedures();
            else if (vTypeDB.Contains("CH")) vResult = SqlMsSql.getMsSqlDummy();
            return vResult;
        }

        public static string getSQLFunctions(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = SqlMsSql.getMsSqlFuctions();
            else if (vTypeDB.Contains("VERTICA")) vResult = SqlVertica.getVerticaDummy();
            else if (vTypeDB.Contains("ORACLE")) vResult = SqlOracle.getOracleFuctions();
            else if (vTypeDB.Contains("PGSQL")) vResult = SqlPgSql.getPgSqlFuctions();
            else if (vTypeDB.Contains("MYSQL")) vResult = SqlMySql.getMySqlFuctions();
            else if (vTypeDB.Contains("DB2")) vResult = SqlDb2.getDb2Fuctions();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = SqlSnow.getSnowFuctions();
            else if (vTypeDB.Contains("CH")) vResult = SqlMsSql.getMsSqlDummy();
            return vResult;
        }

        public static string getSQLTableColumn(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = SqlMsSql.getMsSqlColumns();
            else if (vTypeDB.Contains("VERTICA")) vResult = SqlVertica.getVerticaColumns();
            else if (vTypeDB.Contains("ORACLE")) vResult = SqlOracle.getOracleColumns();
            else if (vTypeDB.Contains("PGSQL")) vResult = SqlPgSql.getPgSqlColumns();
            else if (vTypeDB.Contains("MYSQL")) vResult = SqlMySql.getMySqlColumns();
            else if (vTypeDB.Contains("DB2")) vResult = SqlDb2.getDb2Columns();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = SqlSnow.getSnowColumns();
            else if (vTypeDB.Contains("CH")) vResult = SqlCloud.getCHTablesColumns();
            return vResult;
        }

        public static string getSQLIndexes(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult =  SqlMsSql.getMsSqlIndexes();
            else if (vTypeDB.Contains("VERTICA")) vResult = SqlVertica.getVerticaIndexes();
            else if (vTypeDB.Contains("ORACLE")) vResult = SqlOracle.getOracleIndexes();
            else if (vTypeDB.Contains("PGSQL")) vResult = SqlPgSql.getPgSqlIndexes();
            else if (vTypeDB.Contains("MYSQL")) vResult = SqlMySql.getMySqlIndexes();
            else if (vTypeDB.Contains("DB2")) vResult = SqlDb2.getDb2Indexes();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = SqlSnow.getSnowIndexes();
            else if (vTypeDB.Contains("CH")) vResult = SqlMsSql.getMsSqlDummy();
            return vResult;
        }

        public static string getSQLDependencies(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = SqlMsSql.getMsSqlDummy();
            else if (vTypeDB.Contains("VERTICA")) vResult = SqlVertica.getVerticaDummy();
            else if (vTypeDB.Contains("ORACLE")) vResult = SqlOracle.getOracleDependenies();
            else if (vTypeDB.Contains("PGSQL")) vResult = SqlVertica.getVerticaDummy();
            else if (vTypeDB.Contains("MYSQL")) vResult = SqlVertica.getVerticaDummy();
            else if (vTypeDB.Contains("DB2")) vResult = SqlVertica.getVerticaDummy();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = SqlSnow.getSnowDummy();
            else if (vTypeDB.Contains("CH")) vResult = SqlMsSql.getMsSqlDummy();
            return vResult;
        }




        public static string getErrConType(string vErroMsg)
        {
            string vResult = "";
            if (vErroMsg.Contains("Login fails")) vResult = "LoginErr";
            return vResult;
        }   


 
 

 




        /*    public static string getDb2Fuctions()
            {
                return @"  select distinct specific_schema || '.' || specific_name value from SYSIBM.ROUTINES  where routine_type='FUNCTION' order by 1";
            }
        */

    }
}
