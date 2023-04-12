using System;
using System.Drawing;
using System.Windows.Forms;
using static SqlEngine.SCsv;

namespace SqlEngine
{
    public sealed partial class Wf10CsvEdit : Form
    {
        private string vCurrConnection;
        public Wf10CsvEdit()
        {
            if (Cursor.Current != null) Cursor = new Cursor(Cursor.Current.Handle);
            var pX = Cursor.Position.X - 300;
            var pY = Cursor.Position.Y - 30;

            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(pX, pY);
            
            RefreshList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderPropertiesList = FolderList();
            Close();
        }

        private void RefreshList ()
        {
            FolderPropertiesList = FolderList();
            CMBoxConnection.Items.Clear();
            foreach (var vCurrFolder in FolderPropertiesList)
            {
                CMBoxConnection.Items.Add(vCurrFolder.FolderName);
            }

        }

        private void wf10Create_Click(object sender, EventArgs e)
        {

            SRegistry.SetLocalValue("Csv." + CsvName.Text,"Path", CsvPath.Text);
            RefreshList();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  // delete
        {
            if (vCurrConnection.Length > 2)
            {
                SRegistry.DelLocalValue("Csv." + vCurrConnection);
                vCurrConnection = "";

                CsvName.Text = ""; 
                CsvPath.Text = "";
                RefreshList();


            }
        }

        private void CMBoxConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            vCurrConnection = CMBoxConnection.SelectedItem.ToString();    
            CsvName.Text = vCurrConnection;

            var vCurrFolderN = FolderPropertiesList.Find(item => item.FolderName == vCurrConnection);

            CsvPath.Text = vCurrFolderN.Path ;

        }

        private void wf10Browse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                var result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    CsvPath.Text=fbd.SelectedPath;

                }
            }
        }

        private void in2SqlWF10CsvEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
