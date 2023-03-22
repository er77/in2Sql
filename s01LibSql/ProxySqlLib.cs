using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlEngine
{
    class ProxySqlLib
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
            if (vTypeDB.Contains("MSSQL")) vResult = libMsSql.getMsSqlViews();
            else if (vTypeDB.Contains("VERTICA")) vResult = libVertica.getVerticaViews();
            else if (vTypeDB.Contains("ORACLE")) vResult = libOracle.getOracleViews();
            else if (vTypeDB.Contains("PGSQL")) vResult = libPgSql.getPgSqlViews();
            else if (vTypeDB.Contains("MYSQL")) vResult = libMySql.getMySqlViews();
            else if (vTypeDB.Contains("DB2")) vResult = libDb2.getDb2Views();
            else if (vTypeDB.Contains("CH")) vResult = libCHouse.getCHViews();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = libSnow.getSnowViews();
            return vResult;
        }

        public static string getSqlTables(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = libMsSql.getMsSqlTables();
            else if (vTypeDB.Contains("VERTICA")) vResult = libVertica.getVerticaTables();
            else if (vTypeDB.Contains("ORACLE")) vResult = libOracle.getOracleTables();
            else if (vTypeDB.Contains("PGSQL")) vResult = libPgSql.getPgSqlTables();
            else if (vTypeDB.Contains("MYSQL")) vResult = libMySql.getMySqlTables();
            else if (vTypeDB.Contains("DB2")) vResult = libDb2.getDb2Tables();
            else if (vTypeDB.Contains("CH")) vResult = libCHouse.getCHTables();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = libSnow.getSnowTables();
            return vResult;
        }

        public static string getSQLProgramms(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = libMsSql.getMsSqlProcedurees();
            else if (vTypeDB.Contains("VERTICA")) vResult = libVertica.getVerticaDummy();
            else if (vTypeDB.Contains("ORACLE")) vResult = libOracle.getOracleProcedurees();
            else if (vTypeDB.Contains("PGSQL")) vResult = libPgSql.getPgSqlProcedures();
            else if (vTypeDB.Contains("MYSQL")) vResult = libMySql.getMySqlProcedurees();
            else if (vTypeDB.Contains("DB2")) vResult = libDb2.getDb2Procedurees();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = libSnow.getSnowProcedures();
            else if (vTypeDB.Contains("CH")) vResult = libMsSql.getMsSqlDummy();
            return vResult;
        }

        public static string getSQLFunctions(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = libMsSql.getMsSqlFuctions();
            else if (vTypeDB.Contains("VERTICA")) vResult = libVertica.getVerticaDummy();
            else if (vTypeDB.Contains("ORACLE")) vResult = libOracle.getOracleFuctions();
            else if (vTypeDB.Contains("PGSQL")) vResult = libPgSql.getPgSqlFuctions();
            else if (vTypeDB.Contains("MYSQL")) vResult = libMySql.getMySqlFuctions();
            else if (vTypeDB.Contains("DB2")) vResult = libDb2.getDb2Fuctions();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = libSnow.getSnowFuctions();
            else if (vTypeDB.Contains("CH")) vResult = libMsSql.getMsSqlDummy();
            return vResult;
        }

        public static string getSQLTableColumn(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = libMsSql.getMsSqlColumns();
            else if (vTypeDB.Contains("VERTICA")) vResult = libVertica.getVerticaColumns();
            else if (vTypeDB.Contains("ORACLE")) vResult = libOracle.getOracleColumns();
            else if (vTypeDB.Contains("PGSQL")) vResult = libPgSql.getPgSqlColumns();
            else if (vTypeDB.Contains("MYSQL")) vResult = libMySql.getMySqlColumns();
            else if (vTypeDB.Contains("DB2")) vResult = libDb2.getDb2Columns();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = libSnow.getSnowColumns();
            else if (vTypeDB.Contains("CH")) vResult = libCHouse.getCHTablesColumns();
            return vResult;
        }

        public static string getSQLIndexes(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult =  libMsSql.getMsSqlIndexes();
            else if (vTypeDB.Contains("VERTICA")) vResult = libVertica.getVerticaIndexes();
            else if (vTypeDB.Contains("ORACLE")) vResult = libOracle.getOracleIndexes();
            else if (vTypeDB.Contains("PGSQL")) vResult = libPgSql.getPgSqlIndexes();
            else if (vTypeDB.Contains("MYSQL")) vResult = libMySql.getMySqlIndexes();
            else if (vTypeDB.Contains("DB2")) vResult = libDb2.getDb2Indexes();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = libSnow.getSnowIndexes();
            else if (vTypeDB.Contains("CH")) vResult = libMsSql.getMsSqlDummy();
            return vResult;
        }

        public static string getSQLDependencies(string vTypeDB)
        {
            string vResult = "";
            if (vTypeDB.Contains("MSSQL")) vResult = libMsSql.getMsSqlDummy();
            else if (vTypeDB.Contains("VERTICA")) vResult = libVertica.getVerticaDummy();
            else if (vTypeDB.Contains("ORACLE")) vResult = libOracle.getOracleDependenies();
            else if (vTypeDB.Contains("PGSQL")) vResult = libVertica.getVerticaDummy();
            else if (vTypeDB.Contains("MYSQL")) vResult = libVertica.getVerticaDummy();
            else if (vTypeDB.Contains("DB2")) vResult = libVertica.getVerticaDummy();
            else if (vTypeDB.Contains("SNOWFLAKE")) vResult = libSnow.getSnowDummy();
            else if (vTypeDB.Contains("CH")) vResult = libMsSql.getMsSqlDummy();
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
