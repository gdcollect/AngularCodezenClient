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
    public partial class frmTabs : Form
    {
        public NgSubMenu2 SubMenu = new NgSubMenu2();
        public List<string> TabsList = new List<string>();
        private string SelectedTab;
        public frmTabs()
        {
            InitializeComponent();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            var newName = CommonFunctions.GetNewName("Tab", TabsList);
            textBox1.Text = newName;
            SelectedTab = newName;
            TabsList.Add(newName);
            CommonFunctions.FillList(listBox1, TabsList);
            CommonFunctions.SelectList(listBox1, newName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter the Tab name");
                return;
            }
            CommonFunctions.ChangeValueIn(TabsList, SelectedTab, textBox1.Text);
            for (int i = 0; i < SubMenu.Model.Fields.Count; i++)
            {
                if (SubMenu.Model.Fields[i].TabName == SelectedTab)
                {
                    SubMenu.Model.Fields[i].TabName = textBox1.Text;
                }
            }
            CommonFunctions.FillList(listBox1, TabsList);
        }

        private void frmTabs_Load(object sender, EventArgs e)
        {
            CommonFunctions.FillList(listBox1, TabsList);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.GetItemText(listBox1.SelectedItem);
            SelectedTab = textBox1.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TabsList.Remove(SelectedTab);
            SelectedTab = "";
            textBox1.Text = "";
            CommonFunctions.FillList(listBox1, TabsList);
        }
    }
}
