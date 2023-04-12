using System;
using System.Windows.Forms;

namespace SqlEngine
{
    public sealed partial class Wf04EditQuery : Form
    {         
        private readonly STool.CurrentTableRecords currentTableRecords;
        public Wf04EditQuery()
        {
            currentTableRecords = STool.GetCurrentSql();

            if (currentTableRecords.CurrCloudExTName != "")
             {
                SqlEngine.CurrExcelApp.ActiveSheet.ListObjects(currentTableRecords.CurrCloudExTName).Range().Select();
             }
                                       
            InitializeComponent();
            SqlEditor.Language = FastColoredTextBoxNS.Language.SQL;

            SqlEditor.Text = currentTableRecords.Sql;
            Text = @"Sql Edit: " + currentTableRecords.TableName; 
            //SqlEditor_TextChanged( null, null);            
        }      

        private void SqlEditTol_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("New"))
                SqlEditor.Clear();

            else if (sender.ToString().Contains("Open"))
            {
                string vSql;

                vSql = currTable.QueryTable.CommandText;

                vSql = IntSqlVbaEngine.RemoveBetween(vSql, '`', '`');
                vSql = vSql.Replace("/**/", "");

                SqlEditor.Text = vSql;
            }

            else if (sender.ToString().Contains("Save"))
                currTable.QueryTable.CommandText = IntSqlVbaEngine.SetSqlLimit(IntSqlVbaEngine.GetOdbcNameFromCell(), SqlEditor.Text);

            else if (sender.ToString().Contains("Cut"))
                SqlEditor.Cut();

            else if (sender.ToString().Contains("Copy"))
                SqlEditor.Copy();

            else if (sender.ToString().Contains("Paste"))
            {
                  
                SqlEditor.Paste();
                
            }

            else if (sender.ToString().Contains("Execute"))
            {
                SqlEditor.ReadOnly = true;
                SQLEditToolStrip.Focus();

                if (currentTableRecords.TypeConnection.Contains("ODBC"))
                {   currTable.QueryTable.CommandText = IntSqlVbaEngine.SetSqlLimit(IntSqlVbaEngine.GetOdbcNameFromObject(currTable), SqlEditor.Text);
                    IntSqlVbaEngine.ObjRefreshHistory(currTable);                    
                }

                if (currentTableRecords.TypeConnection.Contains("CLOUD"))
                {
                    SVbaEngineCloud.CreateExTable(
                                                         currentTableRecords.CurrCloudName
                                                       , currentTableRecords.TableName
                                                       , SqlEditor.Text
                                                       , 1
                                                       , currentTableRecords.CurrCloudExTName) ;
                }
                 
                SqlEditor.ReadOnly = false;
            }

        }

  /*      private void SqlECloseIm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TableName_Click(object sender, EventArgs e)
        {
         //   var vActivCell = SqlEngine.currExcelApp.ActiveCell;
        //    currTable.QueryTable.Name = this.TableName.Text ;
        }
*/
        private void SQLEditToolStrip_VisibleChanged(object sender, EventArgs e)
        {
           // vCurrObject.Name = this.TableName.Text;

        }        

/*        private void SqlEditor_TextChanged(object sender, EventArgs e)
        {
 
           
        }
*/
        private void SqlEditor_Load(object sender, EventArgs e)
        {
            SqlEditor.Language = FastColoredTextBoxNS.Language.SQL;
        }

        private void In2SqlWF04EditQuery_Load(object sender, EventArgs e)
        {

        }
    }
}
