using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace SqlEngine
{
    public sealed partial class Wf09CloudCloudConnectionEditor : Form
    {
        private String vConnType = "CH";

        public Wf09CloudCloudConnectionEditor(string vEditName="")
        {
           
            if (Cursor.Current != null)
                Cursor = new Cursor(Cursor.Current.Handle);
            var pX = Cursor.Position.X - 300;
            var pY = Cursor.Position.Y - 50;

            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(pX, pY);

            WF09BTOk.Enabled = false;
            rbClickHouse.Enabled = true;
            if (vEditName.Length > 2  )              
            {
                var vCurrRegKey = Registry.CurrentUser.OpenSubKey(@"Software\in2sql");
                tbURL.Text = SRegistry.GetLocalRegValue(vCurrRegKey, vEditName + ".Url");
                tbLogin.Text = SRegistry.GetLocalRegValue(vCurrRegKey, vEditName + ".Login");
                tbPassword.Text = SRegistry.GetLocalRegValue(vCurrRegKey, vEditName + ".Password");
                tbSQL.Text = SSqlLibrary.GetCloudSqlCheck(vEditName);
                var  vNm = vEditName.Split('.');
                if (vNm.Length > 1)
                    tbName.Text = vNm[1];
            }
        }

        private void WF09BTOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WF09BTCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WF09BTTest_Click(object sender, EventArgs e)
        {
            var vSqlUrl = SCloud.prepareCloudQuery_int(tbURL.Text, tbSQL.Text, tbLogin.Text, tbPassword.Text);

            vSqlUrl = STool.HttpGet(vSqlUrl);
            
            if (vSqlUrl.Length < 2)
            {
                MessageBox.Show(@"Test Failed");
                return;
            }
            MessageBox.Show(@"Test Passed ");
            WF09BTOk.Enabled = true;

            vSqlUrl = "Cloud" + vConnType + '.' + tbName.Text;

            SRegistry.SetLocalValue(vSqlUrl, "Url" , tbURL.Text) ;
            SRegistry.SetLocalValue(vSqlUrl, "Login", tbLogin.Text);
            SRegistry.SetLocalValue(vSqlUrl, "Password", tbPassword.Text);      

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
