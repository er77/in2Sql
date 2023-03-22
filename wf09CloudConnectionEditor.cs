using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlEngine
{
    public partial class wf09CloudConnectionEditor : Form
    {
        string vConnType = "CH";

        public wf09CloudConnectionEditor(string vEditName="")
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            int pX = Cursor.Position.X - 300;
            int pY = Cursor.Position.Y - 50;

            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(pX, pY);

            WF09BTOk.Enabled = false;
            rbClickHouse.Enabled = true;
            if (vEditName.Length > 2  )              
                {
                RegistryKey vCurrRegKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
                tbURL.Text = sRegistry.getLocalRegValue(vCurrRegKey, vEditName + ".Url");
                tbLogin.Text = sRegistry.getLocalRegValue(vCurrRegKey, vEditName + ".Login");
                tbPassword.Text = sRegistry.getLocalRegValue(vCurrRegKey, vEditName + ".Password");
                tbSQL.Text = SqlCloud.getCloudSqlCheck(vEditName);
                string[]  vNM = vEditName.Split('.');
                if (vNM.Count() > 1)
                    tbName.Text = vNM[1];
                }


        }

        private void WF09BTOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WF09BTCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WF09BTTest_Click(object sender, EventArgs e)
        {
            string vSqlURL;

            vSqlURL = sCloud.prepareCloudQuery_init(tbURL.Text, tbSQL.Text, tbLogin.Text, tbPassword.Text);

            vSqlURL = sTool.HttpGet(vSqlURL);
            
            if (vSqlURL.Length < 2)
            {
                MessageBox.Show("Test Failed");
                return;
            }

             MessageBox.Show("Test Passed ");
            WF09BTOk.Enabled = true;

            vSqlURL = "Cloud" + vConnType + '.' + tbName.Text;

            sRegistry.setLocalValue(vSqlURL, "Url" , tbURL.Text) ;
            sRegistry.setLocalValue(vSqlURL, "Login", tbLogin.Text);
            sRegistry.setLocalValue(vSqlURL, "Password", tbPassword.Text);      

        }

        private void rbClickHouse_CheckedChanged(object sender, EventArgs e)
        {
            vConnType = "CH";
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
