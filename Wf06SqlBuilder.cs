using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SqlEngine
{
    public partial class Wf06SqlBuilder : Form
    {
        private static readonly List<TreeNode> VTableList = new List<TreeNode>() ;

        private  string vConnType = "ODBC";

        public Wf06SqlBuilder()
        {
            InitializeComponent();
            TBJoiner.AllowDrop = true;

            TBJoiner.DragDrop += TBJoiner_DragDrop;
            TBJoiner.DragEnter += TBJoiner_DragEnter;
        }

        private void TBJoiner_TextChanged(object sender, EventArgs e)
        {

        }

        public void SetLblConnectionName (string lblName, string vCurrConnType = "ODBC" )
        {
            this.lblConnectionName.Text = lblName;
            vConnType = vCurrConnType;
        }

        private string GetLblConnectionName( )
        {
           return  lblConnectionName.Text;
        }

        private void TBJoiner_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.Text) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private TreeNode vMainTable ;

        private void  DrawSelect ()
        {
            TBJoiner.Text = @"SELECT \r\n\t 1 cnt \r\n ";

            var i = 0;
            foreach (var tn in VTableList)
            {
                i = i + 1;
                foreach (TreeNode tb in tn.Nodes)
                    if (tb.Text.Equals("Indexes") == false)
                    {
                        var vClmnName = tb.Text.Split('|');
                        vClmnName[0] = vClmnName[0].Trim();
                        if ( i == 1 )
                          TBJoiner.Text = TBJoiner.Text + @"\t , a" + i + ".\"" + vClmnName[0] + "\" \r\n";
                        else
                          TBJoiner.Text = TBJoiner.Text + @"\t , a" + i + ".\"" + vClmnName[0] + "\" \t  a" + i + "_" + vClmnName[0] + "\r\n";
                    }
            }
            i = 0;
            var vInnerJoin = "";
            foreach (var tn in VTableList)
            {
                i = i + 1;
                if (i == 1)
                {
                    TBJoiner.Text = TBJoiner.Text + @" FROM " + tn.Text + @" a" + i + @" \r\n";
                    continue;
                }

                TBJoiner.Text = TBJoiner.Text + @"\t  left join  " + tn.Text + @" a" + i + @" on  \t 1=1 \r\n";
                foreach (TreeNode vCurrColumn in tn.Nodes)
                {

                    var j = 0;
                    foreach (var tl in VTableList)
                    {
                        j = j + 1;
                        if (!(i >= j & i != j)) continue;
                        
                        foreach (TreeNode vClm in tl.Nodes)
                            if (vClm.Text.Equals(vCurrColumn.Text) & (vClm.Text.Equals("Indexes") == false))
                            {
                                var vClmnName = vCurrColumn.Text.Split('|');
                                vClmnName[0] = vClmnName[0].Trim();
                                if (vClmnName[0].ToUpper().Contains("DATE") | vClmnName[0].ToUpper().Contains("GUID"))
                                {
                                    TBJoiner.Text = TBJoiner.Text + @"\t \t /* and  a" + i + "." + vClmnName[0] + @" =  a" + j + "." + vClmnName[0] + "  */ \r\n";
                                    vInnerJoin = vInnerJoin + "\t /* and a" + i + "." + vClmnName[0] + " is not null */ \r\n";
                                }
                                else
                                {
                                    TBJoiner.Text = TBJoiner.Text + @"\t \t and  a" + i + ".\"" + vClmnName[0] + "\" =  a" + j + ".\"" + vClmnName[0] + "\"\r\n";
                                    vInnerJoin = vInnerJoin + "\t and a" + i + ".\"" + vClmnName[0] + "\" is not null \r\n";
                                }
                                    
                            }
                    }
                }

                TBJoiner.Text = TBJoiner.Text + @" \r\n ";


            }

            TBJoiner.Text = TBJoiner.Text + @" WHERE 1=1 \r\n ";
            TBJoiner.Text = TBJoiner.Text + vInnerJoin;

            SqlColored();

        }

        private void SqlColored( )
        {
            // getting keywords/functions
            var keywords = SSqlLibrary.GetMsSqlReserved();

            var keywordMatches = Regex.Matches(TBJoiner.Text.ToUpper(), keywords);

            // getting types/classes from the text 
            const string types = @"\b(Console)\b";
            var typeMatches = Regex.Matches(TBJoiner.Text, types);

            // getting comments (inline or multiline)
            const string comments = @"(\/\/.+?$|\/\*.+?\*\/)";
            var commentMatches = Regex.Matches(TBJoiner.Text, comments, RegexOptions.Multiline);

            // getting strings
            const string strings = "\".+?\"";
            var stringMatches = Regex.Matches(TBJoiner.Text, strings);

            // saving the original caret position + forecolor
            var originalIndex = TBJoiner.SelectionStart;
            var originalLength = TBJoiner.SelectionLength;
            var originalColor = Color.Black;

            // MANDATORY - focuses a label before highlighting (avoids blinking)
            Focus();

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

        private void TBJoiner_DragDrop(object sender, DragEventArgs e)
        {
            var str = e.Data.GetData(DataFormats.Text).ToString();
          //  var miSelectNode = in2SqlWF03PanelRigtSqlM.getNode(e.X, e.Y);
            //MessageBox.Show(str);

            var vtb = Wf03PanelRightSqlM.CurrSqlPanel.FindeTable(str, GetLblConnectionName());
            if (vtb == null) return;
            
            if (vtb.Nodes.Count < 2)
            {
                MessageBox.Show(@"Please expand table columns by clicking on the table name ");
                return;
            }

            VTableList.Add(vtb);
            if (VTableList.Count == 1)
                vMainTable = vtb;
            TBJoiner.Clear();

            DrawSelect();
            //throw new NotImplementedException();
        }

        private void TBJoiner_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void newToolStripButton1_Click(object sender, EventArgs e)
        {
            VTableList.Clear();
            vMainTable = null;
            TBJoiner.Clear();
        }

/*        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_DropDown(object sender, EventArgs e)
        {

        }
*/
        private void copyToolStripButton1_Click(object sender, EventArgs e)
        {
            TBJoiner.SelectAll();
            TBJoiner.Copy();
            TBJoiner.Focus();
        }
    }
}
