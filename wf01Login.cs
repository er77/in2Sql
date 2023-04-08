using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlEngine
{
    public sealed partial class wf01Login : Form
    {
        String vODBCName;
        public wf01Login(String vCurrODBCName)
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            int pX = Cursor.Position.X - 200;
            int pY = Cursor.Position.Y - 50;
            vODBCName = vCurrODBCName;
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(pX, pY);
            WF01BTOk.Enabled = false;
        }

        private void in2SqlWF01Login_Load(object sender, EventArgs e)
        {
            WF01LblODBC.Text = "ODBC: " + vODBCName;
            WF01TBLogin.Text = vODBCName;
            WF01BTOk.Enabled = false;
        }

        private void WF01BTTest_Click(object sender, EventArgs e)
        {
            var vCurrODBC = SOdbc.vODBCList.Find(item => item.OdbcName == vODBCName);
            vCurrODBC.Login = WF01TBLogin.Text;
            vCurrODBC.Password = WF01TBPassword.Text;
            SOdbc.ChangeOdbcValue(vODBCName, vCurrODBC);

            SOdbc.checkOdbcStatus(vODBCName);

            vCurrODBC = SOdbc.vODBCList.Find(item => item.OdbcName == vODBCName);
            DialogResult result;
            if (vCurrODBC.ConnStatus == 1)
            {
                WF01BTOk.Enabled = true;
                result = MessageBox.Show("Test passed".ToString());
                sRegistry.setLocalValue(vODBCName, "Login", vCurrODBC.Login);
                sRegistry.setLocalValue(vODBCName, "Password", vCurrODBC.Password);

            }
            else
            {
                result = MessageBox.Show(vCurrODBC.ConnErrMsg);
            }

            sTool.RunGarbageCollector();
        }

        private void WF01BTOk_Click(object sender, EventArgs e)
        {
            this.Close(); //Application.Exit();
        }

        private void WF01BTCancel_Click(object sender, EventArgs e)
        {
            this.Close(); //Application.Exit();
        }

        private void WF01LblPassword_Click(object sender, EventArgs e)
        {

        }

        private void WF01LblLogin_Click(object sender, EventArgs e)
        {

        }

  
    }
}
