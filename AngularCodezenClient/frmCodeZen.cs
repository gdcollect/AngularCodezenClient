using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static System.Resources.ResXFileRef;

namespace AngularCodezenClient
{
    public partial class frmCodeZen : Form
    {
        ThreadStart childref = new ThreadStart(GenerateProject);

        public string ErrorMessages = "";
        delegate void SetTextCallback(string text);
        int errorCnt = 0;
        static string outPutStr = "";
        string strFileName = "";
        List<NgTable> Tables = new List<NgTable>();
        static NgProject2 pr = new NgProject2();
        string selectedMenuName = "";
        string selectedFormName = "";
        string selectedFieldName = "";

        NgMenu2 selectedMenu = new NgMenu2();
        NgSubMenu2 selectedForm = new NgSubMenu2();
        NgField2 selectedField = new NgField2();
        int level = 0;

        public List<string> Menues = new List<string>();
        public List<string> Tabs = new List<string>();
        public List<string> ComboService = new List<string>();
        public List<string> CheckBoxService = new List<string>();
        public List<string> RadioButtonService = new List<string>();

        public frmCodeZen()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menues.Add("Menu");
            PushOutPut("");
            frmConnectionString frm = new frmConnectionString();
            frm.ShowDialog();
            var cstr = frm.ConnectionString;
            if (cstr != "")
            {
                Tables = DB.FetchDBSchema(cstr);
                pr = new NgProject2();
                NgMenu2 mn = new NgMenu2("Menu");
                pr = SetTreeView1(mn, Tables);
                pr.NetProject.DataBaseConnectionString = cstr;
                SetTreeView();
            }
            Menues = CommonFunctions.ProcessMenues(pr);

            foreach (var s in pr.Menues)
            {
                foreach (var ss in s.Submenues)
                {
                    ss.TabNameList = CommonFunctions.ProcessTabs(ss);

                }
            }
        }

        static bool Upload(string url, string filePath, string localFilename, string uploadFileName)
        {
            Boolean isFileUploaded = false;

            try
            {
                HttpClient httpClient = new HttpClient();

                var fileStream = File.Open(localFilename, FileMode.Open);
                var fileInfo = new FileInfo(localFilename);
                UploadFIle uploadResult = null;
                bool _fileUploaded = false;

                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Headers.Add("filePath", filePath);
                content.Add(new StreamContent(fileStream), "\"file\"", string.Format("\"{0}\"", uploadFileName + fileInfo.Extension)
                        );

                Task taskUpload = httpClient.PostAsync(url, content).ContinueWith(task =>
                {
                    if (task.Status == TaskStatus.RanToCompletion)
                    {
                        var response = task.Result;

                        if (response.IsSuccessStatusCode)
                        {
                            uploadResult = response.Content.ReadAsAsync<UploadFIle>().Result;
                            if (uploadResult != null)
                                _fileUploaded = true;

                        }

                    }

                    fileStream.Dispose();
                });

                taskUpload.Wait();
                if (_fileUploaded)
                    isFileUploaded = true;

                httpClient.Dispose();

            }
            catch
            {
                isFileUploaded = false;
            }


            return isFileUploaded;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "Project1";
            openFileDialog1.Filter = "XML Files|*.xml";
            var xx = openFileDialog1.ShowDialog();
            if (xx == DialogResult.Cancel)
            {
                return;
            }

            strFileName = openFileDialog1.FileName;
            NgProject pr2 = null;


            try
            {
                pr2 = CommonFunctions.FromXml(strFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (pr2 == null || pr2.Menues.Count == 0)
            {
                return;
            }
            else
            {
                pr = CommonFunctions.ToNgProgect2(pr2);
                SetTreeView();
            }
            SetServices();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pr == null)
            {
                MessageBox.Show("No project exists. Try by creating New Project or Open.");
                return;
            }
            if (pr.Menues.Count() == 0)
            {
                MessageBox.Show("No project exists. Try by creating New Project or Open.");
                return;
            }

            if (strFileName == "")
            {
                saveFileDialog1.FileName = "Project1";
                saveFileDialog1.Filter = "XML Files|*.xml";
                saveFileDialog1.ShowDialog();
                strFileName = saveFileDialog1.FileName;
            }
            if (strFileName == "")
            {
                return;
            }
            CommonFunctions.SaveXml(strFileName, pr);
            MessageBox.Show("Done");
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pr == null)
            {
                MessageBox.Show("No project exists. Try by creating New Project or Open.");
                return;
            }
            if (pr.Menues.Count() == 0)
            {
                MessageBox.Show("No project exists. Try by creating New Project or Open.");
                return;
            }

            saveFileDialog1.FileName = "Project1";
            saveFileDialog1.Filter = "XML Files|*.xml";
            saveFileDialog1.ShowDialog();
            strFileName = saveFileDialog1.FileName;
            if (strFileName == "")
            {
                return;
            }
            CommonFunctions.SaveXml(strFileName, pr);
            MessageBox.Show("Done");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task PostDataLocal(NgProject pr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.SITEURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("AgCodeGen", pr).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<string>();
                    MessageBox.Show(result);
                    PushOutPut(result);
                }
                else
                {
                    MessageBox.Show(response.ToString());
                }
            }
        }

        private static async Task PostData(string pr)
        {
            var fr = new FilePr();
            fr.Name = pr;
            fr.ChangedName = pr;

            ////using (var client = new HttpClient())
            ////{
            ////    client.BaseAddress = new Uri(Constants.SITEURL);
            ////    var content = new FormUrlEncodedContent(new[]
            ////    {
            ////        new KeyValuePair<string, string>("", "pr")
            ////    });
            ////    var result = await client.PostAsync("/CodeGen", content);
            ////    string resultContent = await result.Content.ReadAsStringAsync();
            ////    Console.WriteLine(resultContent);
            ////}

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.SITEURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("CodeGen", fr).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {

                    PushOutPut("Success.");
                    var result = await response.Content.ReadAsAsync<string>();
                    PushOutPut("Downloading File.");
                    if (DownlaodFile(result))
                    {
                        PushOutPut("Successfully downloaded the to : " + CommonFunctions.GetFromReg(Constants.DOWNLOAD_FOLDER_PATH) + "\\" + result);
                        MessageBox.Show("Completed. Downloaded the file to : " + CommonFunctions.GetFromReg(Constants.DOWNLOAD_FOLDER_PATH) + "\\" + result);

                    }
                    //PushOutPut(result);
                }
                else
                {
                    PushOutPut(response.ToString());
                }
            }
        }

        public static bool DownlaodFile(string downloadFileName)
        {
            string url = Constants.DOWNLOADSITEURL;
            string downloadPath = "";
            var ss = CommonFunctions.GetFromReg(Constants.DOWNLOAD_FOLDER_PATH);
            if (ss != null && ss.Trim().Length > 0)
            {
                downloadPath = ss;
            }
            else
            {
                downloadPath = Application.StartupPath + @"\Downloads\";
            }


            if (!Directory.Exists(downloadPath))
                Directory.CreateDirectory(downloadPath);

            Boolean isFileDownloaded = CommonFunctions.Download(url, downloadFileName, downloadPath);
            if (isFileDownloaded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private NgProject2 SetTreeView1(NgMenu2 mn, List<NgTable> tables)
        {
            mn.Index = 1;
            NgProject2 pr1 = new NgProject2();

            foreach (var t in tables)
            {
                var s = CommonFunctions.AttachSubMenu(t);
                mn.Submenues.Add(s);
            }
            pr1.Menues.Add(mn);

            return pr1;
        }

        private void SetTreeView()
        {
            try
            {
                treeView1.Nodes.Clear();
                foreach (var mn in pr.Menues.OrderBy(x => x.Index))
                {
                    TreeNode tn = new TreeNode(mn.Name);

                    foreach (var s in mn.Submenues.OrderBy(x => x.Index))
                    {
                        TreeNode stn = new TreeNode(s.Name);
                        foreach (var f in s.Model.Fields.OrderBy(x => x.Index))
                        {
                            stn.Nodes.Add(f.DisplayName);
                        }
                        tn.Nodes.Add(stn);
                    }
                    treeView1.Nodes.Add(tn);
                }

                treeView1.Nodes[0].Expand();
            }
            catch
            { }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            level = e.Node.Level;
            if (level == 0)
            {
                selectedMenuName = e.Node.Text;
                var mn = CommonFunctions.GetMenu(selectedMenuName, pr);
                selectedMenu = mn;
            }
            if (level == 1)
            {
                selectedFormName = e.Node.Text;
                selectedMenuName = e.Node.Parent.Text;
                Menues = pr.MenuesList;
                var fr = CommonFunctions.GetForm(selectedMenuName, selectedFormName, pr);
                selectedForm = fr;
                fr.refF = this;

                this.propertyGrid1.SelectedObject = fr;
            }
            if (level == 2)
            {
                selectedFormName = e.Node.Parent.Text;
                selectedMenuName = e.Node.Parent.Parent.Text;
                selectedFieldName = e.Node.Text;
                var fl = CommonFunctions.GetField(selectedMenuName, selectedFormName, selectedFieldName, pr);
                selectedField = fl;
                fl.refF = this;
                Tabs = selectedForm.TabNameList;
                ComboService = pr.ComboServiceList;
                RadioButtonService = pr.RadioButtonServiceList;
                propertyGrid1.SelectedObject = fl;
            }
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBox1.InvokeRequired)
            {
                //SetTextCallback d = new SetTextCallback(SetText);
                //this.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBox1.Text = text;
            }
        }

        private static void PushOutPut(string str, bool clean = false)
        {
            try
            {
                if (clean == false)
                {
                    outPutStr = outPutStr + str + Environment.NewLine;
                    //this.richTextBox1.Text=outPutStr;
                }
                else
                {
                    outPutStr = str + Environment.NewLine;
                    //this.richTextBox1.Text = "";
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void SetServices()
        {
            pr.RadioButtonServiceList = pr.RadiosList.Select(x => x.Name).ToList();
            pr.ComboServiceList = pr.CombosList.Select(x => x.Name).ToList();
        }

        private void comboBoxLookuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCombos fr = new frmCombos();
            fr.CombosList = pr.CombosList;
            fr.pr = pr;
            fr.ShowDialog();
            pr.CombosList = fr.CombosList;
            SetServices();
        }

        private void radioButtonsLookUpServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCheckBox fr = new frmCheckBox();
            fr.RadioBoxes = pr.RadiosList;
            fr.ShowDialog();
            pr.RadiosList = fr.RadioBoxes;
            SetServices();
            ComboService = pr.ComboServiceList;
        }

        private void menuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMenues fr = new frmMenues();
            fr.pr = pr;
            fr.Menues = Menues;
            fr.ShowDialog();
            pr = fr.pr;
            Menues = fr.Menues;
        }

        private void tabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTabs fr = new frmTabs();
            fr.TabsList = selectedForm.TabNameList;
            fr.SubMenu = selectedForm;
            fr.ShowDialog();
            selectedForm = fr.SubMenu;
            selectedForm.TabNameList = fr.TabsList;
        }

        private static void GenerateProject()
        {
            PushOutPut("Uploading the project");
            DateTime dtn = DateTime.Now;
            var pr3 = CommonFunctions.ToNgProgect(pr);
            CommonFunctions.SaveXml("temp.xml", pr3);

            Boolean uploadStatus = false;
            string url = Constants.SITEURL + "FileHandling";
            string filePath = @"\";
            Random rnd = new Random();
            string uploadFileName = CommonFunctions.RandomString();

            uploadStatus = Upload(url, filePath, "temp.xml", uploadFileName);
            if (uploadStatus)
            {
                try
                {
                    PushOutPut("Upload success");
                    PushOutPut("Generating the code");
                    PostData(uploadFileName + ".xml").Wait();
                }
                catch (Exception ex)
                {
                    PushOutPut(ex.Message);
                }
            }
            else
            {
                PushOutPut("Upload failed. Please try again.");
                MessageBox.Show("Upload failed. Please try again.");
            }
        }
        private void generateCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pr == null)
            {
                MessageBox.Show("No project exists. Try by creating New Project or Open.");
                return;
            }
            if (pr.Menues.Count() == 0)
            {
                MessageBox.Show("No project exists. Try by creating New Project or Open.");
                return;
            }

            ValidateProject();
            if (errorCnt > 0)
            {
                PushOutPut(string.Format("Errors : {0}. Please fix them", errorCnt));
                MessageBox.Show("Errors : " + errorCnt + " Please check the output window for the list of errors.");
            }
            else
            {

                Thread childThread = new Thread(childref);
                childThread.Start();
            }

            //-------------------
            ////PushOutPut("Output : ", true);
            ////ValidateProject();
            ////if (errorCnt > 0)
            ////{
            ////    PushOutPut(string.Format("Errors : {0}. Please fix them", errorCnt));
            ////    MessageBox.Show("Errors : " + errorCnt + " Please check the output window for the list of errors.");
            ////}
            ////else
            ////{
            ////    try
            ////    {

            ////        var pr1 = CommonFunctions.ToNgProgect(pr);
            ////        pr1.Name = "AgCodeGen";
            ////        PostDataLocal(pr1).Wait();
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        MessageBox.Show(ex.Message);
            ////    }
            ////}

        }



        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var pr3 = new NgProject2();
            var constr = CommonFunctions.GetFromReg(Constants.CONNECTIONSTRING);
            if (constr != "")
            {
                var NewTables = DB.FetchDBSchema(constr);
                var pr2 = new NgProject2();
                NgMenu2 mn = new NgMenu2("Tables");
                mn.Name = "Tables";
                pr2 = SetTreeView1(mn, NewTables);
                pr3.RadiosList = pr.RadiosList;
                pr3.CombosList = pr.CombosList;
                pr3.NetProject.DataBaseConnectionString = pr.NetProject.DataBaseConnectionString;

                foreach (var xx in pr2.Menues)
                {
                    var mn1 = new NgMenu2();
                    mn1.Name = Constants.MENUNAME;
                    mn1.Index = xx.Index;

                    foreach (var yy in xx.Submenues)
                    {
                        var sub1 = new NgSubMenu2();
                        var sub = CommonFunctions.GetSubMenu(pr, yy.Name);
                        if (sub != null)
                        {
                            CommonFunctions.CopySubMenu(sub, sub1);
                            var cf = 0;
                            foreach (var zz in yy.Model.Fields)
                            {
                                cf++;
                                var zz1 = CommonFunctions.GetField(sub, zz.OriginalName);
                                if (zz1 != null)
                                {
                                    zz1.Length = zz.Length;
                                    sub1.Model.Fields.Add(zz1);
                                }
                                else
                                {
                                    sub1.Model.Fields.Add(zz);
                                }
                            }
                            mn1.Submenues.Add(sub1);
                        }
                        else
                        {
                            CommonFunctions.CopySubMenu(yy, sub1);
                            foreach (var zz in yy.Model.Fields)
                            {
                                var zz1 = new NgField2();
                                CommonFunctions.CopyField(zz, zz1);
                                zz1.Length = zz.Length;
                                sub1.Model.Fields.Add(zz1);
                            }
                            mn1.Submenues.Add(sub1);
                        }
                    }
                    pr3.Menues.Add(mn1);
                }
            }

            pr = pr3;
            SetTreeView();
            MessageBox.Show("Done");
        }
        private void ValidateForPrimaryKeys()
        {
            PushOutPut("Validating for primery keys..");
            foreach (var mn in pr.Menues)
            {
                foreach (var su in mn.Submenues)
                {
                    var primaryKeyExists = false;
                    foreach (var fl in su.Model.Fields)
                    {
                        if (fl.IsPrimaryKey == true)
                        {
                            primaryKeyExists = true;
                        }
                    }
                    if (primaryKeyExists == false)
                    {
                        errorCnt++;
                        PushOutPut("Primary key doesnt exists for the table : " + su.Name);
                    }
                }
            }
        }

        private void ValidateForComboBoxes()
        {
            PushOutPut("Validating for Comboxes..");
            foreach (var m in pr.Menues)
            {
                foreach (var s in m.Submenues)
                {
                    foreach (var f in s.Model.Fields)
                    {
                        if (f.ControlType == ControlType.ComboBox)
                        {
                            if (f.ComboBoxSource == "")
                            {
                                errorCnt++;
                                PushOutPut(string.Format("Combo box {0} Under the form {1} doesnt have Combo source", f.DisplayName, s.DisplayName));
                            }

                        }

                    }
                }
            }
        }

        private void ValidateForRadioBoxes()
        {
            PushOutPut("Validating for Radiobuttons..");
            foreach (var m in pr.Menues)
            {
                foreach (var s in m.Submenues)
                {
                    foreach (var f in s.Model.Fields)
                    {
                        if (f.ControlType == ControlType.RadioButtons)
                        {
                            if (f.RadioBoxSource == "")
                            {
                                errorCnt++;
                                PushOutPut(string.Format("Radio button {0} Under the form {1} doesnt have Radio source", f.DisplayName, s.DisplayName));
                            }
                        }
                    }
                }
            }
        }

        private void ValidateForRequiredFields()
        {
            PushOutPut("Validating for Radiobuttons..");
            foreach (var m in pr.Menues)
            {
                foreach (var s in m.Submenues)
                {
                    foreach (var f in s.Model.Fields)
                    {
                        if (f.IsValidationRequired && f.ErrorMessage == "")
                        {
                            errorCnt++;
                            PushOutPut(string.Format("Field {0} Under the form {1} doesnt have Message for validating", f.DisplayName, s.DisplayName));
                        }
                    }
                }
            }
        }

        private void ValidateForTabsFields()
        {
            PushOutPut("Validating for Tabs..");
            foreach (var m in pr.Menues)
            {
                foreach (var s in m.Submenues)
                {
                    foreach (var f in s.Model.Fields)
                    {
                        if (f.IsValidationRequired && f.ErrorMessage == "")
                        {
                            errorCnt++;
                            PushOutPut(string.Format("Field {0} Under the form {1} doesnt have Message for validating", f.DisplayName, s.DisplayName));
                        }
                    }
                }
            }
        }

        private void ValidateProject()
        {
            PushOutPut("", true);
            errorCnt = 0;
            ValidateForPrimaryKeys();
            ValidateForComboBoxes();
            ValidateForRadioBoxes();
            ValidateForRequiredFields();
            ValidateForMenues();
            ValidateForTabs();

        }

        private void ValidateForTabs()
        {
            PushOutPut("Validating for Tabs..");
            foreach (var m in pr.Menues)
            {
                foreach (var s in m.Submenues)
                {
                    foreach (var f in s.Model.Fields)
                    {
                        if (s.IsTabbed)
                        {
                            if (f.TabName == "")
                            {
                                errorCnt++;
                                PushOutPut(string.Format("Select the Tab Name for  {0} Under the form {1}", f.DisplayName, s.DisplayName));
                            }

                        }
                    }
                }
            }
        }

        private void ValidateForMenues()
        {
            PushOutPut("Validating for Menues..");
            foreach (var m in pr.Menues)
            {
                foreach (var s in m.Submenues)
                {
                    if (s.Menues.Trim() == "")
                    {
                        PushOutPut(string.Format("Select the Menu Name for the form {1}", s.DisplayName));
                    }

                }
            }
        }

        private void validateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pr == null)
            {
                MessageBox.Show("No project exists. Try by creating New Project or Open.");
                return;
            }
            if (pr.Menues.Count() == 0)
            {
                MessageBox.Show("No project exists. Try by creating New Project or Open.");
                return;
            }
            ValidateProject();
            if (errorCnt == 0)
            {
                MessageBox.Show("Validation succeded.");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ErrorMessages.Length != outPutStr.Length)
            {
                ErrorMessages = outPutStr;
                richTextBox1.Text = outPutStr;
            }

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmDownloadFilePath frm = new frmDownloadFilePath();
            frm.ShowDialog();
        }

        private void frmCodeZen_Load(object sender, EventArgs e)
        {
            var ss = CommonFunctions.GetFromReg(Constants.DOWNLOAD_FOLDER_PATH);
            if (!(ss != null && ss.Trim().Length > 0))
            {
                var downloadPath = Application.StartupPath + @"\Downloads\";
                CommonFunctions.SaveToRig(Constants.DOWNLOAD_FOLDER_PATH, downloadPath);
            }
        }
    }


}
