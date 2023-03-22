using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlEngine
{
    public partial class wf07OdbcProperties : Form
    {
        private string vOdbcType = "";

        private string vCurrAction = "";


        public wf07OdbcProperties()
        {
            InitializeComponent();
        }

        private void RTBOdbcProp_TextChanged(object sender, EventArgs e)
        {

        }
         
        private void setSQlEdit ()
        {
            /*
                01 SQL Server               01 Tables
                                   02 TableColumn
                                  03 Indexes
                               04 Views
                                    05 Functions
                                 06 Programms
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
                            RTBOdbcProp.Text = SqlMsSql.getMsSqlTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SqlMsSql.getMsSqlColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SqlMsSql.getMsSqlIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SqlMsSql.getMsSqlViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SqlMsSql.getMsSqlFuctions ();
                            break;
                        case "06 Programms":
                            RTBOdbcProp.Text = SqlMsSql.getMsSqlProcedurees ();
                            break;
                    }
                    break;

                case "02 Oracle":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SqlOracle.getOracleTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SqlOracle.getOracleColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SqlOracle.getOracleIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SqlOracle.getOracleViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SqlOracle.getOracleFuctions ();
                            break;
                        case "06 Programms":
                            RTBOdbcProp.Text = SqlOracle.getOracleProcedurees ();
                            break;
                    }
                    break; 

                case "03 Vertica":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SqlVertica.getVerticaTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SqlVertica.getVerticaColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SqlVertica.getVerticaIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SqlVertica.getVerticaViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SqlVertica.getVerticaDummy ();
                            break;
                        case "06 Programms":
                            RTBOdbcProp.Text = SqlVertica.getVerticaDummy ();
                            break;
                    }
                    break;

                case "04 PostgreSQL":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SqlPgSql.getPgSqlTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SqlPgSql.getPgSqlColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SqlPgSql.getPgSqlIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SqlPgSql.getPgSqlViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SqlPgSql.getPgSqlFuctions ();
                            break;
                        case "06 Programms":
                            RTBOdbcProp.Text = SqlPgSql.getPgSqlProcedures ();
                            break;
                    }
                    break;

                case "05 MySQL":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SqlMySql.getMySqlTables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SqlMySql.getMySqlColumns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SqlMySql.getMySqlIndexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SqlMySql.getMySqlViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SqlMySql.getMySqlFuctions ();
                            break;
                        case "06 Programms":
                            RTBOdbcProp.Text = SqlMySql.getMySqlProcedurees ();
                            break;
                    }
                    break;

                case "06 IBM DB2":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SqlDb2.getDb2Tables ();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SqlDb2.getDb2Columns ();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SqlDb2.getDb2Indexes ();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SqlDb2.getDb2Views();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SqlDb2.getDb2Fuctions ();
                            break;
                        case "06 Programms":
                            RTBOdbcProp.Text = SqlDb2.getDb2Procedurees ();
                            break;
                    }
                    break;

                case "07 SnowFlake":
                    switch (vCurrAction)
                    {
                        case "01 Tables":
                            RTBOdbcProp.Text = SqlSnow.getSnowTables();
                            break;
                        case "02 TableColumn":
                            RTBOdbcProp.Text = SqlSnow.getSnowColumns();
                            break;
                        case "03 Indexes":
                            RTBOdbcProp.Text = SqlSnow.getSnowIndexes();
                            break;
                        case "04 Views":
                            RTBOdbcProp.Text = SqlSnow.getSnowViews();
                            break;
                        case "05 Functions":
                            RTBOdbcProp.Text = SqlSnow.getSnowFuctions();
                            break;
                        case "06 Programms":
                            RTBOdbcProp.Text = SqlSnow.getSnowProcedures();
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

            setSQlEdit();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            vCurrAction = comboBox2.SelectedItem.ToString();

            if ( vOdbcType == "")
            { 
                MessageBox.Show(" Please select Sql Database in the first ComboBox");
                return;
            }

            setSQlEdit();
        }

        private void SqlColored()
        {
            // getting keywords/functions
            string keywords = SqlMsSql.getMsSqlReserved();

            MatchCollection keywordMatches = Regex.Matches(RTBOdbcProp.Text.ToUpper(), keywords);

            // getting types/classes from the text 
            string types = @"\b(Console)\b";
            MatchCollection typeMatches = Regex.Matches(RTBOdbcProp.Text, types);

            // getting comments (inline or multiline)
            string comments = @"(\/\/.+?$|\/\*.+?\*\/)";
            MatchCollection commentMatches = Regex.Matches(RTBOdbcProp.Text, comments, RegexOptions.Multiline);

            // getting strings
            string strings = "\".+?\"";
            MatchCollection stringMatches = Regex.Matches(RTBOdbcProp.Text, strings);

            // saving the original caret position + forecolor
            int originalIndex = RTBOdbcProp.SelectionStart;
            int originalLength = RTBOdbcProp.SelectionLength;
            Color originalColor = Color.Black;

            // MANDATORY - focuses a label before highlighting (avoids blinking)
            this.Focus();

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
