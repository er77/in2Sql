
using static SqlEngine.LibMsSql;
using static SqlEngine.LibMySql;
using static SqlEngine.LibOracle;
using static SqlEngine.LibDb2;
using static SqlEngine.LibVertica;
using static SqlEngine.LibSnowFlake;
using static SqlEngine.LibCH;
using static SqlEngine.LibPgSql;


namespace SqlEngine
{
    class SqlLib
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

        public static string getErrConType(string vErroMsg)
        {
            string vResult = "";
            if (vErroMsg.Contains("Login fails")) vResult = "LoginErr";
            return vResult;
        }
    }
}
