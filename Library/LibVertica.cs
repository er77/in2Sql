namespace SqlEngine
{
    class LibVertica
    {
  

        public static string getVerticaDummy()
        {
            return @" select ''  as  value from dual ";
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

    }
}
