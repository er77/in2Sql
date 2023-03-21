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
    public partial class wf06SqlBuilder : Form
    {
        private static List<TreeNode> vTableList = new List<TreeNode>() ;

        private  string vConnType = "ODBC";

        public wf06SqlBuilder()
        {
            InitializeComponent();
            this.TBJoiner.AllowDrop = true;

            this.TBJoiner.DragDrop += TBJoiner_DragDrop;
            this.TBJoiner.DragEnter += TBJoiner_DragEnter;
        }

        private void TBJoiner_TextChanged(object sender, EventArgs e)
        {

        }

        public void setLblConnectionName (string lblName, string vCurrConnType = "ODBC" )
        {
            this.lblConnectionName.Text = lblName;
            vConnType = vCurrConnType;
        }

        public string getLblConnectionName( )
        {
           return  this.lblConnectionName.Text;
        }

        public void TBJoiner_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text)) 
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        TreeNode vMainTable = null;

        private void  drawSelect ()
        {
            TBJoiner.Text = "SELECT \r\n\t 1 cnt \r\n ";

            int i = 0;
            foreach (TreeNode tn in vTableList)
            {
                i = i + 1;
                foreach (TreeNode tb in tn.Nodes)
                    if (tb.Text.Equals("Indexes") == false)
                    {
                        string[] vClmnName = tb.Text.Split('|');
                        vClmnName[0] = vClmnName[0].Trim();
                        if ( i == 1 )
                          TBJoiner.Text = TBJoiner.Text + "\t , a" + i + ".\"" + vClmnName[0] + "\" \r\n";
                        else
                          TBJoiner.Text = TBJoiner.Text + "\t , a" + i + ".\"" + vClmnName[0] + "\" \t  a" + i + "_" + vClmnName[0] + "\r\n";
                    }
            }
            i = 0;
            string vInnerJoin = "";
            foreach (TreeNode tn in vTableList)
            {
                i = i + 1;
                if (i == 1)
                {
                    TBJoiner.Text = TBJoiner.Text + " FROM " + tn.Text + " a" + i + " \r\n";
                    continue;
                }

                TBJoiner.Text = TBJoiner.Text + "\t  left join  " + tn.Text + " a" + i + " on  \t 1=1 \r\n";
                foreach (TreeNode vCurrColumn in tn.Nodes)
                {

                    int j = 0;
                    foreach (TreeNode tl in vTableList)
                    {
                        j = j + 1;
                        if (i >= j & i != j)
                            foreach (TreeNode vClm in tl.Nodes)
                                if (vClm.Text.Equals(vCurrColumn.Text) & (vClm.Text.Equals("Indexes") == false))
                                {
                                    string[] vClmnName = vCurrColumn.Text.Split('|');
                                    vClmnName[0] = vClmnName[0].Trim();
                                    if (vClmnName[0].ToUpper().Contains("DATE") | vClmnName[0].ToUpper().Contains("GUID"))
                                    {
                                        TBJoiner.Text = TBJoiner.Text + "\t \t /* and  a" + i + "." + vClmnName[0] + " =  a" + j + "." + vClmnName[0] + "  */ \r\n";
                                        vInnerJoin = vInnerJoin + "\t /* and a" + i + "." + vClmnName[0] + " is not null */ \r\n";
                                    }
                                    else
                                    {
                                        TBJoiner.Text = TBJoiner.Text + "\t \t and  a" + i + ".\"" + vClmnName[0] + "\" =  a" + j + ".\"" + vClmnName[0] + "\"\r\n";
                                        vInnerJoin = vInnerJoin + "\t and a" + i + ".\"" + vClmnName[0] + "\" is not null \r\n";
                                    }
                                    
                                }
                    }
                }

                TBJoiner.Text = TBJoiner.Text + " \r\n ";


            }

            TBJoiner.Text = TBJoiner.Text + " WHERE 1=1 \r\n ";
            TBJoiner.Text = TBJoiner.Text + vInnerJoin;

            SqlColored();

        }

        private void SqlColored( )
        {
            // getting keywords/functions
            string keywords = sLibrary.getMsSqlReserved();

            MatchCollection keywordMatches = Regex.Matches(TBJoiner.Text.ToUpper(), keywords);

            // getting types/classes from the text 
            string types = @"\b(Console)\b";
            MatchCollection typeMatches = Regex.Matches(TBJoiner.Text, types);

            // getting comments (inline or multiline)
            string comments = @"(\/\/.+?$|\/\*.+?\*\/)";
            MatchCollection commentMatches = Regex.Matches(TBJoiner.Text, comments, RegexOptions.Multiline);

            // getting strings
            string strings = "\".+?\"";
            MatchCollection stringMatches = Regex.Matches(TBJoiner.Text, strings);

            // saving the original caret position + forecolor
            int originalIndex = TBJoiner.SelectionStart;
            int originalLength = TBJoiner.SelectionLength;
            Color originalColor = Color.Black;

            // MANDATORY - focuses a label before highlighting (avoids blinking)
            this.Focus();

            // removes any previous highlighting (so modified words won't remain highlighted)
            TBJoiner.SelectionStart = 0;
            TBJoiner.SelectionLength = TBJoiner.Text.Length;
            TBJoiner.SelectionColor = originalColor;

            // scanning...
            foreach (Match m in keywordMatches)
            {
                TBJoiner.SelectionStart = m.Index;
                TBJoiner.SelectionLength = m.Length;
                TBJoiner.SelectionColor = Color.Blue;
            }

            foreach (Match m in typeMatches)
            {
                TBJoiner.SelectionStart = m.Index;
                TBJoiner.SelectionLength = m.Length;
                TBJoiner.SelectionColor = Color.DarkCyan;
            }


            foreach (Match m in stringMatches)
            {
                TBJoiner.SelectionStart = m.Index;
                TBJoiner.SelectionLength = m.Length;
                TBJoiner.SelectionColor = Color.Brown;
            }


            foreach (Match m in commentMatches)
            {
                TBJoiner.SelectionStart = m.Index;
                TBJoiner.SelectionLength = m.Length;
                TBJoiner.SelectionColor = Color.Green;
            }

            // restoring the original colors, for further writing
            TBJoiner.SelectionStart = originalIndex;
            TBJoiner.SelectionLength = originalLength;
            TBJoiner.SelectionColor = originalColor;

            // giving back the focus
            TBJoiner.Focus();
        }

        public   void TBJoiner_DragDrop(object sender, DragEventArgs e)
        {
            string str = e.Data.GetData(DataFormats.Text).ToString();
          //  var miSelectNode = in2SqlWF03PanelRigtSqlM.getNode(e.X, e.Y);
            //MessageBox.Show(str);

            TreeNode vtb = wf03PanelRigtSqlM.CurrSqlPanel.findeTable(str, getLblConnectionName());
            if ((vtb == null) == false)
            {
                if (vtb.Nodes.Count < 2)
                {
                    MessageBox.Show("Please expand table columns by clicking on the table name ");
                    return;
                }

                vTableList.Add(vtb);
                if (vTableList.Count == 1)
                    vMainTable = vtb;
                TBJoiner.Clear();

                drawSelect();

            }
            //throw new NotImplementedException();
        }

        public void TBJoiner_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void newToolStripButton1_Click(object sender, EventArgs e)
        {
            vTableList.Clear();
            vMainTable = null;
            TBJoiner.Clear();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_DropDown(object sender, EventArgs e)
        {

        }

        private void copyToolStripButton1_Click(object sender, EventArgs e)
        {
            TBJoiner.SelectAll();
            TBJoiner.Copy();
            TBJoiner.Focus();
        }
    }
}
