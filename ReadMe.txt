

Some ideas 
 https://www.codeproject.com/Articles/30705/C-CSV-Import-Export


    ----------
    https://docs.microsoft.com/ru-ru/visualstudio/vsto/listobject-control?view=vs-2019

    ListObject Events 

    BeforeAddDataBoundRow BeforeDoubleClick BeforeRightClick BindingContextChanged Change DataBindingFailure DataMemberChanged  DataSourceChanged Deselected ErrorAddDataBoundRow OriginalDataRestored
        Selected SelectedIndexChanged SelectionChange


        https://www.oracle.com/webfolder/technetwork/tutorials/obe/db/12c/r1/appdev/dotnet/Web_version_Fully_Managed_ODPnet_OBE/odpnetmngdrv.html

        string connString = "DATA SOURCE=10.204.3.1:1521/PROD;" +
"PERSIST SECURITY INFO=True;USER ID=username; password=password; Pooling =False;";

OracleConnection conn = new OracleConnection();
conn.ConnectionString = connString;
conn.Open();

https://github.com/oracle/dotnet-db-samples/blob/master/samples/dotnet-core/DataReader/DataReader.cs
  //Demo: Basic ODP.NET Core application to connect, query, and return
            // results from an OracleDataReader to a console

            //Create a connection to Oracle			
            string conString = "User Id=hr;Password=<password>;" +

            //How to connect to an Oracle DB without SQL*Net configuration file
            //  also known as tnsnames.ora.
            "Data Source=<ip or hostname>:1521/<service name>;";

            //How to connect to an Oracle DB with a DB alias.
            //Uncomment below and comment above.
            //"Data Source=<service name alias>;";
     
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;                        

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select first_name from employees where department_id = :id";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", 50);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Employee First Name: " + reader.GetString(0));
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press 'Enter' to continue");

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadLine();
                }
            }
        }
    }