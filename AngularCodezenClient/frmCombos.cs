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
    public partial class frmCombos : Form
    {
        public NgProject2 pr = new NgProject2();
        public List<NGCombo> CombosList = new List<NGCombo>();
        private NGCombo SelectedCombo = new NGCombo();
        private NGOPT SelectedOption = new NGOPT();
        
        public frmCombos()
        {
            InitializeComponent();
        }

        private void FillServices()
        {
            comboBox1.Items.Clear();
            foreach (var mn in pr.Menues)
            {
                foreach (var s in mn.Submenues)
                {
                    comboBox1.Items.Add(s.Name);
                }
            }
        }

        private NgSubMenu2 GetSubMenu(string str)
        {
            NgSubMenu2 s = new NgSubMenu2();
            foreach (var mn in pr.Menues)
            {
                foreach (var su in mn.Submenues)
                {
                    if (su.Name == str)
                    {
                        return su;
                    }
                }
            }
            return s;
        }
        private void FillList()
        {
            listBox1.Items.Clear();
            foreach (var o in CombosList)
            {
                listBox1.Items.Add(o.Name);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NGCombo c = new NGCombo();
            var ss = CombosList.Select(x => x.Name).ToList();
            c.Name = CommonFunctions.GetNewName("NewComboService",ss);
            SelectedCombo = c;
            CombosList.Add(c);
            FillList();
            textBox3.Text = c.Name;
            CommonFunctions.SelectList(listBox1, c.Name);
            
        }

        private void frmCombos_Load(object sender, EventArgs e)
        {
            FillList();
            FillServices();
        }

        private void FillOpt()
        {
            textBox1.Text = SelectedOption.Key;
            textBox2.Text = SelectedOption.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SelectedOption = new NGOPT();
            SelectedOption.Key = "Key";
            SelectedOption.Value = "Value";
            SelectedCombo.Options.Add(SelectedOption);
            var ss = SelectedCombo.Options.Select(x => x.Key).ToList();
            FillOpt();
            CommonFunctions.FillList(listBox2, ss);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                panel2.Visible = true;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            NGOPT opt = new NGOPT();
            var ss = SelectedCombo.Options.Select(x => x.Key).ToList();
            opt.Key = CommonFunctions.GetNewName("Key", ss);
            ss = SelectedCombo.Options.Select(x => x.Value).ToList();
            opt.Value = CommonFunctions.GetNewName("Value", ss);
            SelectedCombo.Options.Add(opt);
            SelectedOption = opt;
            FillTexts();
            FillOpt();
            FillOptList();

            CommonFunctions.SelectList(listBox2, SelectedOption.Key);
        }

        private void FillTexts(bool clean=false)
        {
            if (clean == true)
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                textBox1.Text = SelectedOption.Key;
                textBox2.Text = SelectedOption.Value;
            }
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                return;
            }
            SelectedOption.Key = textBox1.Text;
            SelectedOption.Value = textBox2.Text;
            FillOptList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SelectedCombo.Options.Remove(SelectedOption);
            SelectedOption = null;
            FillTexts(true);
            FillOptList();
            textBox1.Text = "";
            textBox2.Text = "";
            SelectedOption = new NGOPT();
        }

        private void FillOptList()
        {
            var ss = SelectedCombo.Options.Select(x => x.Key).ToList();
            CommonFunctions.FillList(listBox2, ss);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            CombosList.Remove(SelectedCombo);
            SelectedCombo = null;
            textBox3.Text = "";
            FillList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = listBox1.SelectedItem;
            var k1 = listBox1.SelectedIndex;
            if (SelectedCombo != null)
            {
                if (SelectedCombo.Name != textBox3.Text)
                {
                    button2_Click(null, null);
                    listBox1.SetSelected(k1, true);
                }
            }

            //var s = listBox1.SelectedItem;
            if (s == null)
            {
                return;
            }
            foreach (var k in CombosList)
            {
                if (k.Name == s.ToString())
                {
                    SelectedCombo = k;
                    textBox3.Text = k.Name;
                    try
                    {
                        if (SelectedCombo.IsFixed == true)
                        {
                            radioButton1.Checked = true;
                        }
                        else
                        {
                            radioButton2.Checked = true;
                        }
                        comboBox1.SelectedIndex = GetIndex(comboBox1, k.ServiceName);
                        comboBox3.SelectedIndex = GetIndex(comboBox3, k.Key);
                        comboBox2.SelectedIndex = GetIndex(comboBox2, k.Value);
                    }
                    catch
                    { }
                }
            }
            FillOptList();
            SelectedOption = null;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private int GetIndex(ComboBox cmb,string value)
        {
            int i = 0;
            foreach (var it in cmb.Items)
            {
                if (it.ToString() == value)
                {
                    return i;
                }
                i++;
            }
            return 0;
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = listBox2.SelectedItem;
            var k1 = listBox2.SelectedIndex;
            if (SelectedOption != null)
            {
                if (SelectedOption.Key != textBox1.Text || SelectedOption.Value != textBox2.Text)
                {
                    button5_Click(null, null);
                    listBox2.SetSelected(k1, true);
                }
            }
            
            if (s == null)
            {
                return;
            }
            foreach (var k in SelectedCombo.Options)
            {
                if (k.Key == s.ToString())
                {
                    SelectedOption = k;
                    textBox1.Text = k.Key;
                    textBox2.Text = k.Value;
                    
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedCombo.Name = textBox3.Text;
            if (radioButton1.Checked)
                SelectedCombo.IsFixed = true;
            else
                SelectedCombo.IsFixed = false;
            SelectedCombo.ServiceName = comboBox1.Text;
            SelectedCombo.Key = comboBox3.Text;
            SelectedCombo.Value = comboBox2.Text;
            FillList();
        }

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = comboBox1.SelectedItem;
            var sub = GetSubMenu(s.ToString());
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            foreach (var fld in sub.Model.Fields)
            {
                comboBox2.Items.Add(fld.DisplayName);
                comboBox3.Items.Add(fld.DisplayName);
            }
        }
    }
}
