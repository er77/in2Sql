using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SqlEngine.sCloud;

namespace SqlEngine
{
    public partial class wf05SqlEngine : Form
    {
        public wf05SqlEngine()
        {
            InitializeComponent();

            ConnectionDropDownMenu();
            SqlDocument.Language = FastColoredTextBoxNS.Language.SQL;


        }

        private void ConnectionDropDownMenu()
        {
            contextMenuSqlConnections.Items.Clear();

            foreach (var vCurrvODBCList in sODBC.vODBCList)
            {
                ToolStripMenuItem vCurrConnMenu = new ToolStripMenuItem(vCurrvODBCList.OdbcName + " | odbc"  );
                vCurrConnMenu.Click += Connection_Click;
                contextMenuSqlConnections.Items.Add(vCurrConnMenu);
            }
            SqlConnectionsToolStripDropDown.DropDown = contextMenuSqlConnections;

            foreach (var vCurrvCloud in sCloud.vCloudList)
            {
                ToolStripMenuItem vCurrConnMenu = new ToolStripMenuItem(vCurrvCloud.CloudName + " | cloud");
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


        private void opnSqlDocument()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Text files (.sql)|*.sql",
                Title = "Open Sql Script..."
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                SqlDocument.Text = sr.ReadToEnd();
            }
            ofd = null;
        }

        private void saveSqlDocument()
        {
            SaveFileDialog svf = new SaveFileDialog
            {
                Filter = "Text files (.sql)|*.sql",
                Title = "Save Sql Script..."
            };
            if (svf.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                sw.Write(SqlDocument.Text);
                sw.Close();
            }
            svf = null;
        }

        BackgroundWorker bw  = new BackgroundWorker();  
        
        private string getSql ()
        {
            string qstr = SqlDocument.SelectedText;

            if (qstr == "")
                qstr = SqlDocument.Text;

            qstr = qstr.Replace(System.Environment.NewLine, " ");
            qstr = qstr.Replace("\r", " ");
            qstr = qstr.Replace("\n", " ");
            qstr = qstr.Replace("\t", " ");
            qstr = qstr.Trim();
            return qstr;
        }

        private void EditTollMenu_Click(object sender, EventArgs e)
        {
            sTool.RunGarbageCollector();

            SqlDocument.ReadOnly = true;
            string qstr = getSql(); 

            if (sender.ToString().Contains("New"))
                SqlDocument.Clear();

            else if (sender.ToString().Contains("Open"))
                opnSqlDocument();

            else if (sender.ToString().Contains("Save"))
                saveSqlDocument();

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
                    sqlExecuteandDataGrid(qstr);                          
                else
                    MessageBox.Show("This Sql Engine is busy. Please create new one", "sql run event",
                                                                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);             

            else if (sender.ToString().Contains("SqlConnections"))
                ConnectionDropDownMenu();

            else if (sender.ToString().Contains("Excel"))
                sqlExecExcel(qstr);

            else
                MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

            SqlDocument.ReadOnly = false;

        }

     /*   private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Action action = () => SqlDocument.ReadOnly = false;
            SqlDocument.Invoke(action);
        }
        */

        private void  OdbcGrid (string vOdbcName , string vSqlCommand)
        {
            try
            {
                string DsnConn = sODBC.getODBCProperties(vOdbcName, "DSNStr"); 

                if (DsnConn == null | DsnConn == "")
                {
                    MessageBox.Show("Please make the connection by expand list on the left pane ", "sql run event",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                VbaEngine.setSqlLimit(vOdbcName, vSqlCommand);
                sTool.addSqlLog(vOdbcName, vSqlCommand);

                using
                        (OdbcConnection conn = new System.Data.Odbc.OdbcConnection(DsnConn))
                        using (OdbcDataAdapter cmnd = new OdbcDataAdapter(vSqlCommand, conn))
                        {
                            DataTable table = new DataTable();
                            cmnd.Fill(table);                     
                            this.SqlDataResult.DataSource = table;
                        }
            }
            catch (Exception e)
            {
                if ((e.HResult == -2147024809) == false)
                    sTool.ExpHandler(e, "OdbcGrid");
            }
        }

        private void CloudGrid(string vCloudName, string vCurrSql)
        {
            try
            {
                if (vCurrSql == null | vCloudName == null | vCurrSql == "" | vCloudName == "")
                    return;              
                 
                string vConnURL = sCloud.prepareCloudQuery(vCloudName, vCurrSql );

                if (vConnURL == null | vConnURL == "")
                {
                    MessageBox.Show("Please make the connection by expand list on the left pane ", "sql run event",
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string vTempFile = sTool.writeHttpToFile(vConnURL);

                this.SqlDataResult.DataSource = sTool.ConvertCSVtoDataTable(vTempFile,',');
                 sTool.deleteFile(vTempFile);
                
            }
            catch (Exception e)
            { 
                    sTool.ExpHandler(e, "CloudGrid");
            }
        }

        private void sqlExecuteandDataGrid(string SqlCommand)
        {
            // await Task.Delay(1);

            if (ConnName.Text.Equals("SQL") | ConnName.Text == "")
            {
                MessageBox.Show("Please select Sql connection on the rigth drop-down menu", "sql run event",
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
                        OdbcGrid(vOdbcName, SqlCommand);
                    else if (vTempName[1].ToUpper().Contains("CLOUD"))
                        CloudGrid(vOdbcName, SqlCommand); 
            }
            catch (Exception e)
            {
                if ((e.HResult == -2147024809) == false)
                    sTool.ExpHandler(e, "sqlExecuteandDataGrid");                    
            } 
        }

        private void sqlExecExcel(string qstr)
        {
            // await Task.Delay(1);

            if (ConnName.Text.Equals("SQL"))
            {
                MessageBox.Show("Please select Sql connection on the rigth drop-down menu", "sql run event",
                                                                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {   string[] vTempName = ConnName.Text.Split('|');
                string vOdbcName = vTempName[0].Trim();
                qstr = "select * from ( " + qstr + " ) df where 1=1 ";
                if (vTempName.Count() > 1)
                    if (vTempName[1].ToUpper().Contains("ODBC"))
                         VbaEngine.createExTable(ConnName.Text, sTool.GetHash(qstr), qstr);
                    
                    else if (vTempName[1].ToUpper().Contains("CLOUD"))                    
                         sVbaEngineCloud.createExTable(vOdbcName, sTool.GetHash(qstr), qstr);
                    
            }
            catch (Exception e)
            {
                if ((e.HResult == -2147024809) == false)
                    sTool.ExpHandler(e, "sqlExecuteandDataGrid");
            }
        }


        private void SqlDocument_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void SqlConnectionsToolStripDropDown_Click(object sender, EventArgs e)
        {

        }

        private void SqlDocument_Load(object sender, EventArgs e)
        {
            SqlDocument.Text = "Free Sql Manager \n\r  https://t.me/in2sql  \n\r https://sourceforge.net/projects/in2sql/ \n\r erasyuk@gmail.com ";
        }

        private void SaveToExTable_ButtonClick(object sender, EventArgs e)
        {

        }
    }
}
