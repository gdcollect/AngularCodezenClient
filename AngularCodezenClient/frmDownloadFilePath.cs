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
    public partial class frmDownloadFilePath : Form
    {
        public frmDownloadFilePath()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = CommonFunctions.GetFromReg(Constants.DOWNLOAD_FOLDER_PATH);
            var xx = folderBrowserDialog1.ShowDialog();
            if (xx == DialogResult.Cancel)
            {
                return;
            }

            var str1 = folderBrowserDialog1.SelectedPath;
            if (str1 != "")
            {
                textBox1.Text = str1;
                CommonFunctions.SaveToRig(Constants.DOWNLOAD_FOLDER_PATH, str1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDownloadFilePath_Load(object sender, EventArgs e)
        {
            var ss = CommonFunctions.GetFromReg(Constants.DOWNLOAD_FOLDER_PATH);
            if (ss != null && ss.Trim().Length > 0)
            {
                textBox1.Text = ss;
            }
            else
            {
                var downloadPath = Application.StartupPath + @"\Downloads\";
                CommonFunctions.SaveToRig(Constants.DOWNLOAD_FOLDER_PATH, downloadPath);
                textBox1.Text = downloadPath;
            }
                
           
        }
    }
}
