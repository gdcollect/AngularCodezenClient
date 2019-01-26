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
    public partial class frmCheckBox : Form
    {
        public List<NgRadio> RadioBoxes = new List<NgRadio>();
        public NgRadio SelectedCheckBox = new NgRadio();
        public NGOPT SelectedOpt = new NGOPT();

        public frmCheckBox()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillLst()
        {
            var ss = SelectedCheckBox.Options.Select(x => x.Value).ToList();
            CommonFunctions.FillList(listBox2, ss);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //NGOPT2 opt = new NGOPT2();
            //opt.Key = "Key";
            //opt.Value = "Value";
            //SelectedCheckBox.Options.Add(opt);
            //FillLst();
        }

        private void FillCheckBoxList()
        {
            var ss = RadioBoxes.Select(x => x.Name).ToList();
            CommonFunctions.FillList(listBox1, ss);
        }
        private void frmCheckBox_Load(object sender, EventArgs e)
        {
            FillCheckBoxList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NgRadio ch = new NgRadio();
            var ss = RadioBoxes.Select(x => x.Name).ToList();
            ch.Name = CommonFunctions.GetNewName("Radio Button Lookup", ss);
            SelectedCheckBox = ch;
            textBox1.Text = ch.Name;
            RadioBoxes.Add(ch);
            FillCheckBoxList();
            CommonFunctions.SelectList(listBox1, ch.Name);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectedOpt.Value = textBox3.Text;
            SelectedOpt.Key = textBox2.Text;
            FillLst();
        }

        private void FillTexts()
        {
             textBox3.Text= SelectedOpt.Value;
             textBox2.Text= SelectedOpt.Key ;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            NGOPT opt = new NGOPT();
            opt.Key = "Key";
            var s = SelectedCheckBox.Options.Select(x => x.Value).ToList();
            opt.Value = CommonFunctions.GetNewName("Value", s);
            SelectedOpt = opt;
            FillTexts();
            SelectedCheckBox.Options.Add(opt);
            FillLst();
            CommonFunctions.SelectList(listBox2, opt.Value);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = listBox1.SelectedItem;

            var k1 = listBox1.SelectedIndex;
            if (SelectedCheckBox != null)
            {
                if (SelectedCheckBox.Name != textBox1.Text)
                {
                    button4_Click(null, null);
                    listBox1.SetSelected(k1, true);
                }
            }

            if (s == null)
            {
                return;
            }
            foreach (var k in RadioBoxes)
            {
                if (k.Name == s.ToString())
                {
                    SelectedCheckBox = k;
                    textBox1.Text = k.Name;
                    FillLst();
                    return;
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = listBox2.SelectedItem;

            var k1 = listBox2.SelectedIndex;
            if (SelectedOpt != null)
            {
                if (SelectedOpt.Key != textBox2.Text || SelectedOpt.Value != textBox3.Text)
                {
                    button3_Click(null, null);
                    listBox2.SetSelected(k1, true);
                }
            }

            if (s == null)
            {
                return;
            }
            foreach (var k in SelectedCheckBox.Options)
            {
                if (k.Value == s.ToString())
                {
                    SelectedOpt = k;
                    textBox2.Text = k.Key;
                    textBox3.Text = k.Value;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SelectedCheckBox.Name = textBox1.Text;
            FillCheckBoxList();
        }

        

        private void button8_Click(object sender, EventArgs e)
        {
            SelectedCheckBox.Options.Remove(SelectedOpt);
            FillLst();
            textBox2.Text = "";
            textBox3.Text = "";
            SelectedOpt = new NGOPT();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RadioBoxes.Remove(SelectedCheckBox);
            FillCheckBoxList();
            textBox1.Text = "";
            SelectedCheckBox = new NgRadio();
        }
    }
}
