namespace SqlEngine
{
    class LibSnowFlake
    {
 
 
        public static string getSnowDummy()
        {
            return @" select ''  as   value  ";
        } 

     

        public static string getSnowViews()
        {
            return @"  SELECT distinct TABLE_SCHEMA || '.' || TABLE_NAME as  value FROM INFORMATION_SCHEMA.views WHERE 1=1  order by 1 ";
        }

     
        public static string getSnowTables()
        {
            return @"  SELECT distinct TABLE_SCHEMA || '.' || TABLE_NAME as  value FROM INFORMATION_SCHEMA.tables WHERE TABLE_TYPE NOT IN ('VIEW')   order by 1 ";
        }

 

        public static string getSnowColumns()
        {
            return @" SELECT  Column_Name || ' | ' || Data_type as  value FROM information_schema.columns  WHERE table_schema || '.' ||  table_name  in (  '%TNAME%' )   ";
        }

        public static string getSnowIndexes()
        {
            return @" SELECT  '' as  value   ";
        }

   

        public static string getSnowProcedures()
        {
            return @"  SELECT distinct PROCEDURE_SCHEMA || '.' || PROCEDURE_NAME  AS value   FROM INFORMATION_SCHEMA.PROCEDURES  order by 1 ";
        }

 


        public static string getSnowFuctions()
        {
            return @" SELECT distinct FUNCTION_SCHEMA  || '.' || FUNCTION_NAME  AS value   FROM  INFORMATION_SCHEMA.FUNCTIONS  order by 1 ";
        }
         
 
 
  

    }
}
