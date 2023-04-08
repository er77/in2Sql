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
    public sealed partial class wf04EditQuery : Form
    {
       private Microsoft.Office.Interop.Excel.ListObject vCurrTable;
         

        private STool.CurrentTableRecords vCTR = STool.GetCurrentSql();

        public wf04EditQuery()
        {
            vCTR = STool.GetCurrentSql();

            if (vCTR.CurrCloudExTName != "")
             {
                SqlEngine.CurrExcelApp.ActiveSheet.ListObjects(vCTR.CurrCloudExTName).Range().Select();
             }

            InitializeComponent();
            SqlEditor.Language = FastColoredTextBoxNS.Language.SQL;

            SqlEditor.Text = vCTR.Sql;
             this.Text = "Sql Edit: " + vCTR.TableName; 
            SqlEditor_TextChanged( null, null);            
        }

      

        private void SqlEditTol_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Contains("New"))
                SqlEditor.Clear();

            else if (sender.ToString().Contains("Open"))
            {
                string vSql;

                vSql = vCurrTable.QueryTable.CommandText;

                vSql = IntSqlVbaEngine.RemoveBetween(vSql, '`', '`');
                vSql = vSql.Replace("/**/", "");

                SqlEditor.Text = vSql;
            }

            else if (sender.ToString().Contains("Save"))
                vCurrTable.QueryTable.CommandText = IntSqlVbaEngine.SetSqlLimit(IntSqlVbaEngine.GetOdbcNameFromCell(), SqlEditor.Text);

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

                if (vCTR.TypeConnection.Contains("ODBC"))
                {   vCurrTable.QueryTable.CommandText = IntSqlVbaEngine.SetSqlLimit(IntSqlVbaEngine.GetOdbcNameFromObject(vCurrTable), SqlEditor.Text);
                    IntSqlVbaEngine.ObjRefreshHistory(vCurrTable);                    
                }

                if (vCTR.TypeConnection.Contains("CLOUD"))
                {
                    SVbaEngineCloud.CreateExTable(
                                                         vCTR.CurrCloudName
                                                       , vCTR.TableName
                                                       , SqlEditor.Text
                                                       , 1
                                                       , vCTR.CurrCloudExTName) ;
                }
                 
                SqlEditor.ReadOnly = false;
            }

        }

        private void SqlECloseIm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TableName_Click(object sender, EventArgs e)
        {
         //   var vActivCell = SqlEngine.currExcelApp.ActiveCell;
        //    vCurrTable.QueryTable.Name = this.TableName.Text ;
        }

        private void SQLEditToolStrip_VisibleChanged(object sender, EventArgs e)
        {
           // vCurrObject.Name = this.TableName.Text;

        }        

        private void SqlEditor_TextChanged(object sender, EventArgs e)
        {
 
           
        }

        private void SqlEditor_Load(object sender, EventArgs e)
        {
            SqlEditor.Language = FastColoredTextBoxNS.Language.SQL;
        }

        private void In2SqlWF04EditQuery_Load(object sender, EventArgs e)
        {

        }
    }
}
