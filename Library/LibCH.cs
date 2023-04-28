 namespace SqlEngine
{
    class LibCH
    { 

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
         

    
         

        public static string getCloudColumns(string vTypeCloudB)
        {
            string vResult = "";
            if (vTypeCloudB.Contains("CloudCH")) vResult = getCHTablesColumns();
            return vResult;
        }
 
  

    }
}
