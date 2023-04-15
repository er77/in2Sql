using System;
using System.Drawing;
using System.Windows.Forms;
namespace SqlEngine
{
    public sealed partial class Wf01Login : Form
    {
        private readonly string odbcName;
        public Wf01Login(string currOdbcName)
        {
            if (currOdbcName == null || Cursor.Current == null) return;
            
            Cursor = new Cursor(Cursor.Current.Handle);
            var pX = Cursor.Position.X - 200;
            var pY = Cursor.Position.Y - 50;
            odbcName = currOdbcName;
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(pX, pY);
            WF01BTOk.Enabled = false;
        }

  /*      private void in2SqlWF01Login_Load(object sender, EventArgs e)
        {
            WF01LblODBC.Text = "ODBC: " + odbcName;
            WF01TBLogin.Text = odbcName;
            WF01BTOk.Enabled = false;
        }
*/
        private void WF01BTTest_Click(object sender, EventArgs e)
        {
            var currOdbc = SOdbc.OdbcPropertiesList.Find(item => item.OdbcName == odbcName);
            currOdbc.Login = WF01TBLogin.Text;
            currOdbc.Password = WF01TBPassword.Text;
            SOdbc.ChangeOdbcValue(odbcName, currOdbc);

            SOdbc.CheckOdbcStatus(odbcName);

            currOdbc = SOdbc.OdbcPropertiesList.Find(item => item.OdbcName == odbcName);
            
            if (currOdbc.ConnStatus == 1)
            {
                WF01BTOk.Enabled = true;
                MessageBox.Show(@"Test passed");
                SRegistry.SetLocalValue(odbcName, "Login", currOdbc.Login);
                SRegistry.SetLocalValue(odbcName, "Password", currOdbc.Password);

            }
            else
            { 
                MessageBox.Show(currOdbc.ConnErrMsg);
            }

            STool.RunGarbageCollector();
        }

        private void WF01BTOk_Click(object sender, EventArgs e)
        {
            Close(); //Application.Exit();
        }

        private void WF01BTCancel_Click(object sender, EventArgs e)
        {
            Close(); //Application.Exit();
        }

        private void WF01LblPassword_Click(object sender, EventArgs e)
        {

        }

        private void WF01LblLogin_Click(object sender, EventArgs e)
        {

        }

  
    }
}
