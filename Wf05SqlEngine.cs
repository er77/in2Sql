using System;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Windows.Forms;
namespace SqlEngine
{
    public partial class Wf05SqlEngine : Form
    {
        public Wf05SqlEngine()
        {
            InitializeComponent();

            ConnectionDropDownMenu();
            SqlDocument.Language = FastColoredTextBoxNS.Language.SQL;


        }

        private void ConnectionDropDownMenu()
        {
            contextMenuSqlConnections.Items.Clear();

            foreach (var vCurrConnMenu in SOdbc.OdbcPropertiesList.Select(odbcList => new ToolStripMenuItem(odbcList.OdbcName + " | odbc"  )))
            {
                vCurrConnMenu.Click += Connection_Click;
                contextMenuSqlConnections.Items.Add(vCurrConnMenu);
            }
            SqlConnectionsToolStripDropDown.DropDown = contextMenuSqlConnections;

            foreach (var vCurrConnMenu in SCloud.CloudPropertiesList.Select(cloud => new ToolStripMenuItem(cloud.CloudName + " | cloud")))
            {
                vCurrConnMenu.Click += Connection_Click;
                contextMenuSqlConnections.Items.Add(vCurrConnMenu);
            }
            SqlConnectionsToolStripDropDown.DropDown = contextMenuSqlConnections;

        }

        private void Connection_Click(object sender, EventArgs e)
        {
            ConnName.Text = sender.ToString();
        }
         
        private void in2SqlWF05SqlEngine_Load(object sender, EventArgs e)
        {
            
            SqlDocument.Select();
           
        }


        private void OpnSqlDocument()
        {
            var ofd = new OpenFileDialog
            {
                Filter = @"Text files (.sql)|*.sql",
                Title = @"Open Sql Script..."
            };
            
            if (ofd.ShowDialog() != DialogResult.OK) return;
            
            var sr = new System.IO.StreamReader(ofd.FileName);
            SqlDocument.Text = sr.ReadToEnd();
        }

        private void SaveSqlDocument()
        {
            var svf = new SaveFileDialog
            {
                Filter = @"Text files (.sql)|*.sql",
                Title = @"Save Sql Script..."
            };
            
            if (svf.ShowDialog() != DialogResult.OK) return;
            
            var sw = new System.IO.StreamWriter(svf.FileName);
            sw.Write(SqlDocument.Text);
            sw.Close();
        }

        BackgroundWorker bw  = new BackgroundWorker();  
        
        private string GetSql ()
        {
            var sqlDocumentText = SqlDocument.SelectedText;

            if (sqlDocumentText == "")
                sqlDocumentText = SqlDocument.Text;

            sqlDocumentText = sqlDocumentText.Replace(Environment.NewLine, " ");
            sqlDocumentText = sqlDocumentText.Replace("\r", " ");
            sqlDocumentText = sqlDocumentText.Replace("\n", " ");
            sqlDocumentText = sqlDocumentText.Replace("\t", " ");
            sqlDocumentText = sqlDocumentText.Trim();
            return sqlDocumentText;
        }

        private void EditTollMenu_Click(object sender, EventArgs e)
        {
            STool.RunGarbageCollector();

            SqlDocument.ReadOnly = true;
            string qstr = GetSql(); 

            if (sender.ToString().Contains("New"))
                SqlDocument.Clear();

            else if (sender.ToString().Contains("Open"))
                OpnSqlDocument();

            else if (sender.ToString().Contains("Save"))
                SaveSqlDocument();

            else if (sender.ToString().Contains("Undo"))
                SqlDocument.Undo();

            else if (sender.ToString().Contains("Redo"))
                SqlDocument.Redo();

            else if (sender.ToString().Contains("Cut"))
                SqlDocument.Cut();

            else if (sender.ToString().Contains("Copy"))
                SqlDocument.Copy();

            else if (sender.ToString().Contains("Paste"))
                SqlDocument.Paste();

            else if (sender.ToString().Contains("sqlRun"))            
                if (bw.IsBusy == false )                                     
                    SqlExecuteandDataGrid(qstr);                          
                else
                    MessageBox.Show(@"This Sql Engine is busy. Please create new one", @"sql run event",
                                                                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);             

            else if (sender.ToString().Contains("SqlConnections"))
                ConnectionDropDownMenu();

            else if (sender.ToString().Contains("Excel"))
                SqlExecExcel(qstr);

            else
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), @"Menu Items Event",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

            SqlDocument.ReadOnly = false;

        }

     /*   private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Action action = () => SqlDocument.ReadOnly = false;
            SqlDocument.Invoke(action);
        }
        */

        private void  OdbcGrid (string odbcName , string sqlCommand)
        {
            try
            {
                var odbcProperties = SOdbc.GetOdbcProperties(odbcName, "DSNStr"); 

                if (odbcProperties == null | odbcProperties == "")
                {
                    MessageBox.Show(@"Please make the connection by expand list on the left pane ", @"sql run event",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                IntSqlVbaEngine.SetSqlLimit(odbcName, sqlCommand);
                STool.AddSqlLog(odbcName, sqlCommand);

                using
                        (OdbcConnection conn = new OdbcConnection(odbcProperties))
                        using (var cmnd = new OdbcDataAdapter(sqlCommand, conn))
                        {
                            var table = new DataTable();
                            cmnd.Fill(table);                     
                            SqlDataResult.DataSource = table;
                        }
            }
            catch (Exception e)
            {
                if ((e.HResult == -2147024809) == false)
                    STool.ExpHandler(e, "OdbcGrid");
            }
        }

        private void CloudGrid(string vCloudName, string vCurrSql)
        {
            try
            {
                if (vCurrSql == null | vCloudName == null | vCurrSql == "" | vCloudName == "")
                    return;              
                 
                var vConnUrl = SCloud.PrepareCloudQuery(vCloudName, vCurrSql );

                if (vConnUrl == null | vConnUrl == "")
                {
                    MessageBox.Show(@"Please make the connection by expand list on the left pane ", @"sql run event",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var vTempFile = STool.WriteHttpToFile(vConnUrl);

                SqlDataResult.DataSource = STool.ConvertCsVtoDataTable(vTempFile,',');
                STool.DeleteFile(vTempFile);
                
            }
            catch (Exception e)
            { 
                    STool.ExpHandler(e, "CloudGrid");
            }
        }

        private void SqlExecuteandDataGrid(string sqlCommand)
        {
            // await Task.Delay(1);

            if (ConnName.Text.Equals("SQL") | ConnName.Text == "")
            {
                MessageBox.Show(@"Please select Sql connection on the rigth drop-down menu", @"sql run event",
                                                                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                SqlDataResult.SelectAll();
                SqlDataResult.ClearSelection();

                string[] vTempName = ConnName.Text.Split('|');
                string vOdbcName = vTempName[0].Trim();
                if (vTempName.Count() > 1)
                    if (vTempName[1].ToUpper().Contains("ODBC"))
                        OdbcGrid(vOdbcName, sqlCommand);
                    else if (vTempName[1].ToUpper().Contains("CLOUD"))
                        CloudGrid(vOdbcName, sqlCommand); 
            }
            catch (Exception e)
            {
                if ((e.HResult == -2147024809) == false)
                    STool.ExpHandler(e, "SqlExecuteandDataGrid");                    
            } 
        }

        private void SqlExecExcel(string qstr)
        {
            // await Task.Delay(1);

            if (ConnName.Text.Equals("SQL"))
            {
                MessageBox.Show(@"Please select Sql connection on the rigth drop-down menu", @"sql run event",
                                                                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {   var vTempName = ConnName.Text.Split('|');
                var vOdbcName = vTempName[0].Trim();
                qstr = "select * from ( " + qstr + " ) df where 1=1 ";
                
                if (vTempName.Count() <= 1) return;
                
                if (vTempName[1].ToUpper().Contains("ODBC"))
                    IntSqlVbaEngine.CreateExTable(ConnName.Text, STool.GetHash(qstr), qstr);
                    
                else if (vTempName[1].ToUpper().Contains("CLOUD"))                    
                    SVbaEngineCloud.CreateExTable(vOdbcName, STool.GetHash(qstr), qstr);

            }
            catch (Exception e)
            {
                if ((e.HResult == -2147024809) == false)
                    STool.ExpHandler(e, @"SqlExecuteandDataGrid");
            }
        }


/*        private void SqlDocument_TextChanged(object sender, EventArgs e)
        {

            
        }
*/
        private void SqlConnectionsToolStripDropDown_Click(object sender, EventArgs e)
        {

        }

        private void SqlDocument_Load(object sender, EventArgs e)
        {
            SqlDocument.Text = @"Free Sql Manager \n\r  https://t.me/in2sql  \n\r https://sourceforge.net/projects/in2sql/ \n\r erasyuk@gmail.com ";
        }

        private void SaveToExTable_ButtonClick(object sender, EventArgs e)
        {

        }
    }
}
