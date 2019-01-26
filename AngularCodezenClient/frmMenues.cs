using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AngularCodezenClient
{
    public partial class frmMenues : Form
    {
        public NgProject2 pr = new NgProject2();
        public List<string> Menues = new List<string>();
        public string SelectedMenu = "";
        public frmMenues()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str1 = CommonFunctions.GetNewName("New Menu",Menues);
            Menues.Add(str1);
            textBox1.Text = str1;
            SelectedMenu = str1;
            CommonFunctions.FillList(listBox1, Menues);
            CommonFunctions.SelectList(listBox1, str1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter the menu name");
                return;
            }

            CommonFunctions.ChangeValueIn(Menues, SelectedMenu, textBox1.Text);
            for (int i = 0; i < pr.Menues.Count; i++)
            {
                for (int j = 0; j < pr.Menues[i].Submenues.Count;j++)
                {
                    if (pr.Menues[i].Submenues[j].Menues == SelectedMenu)
                    {
                        pr.Menues[i].Submenues[j].Menues = textBox1.Text;
                    }
                }
            }
            CommonFunctions.FillList(listBox1, Menues);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void frmMenues_Load(object sender, EventArgs e)
        {
            CommonFunctions.FillList(listBox1, Menues);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.GetItemText(listBox1.SelectedItem);
            SelectedMenu = textBox1.Text;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Menues.Remove(SelectedMenu);
            CommonFunctions.FillList(listBox1, Menues);
            SelectedMenu = "";
            textBox1.Text = "";
        }
    }
}
