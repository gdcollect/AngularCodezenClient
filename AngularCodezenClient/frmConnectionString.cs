using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AngularCodezenClient
{
    public partial class frmConnectionString : Form
    {
        public string ConnectionString = "";
        public frmConnectionString()
        {
            InitializeComponent();
            textBox1.Text = CommonFunctions.GetFromReg(Constants.CONNECTIONSTRING);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectionString = textBox1.Text;
            CommonFunctions.SaveToRig(Constants.CONNECTIONSTRING, ConnectionString);
            try
            {
                ConnectionString = textBox1.Text;
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                this.Close();
            }
            catch (Exception ex)
            {
                ConnectionString = "";
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
