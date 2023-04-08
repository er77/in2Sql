using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace SqlEngine
{
    public partial class Wf07OdbcProperties : Form
    {
        private string vOdbcType = "";

        private string vCurrAction = "";


        public Wf07OdbcProperties()
        {
            InitializeComponent();
        }

        private void RTBOdbcProp_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetSQlEdit ()
        {
            /*
                01 SQL Server               01 Tables
                                   02 TableColumn
                                  03 Indexes
                               04 Views
                                    05 Functions
                                 06 Programs
                51 Access
                52 Amazon Redshift
                53 Amazon Dynamodb
                54 ASE
                55 ClickHouse
                56 Ferebird
                57 GreenPlum
                58 Google BigQuery
                59 Hive
                60 InterBase 
                61 MongoDB
                62 NexusDB
                63 SQLite
                64 SQL Azure
                65 xBase 

             */

           


            switch (vOdbcType)
            {
                case "01 SQL Server":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SSqlLibrary.GetMsSqlTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SSqlLibrary.GetMsSqlColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SSqlLibrary.GetMsSqlIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SSqlLibrary.GetMsSqlViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SSqlLibrary.GetMsSqlFuctions ();
                            break;
                        case "06 Programs":
                            RTBOdbcProp.Text = SSqlLibrary.GetMsSqlProcedurees ();
                            break;
                    }
                    break;

                case "02 Oracle":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SSqlLibrary.GetOracleTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SSqlLibrary.GetOracleColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SSqlLibrary.GetOracleIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SSqlLibrary.GetOracleViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SSqlLibrary.GetOracleFuctions ();
                            break;
                        case "06 Programs":
                            RTBOdbcProp.Text = SSqlLibrary.GetOracleProcedurees ();
                            break;
                    }
                    break; 

                case "03 Vertica":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SSqlLibrary.GetVerticaTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SSqlLibrary.GetVerticaColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SSqlLibrary.GetVerticaIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SSqlLibrary.GetVerticaViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SSqlLibrary.GetVerticaDummy ();
                            break;
                        case "06 Programs":
                            RTBOdbcProp.Text = SSqlLibrary.GetVerticaDummy ();
                            break;
                    }
                    break;

                case "04 PostgreSQL":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SSqlLibrary.GetPgSqlTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SSqlLibrary.GetPgSqlColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SSqlLibrary.GetPgSqlIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SSqlLibrary.GetPgSqlViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SSqlLibrary.GetPgSqlFuctions ();
                            break;
                        case "06 Programs":
                            RTBOdbcProp.Text = SSqlLibrary.GetPgSqlProcedures ();
                            break;
                    }
                    break;

                case "05 MySQL":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SSqlLibrary.GetMySqlTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SSqlLibrary.GetMySqlColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SSqlLibrary.GetMySqlIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SSqlLibrary.GetMySqlViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SSqlLibrary.GetMySqlFuctions ();
                            break;
                        case "06 Programs":
                            RTBOdbcProp.Text = SSqlLibrary.GetMySqlProcedurees ();
                            break;
                    }
                    break;

                case "06 IBM DB2":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SSqlLibrary.GetDb2Tables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SSqlLibrary.GetDb2Columns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SSqlLibrary.GetDb2Indexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SSqlLibrary.GetDb2Views();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SSqlLibrary.GetDb2Fuctions ();
                            break;
                        case "06 Programs":
                            RTBOdbcProp.Text = SSqlLibrary.GetDb2Procedurees ();
                            break;
                    }
                    break;

                case "07 SnowFlake":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SSqlLibrary.GetSnowTables();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SSqlLibrary.GetSnowColumns();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SSqlLibrary.GetSnowIndexes();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SSqlLibrary.GetSnowViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SSqlLibrary.GetSnowFuctions();
                            break;
                        case "06 Programs":
                            RTBOdbcProp.Text = SSqlLibrary.GetSnowProcedures();
                            break;
                    }
                    break;


                default:
                    RTBOdbcProp.Text = " under construction ";
                    break;
            }

            SqlColored();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            vOdbcType = comboBox1.SelectedItem.ToString();

            if (vCurrAction == "")
            { 
                return;
            }

            SetSQlEdit();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            vCurrAction = comboBox2.SelectedItem.ToString();

            if ( vOdbcType == "")
            { 
                MessageBox.Show(@" Please select Sql Database in the first ComboBox");
                return;
            }

            SetSQlEdit();
        }

        private void SqlColored()
        {
            // getting keywords/functions
            var keywords = SSqlLibrary.GetMsSqlReserved();

            var keywordMatches = Regex.Matches(RTBOdbcProp.Text.ToUpper(), keywords);

            // getting types/classes from the text 
            var types = @"\b(Console)\b";
            var typeMatches = Regex.Matches(RTBOdbcProp.Text, types);

            // getting comments (inline or multiline)
            var comments = @"(\/\/.+?$|\/\*.+?\*\/)";
            var commentMatches = Regex.Matches(RTBOdbcProp.Text, comments, RegexOptions.Multiline);

            // getting strings
            var strings = "\".+?\"";
            var stringMatches = Regex.Matches(RTBOdbcProp.Text, strings);

            // saving the original caret position + forecolor
            var originalIndex = RTBOdbcProp.SelectionStart;
            var originalLength = RTBOdbcProp.SelectionLength;
            var originalColor = Color.Black;

            // MANDATORY - focuses a label before highlighting (avoids blinking)
            Focus();

            // removes any previous highlighting (so modified words won't remain highlighted)
            RTBOdbcProp.SelectionStart = 0;
            RTBOdbcProp.SelectionLength = RTBOdbcProp.Text.Length;
            RTBOdbcProp.SelectionColor = originalColor;

            // scanning...
            foreach (Match m in keywordMatches)
            {
                RTBOdbcProp.SelectionStart = m.Index;
                RTBOdbcProp.SelectionLength = m.Length;
                RTBOdbcProp.SelectionColor = Color.Blue;
            }

            foreach (Match m in typeMatches)
            {
                RTBOdbcProp.SelectionStart = m.Index;
                RTBOdbcProp.SelectionLength = m.Length;
                RTBOdbcProp.SelectionColor = Color.DarkCyan;
            }


            foreach (Match m in stringMatches)
            {
                RTBOdbcProp.SelectionStart = m.Index;
                RTBOdbcProp.SelectionLength = m.Length;
                RTBOdbcProp.SelectionColor = Color.Brown;
            }


            foreach (Match m in commentMatches)
            {
                RTBOdbcProp.SelectionStart = m.Index;
                RTBOdbcProp.SelectionLength = m.Length;
                RTBOdbcProp.SelectionColor = Color.Green;
            }

            // restoring the original colors, for further writing
            RTBOdbcProp.SelectionStart = originalIndex;
            RTBOdbcProp.SelectionLength = originalLength;
            RTBOdbcProp.SelectionColor = originalColor;

            // giving back the focus
            RTBOdbcProp.Focus();
        }
    }
}
