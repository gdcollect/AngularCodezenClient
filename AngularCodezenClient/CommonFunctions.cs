using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static System.Resources.ResXFileRef;

namespace AngularCodezenClient
{
    public static class CommonFunctions
    {
        public static NgSubMenu2 AttachSubMenu(NgTable t)
        {
            var sIndex = 0;
            var fIndex = 0;
            var tabIndex = 0;
            var tabStr = "";

            fIndex = 0;
            NgSubMenu2 s = new NgSubMenu2(t.Name);
            var cCount = t.Columns.Count;
            if (cCount > 10)
            {
                s.IsTabbed = true;
            }
            s.CreateThisForm = true;
            s.NumberOfRowsInPaging = 20;
            s.AllowAddNewFeature = true;
            s.AllowEditFeature = true;
            s.AllowMultiDelete = true;
            s.AllowDelete = true;
            s.DeleteFormMessage = "Are you sure you want to delete this row?";
            s.DeleteFormTitle = "Delete";
            s.DisplayName = s.Name;
            s.Menues = "Menu";
            s.NetClassName = s.Name;
            foreach (var c in t.Columns)
            {
                if (s.IsTabbed == true)
                {
                    if (fIndex % 10 == 0)
                    {
                        tabIndex++;
                        tabStr = "Tab " + tabIndex.ToString();
                        s.Tabs.Add(tabStr);
                    }
                }
                NgField2 f = new NgField2();
                if (s.IsTabbed == true)
                {
                    f.TabName = tabStr;
                }
                if (!CommonFunctions.IsDuplicateFileds(s, c.Name))
                {
                    fIndex++;
                    f.Index = fIndex;
                    f.DisplayName = c.Name;
                    f.OriginalName = c.Name;
                    f.Type = c.DataType;
                    f.Length = c.Length;
                    f.IsIdentity = c.IsAuto;
                    f.IsPrimaryKey = c.IsPrimary;
                    f.NetType = CommonFunctions.GetNetDataType(f.Type);
                    if (f.IsIdentity)
                    {
                        f.ToDisplayInControls = false;
                    }

                    if (f.NetType == "DateTime")
                    {
                        f.ControlType = ControlType.DatePicker;
                    }
                    else
                    {
                        f.ControlType = ControlType.TextBox;
                    }
                    if (c.IsNullable == false)
                    {
                        f.IsValidationRequired = true;
                        f.ErrorMessage = "Please enter the " + f.DisplayName;
                    }
                    if (c.IsAuto == true)
                    {
                        f.ToDisplayInControls = false;
                    }
                    else
                    {
                        f.ToDisplayInControls = true;
                    }
                    if (fIndex > 8)
                    {
                        f.ToDisplayInGrid = false;
                    }
                    else
                    {
                        f.ToDisplayInGrid = true;
                    }
                    s.Model.Fields.Add(f);
                }
            }
            sIndex++;
            s.Index = sIndex;

            return s;
        }
        public static bool Download(string url, string downloadFileName, string downloadFilePath)
        {
            string downloadfile = System.IO.Path.Combine(downloadFilePath, downloadFileName);
            string httpPathWebResource = null;
            Boolean ifFileDownoadedchk = false;
            ifFileDownoadedchk = false;
            WebClient myWebClient = new WebClient();
            httpPathWebResource = url + downloadFileName;
            myWebClient.DownloadFile(httpPathWebResource, downloadfile);

            ifFileDownoadedchk = true;

            return ifFileDownoadedchk;
        }

        public static NgSubMenu2 GetSubMenu(NgProject2 p, string name)
        {
            var xx = new NgSubMenu2();
            try
            {
                foreach (var mn in p.Menues)
                {
                    xx = mn.Submenues.Single(x => x.Name == name);
                    return xx;
                }
            }
            catch
            {
                return null;
            }

            return xx;
        }

        public static NgField2 GetField(NgSubMenu2 ss, string name)
        {
            NgField2 xx = new NgField2();
            try
            {
                xx = ss.Model.Fields.Single(x => x.OriginalName == name);
                return xx;
            }
            catch
            {
                return null;
            }
        }


        public static void CopySubMenu(NgSubMenu2 s1, NgSubMenu2 s2)
        {
            s2.AllowAddNewFeature = s1.AllowAddNewFeature;
            s2.AllowDelete = s1.AllowDelete;
            s2.AllowEditFeature = s1.AllowEditFeature;
            s2.AllowMultiDelete = s1.AllowMultiDelete;
            s2.CreateThisForm = s1.CreateThisForm;
            s2.DeleteFormMessage = s1.DeleteFormMessage;
            s2.DeleteFormTitle = s1.DeleteFormTitle;
            s2.DisplayName = s1.DisplayName;
            s2.Index = s1.Index;
            s2.IsTabbed = s1.IsTabbed;
            s2.Name = s1.Name;
            s2.NumberOfRowsInPaging = s1.NumberOfRowsInPaging;
            s2.TabNameList = s1.TabNameList;
            s2.Tabs = s1.Tabs;
            s2.Menues = s1.Menues;
            s2.Model.Name = s1.Model.Name;
            s2.Module.Name = s1.Module.Name;
        }

        public static void CopyField(NgField2 f1, NgField2 dest)
        {
            dest.ComboBoxSource = f1.ComboBoxSource;
            dest.ComboxBox = f1.ComboxBox;
            dest.ControlType = f1.ControlType;
            dest.DisplayName = f1.DisplayName;
            dest.ErrorMessage = f1.ErrorMessage;
            dest.IncludeInSearch = f1.IncludeInSearch;
            dest.SearchCondition = f1.SearchCondition;
            dest.Index = f1.Index;
            dest.IsPrimaryKey = f1.IsPrimaryKey;
            dest.IsValidationRequired = f1.IsValidationRequired;
            dest.Length = f1.Length;
            dest.NetType = f1.NetType;
            dest.OriginalName = f1.OriginalName;
            dest.RadioBoxSource = f1.RadioBoxSource;
            dest.RadioButton = f1.RadioButton;
            dest.refF = f1.refF;
            dest.TabName = f1.TabName;
            dest.TabsRequired = f1.TabsRequired;
            dest.ToDisplayInControls = f1.ToDisplayInControls;
            dest.ToDisplayInGrid = f1.ToDisplayInGrid;
            dest.Type = f1.Type;
        }


        public static string RandomString(int length = 32)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GetNetDataType(string dtType)
        {
            var retValue = "";
            dtType = dtType.Trim().ToLower();

            if (dtType == "bigint")
            {
                retValue = "int";
            }
            else if (dtType == "bit")
            {
                retValue = "bool";
            }
            else if (dtType == "char")
            {
                retValue = "string";
            }
            else if (dtType == "date")
            {
                retValue = "DateTime";
            }
            else if (dtType == "datetime")
            {
                retValue = "DateTime";
            }
            else if (dtType == "datetime2")
            {
                retValue = "DateTime";
            }
            else if (dtType == "decimal")
            {
                retValue = "int";
            }
            else if (dtType == "int")
            {
                retValue = "int";
            }
            else if (dtType == "nchar")
            {
                retValue = "string";
            }
            else if (dtType == "numeric")
            {
                retValue = "int";
            }
            else if (dtType == "nvarchar")
            {
                retValue = "string";
            }
            else if (dtType == "smallint")
            {
                retValue = "int";
            }
            else if (dtType == "sysname")
            {
                retValue = "string";
            }
            else if (dtType == "time")
            {
                retValue = "DateTime";
            }
            else if (dtType == "varchar")
            {
                retValue = "string";
            }
            else
            {
                retValue = "string";
            }
            return retValue;
        }
        public static string GetFromReg(string name)
        {
            try
            {
                Microsoft.Win32.RegistryKey exampleRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("AgCodeGen");
                var val = exampleRegistryKey.GetValue(name);
                return val.ToString();
            }
            catch
            {
                return "";
            }

        }
        public static void SaveToRig(string name, string value)
        {
            Microsoft.Win32.RegistryKey exampleRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("AgCodeGen");
            exampleRegistryKey.SetValue(name, value);
            exampleRegistryKey.Close();
        }
        public static ControlType GetControlType(string controlType)
        {
            if (controlType == "text")
            {
                return ControlType.TextBox;
            }
            return ControlType.TextBox;
        }
        public static List<string> ChangeValueIn(List<string> TabsList, string changeFrom, string toChange)
        {
            for (int i = 0; i < TabsList.Count; i++)
            {
                if (TabsList[i] == changeFrom)
                {
                    TabsList[i] = toChange;
                    break;
                }
            }
            return TabsList;
        }
        public static void FillList(ListBox l1, List<string> Values)
        {
            l1.Items.Clear();
            foreach (var s in Values)
            {
                if (s != null)
                {
                    l1.Items.Add(s);
                }
            }
        }
        private static bool IsFound(List<string> Values, string s1)
        {
            foreach (var s in Values)
            {
                if (s == s1)
                {
                    return true;
                }
            }
            return false;
        }
        public static string GetNewName(string Name, List<string> Values)
        {
            for (int i = 1; i <= 100; i++)
            {
                var s1 = Name + " " + i.ToString();
                if (IsFound(Values, s1) == false)
                {
                    return s1;
                }
            }
            return Name + " 101";
        }
        public static NgSubMenu2 GetForm(string strMenu, string strForm, NgProject2 pr)
        {
            NgSubMenu2 res = new NgSubMenu2("");
            foreach (var mn in pr.Menues)
            {
                if (mn.Name == strMenu)
                {
                    foreach (var s in mn.Submenues)
                    {
                        if (s.Name == strForm)
                        {
                            res = s;
                            return res;
                        }
                    }
                }
            }
            return res;
        }

        public static NgField2 GetField(string strMenu, string strForm, string strField, NgProject2 pr)
        {
            NgField2 res = new NgField2("");
            foreach (var mn in pr.Menues)
            {
                if (mn.Name == strMenu)
                {
                    foreach (var s in mn.Submenues)
                    {
                        if (s.Name == strForm)
                        {
                            foreach (var f in s.Model.Fields)
                            {
                                if (f.DisplayName == strField)
                                {
                                    res = f;
                                    return res;
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }

        public static NgMenu2 GetMenu(string strMenu, NgProject2 pr)
        {
            NgMenu2 res = new NgMenu2("");
            foreach (var mn in pr.Menues)
            {
                if (mn.Name == strMenu)
                {
                    res = mn;
                    return res;
                }
            }
            return res;
        }
        public static bool IsDuplicateFileds(NgSubMenu2 frm, string fieldName)
        {
            foreach (var f in frm.Model.Fields)
            {
                if (f.DisplayName == fieldName)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsValidToAdd(List<string> values, string toAdd)
        {
            foreach (var s in values)
            {
                if (s == toAdd)
                {
                    return false;
                }
            }
            return true;
        }
        public static void SelectList(ListBox li, string value)
        {
            try
            {
                var i = 0;
                foreach (var item in li.Items)
                {
                    if (item.ToString() == value)
                    {
                        li.SetSelected(i, true);
                        return;
                    }
                    i++;
                }
            }
            catch
            { }

        }
        public static List<string> ProcessMenues(NgProject2 pr)
        {
            List<string> Menues = new List<string>();
            foreach (var s in pr.Menues)
            {
                Menues.Add(s.Name);
            }
            pr.MenuesList = Menues;
            return Menues;
        }

        public static List<string> ProcessTabs(NgSubMenu pr)
        {
            List<string> tabs = new List<string>();
            foreach (var s in pr.Model.Fields)
            {
                if (IsValidToAdd(tabs, s.TabName))
                {
                    tabs.Add(s.TabName);
                }
            }
            return tabs;
        }
        public static List<string> ProcessTabs(NgSubMenu2 pr)
        {
            List<string> tabs = new List<string>();
            foreach (var s in pr.Model.Fields)
            {
                if (IsValidToAdd(tabs, s.TabName))
                {
                    tabs.Add(s.TabName);
                }
            }
            return tabs;
        }
        public static NgProject2 ToNgProgect2(NgProject ng1)
        {
            NgProject2 ng = new NgProject2();
            if (ng1.Name == null || ng1.Name.Trim().Length == 0)
            {
                ng.Name = "Tables";
            }
            else
            {
                ng.Name = ng1.Name;
            }
            ng.Name = ng1.Name;
            ng.Path = ng1.Path;
            ng.MenuesList.Clear();
            ng.NetProject.DataBaseConnectionString = ng1.NetProject.DataBaseConnectionString;


            foreach (var ss in ng1.RadiosList)
            {
                NgRadio ch = new NgRadio();
                ch.Name = ss.Name;
                foreach (var op in ss.Options)
                {
                    NGOPT op1 = new NGOPT();
                    op1.Key = op.Key;
                    op1.Value = op.Value;
                    ch.Options.Add(op1);
                }
                ng.RadiosList.Add(ch);
            }

            foreach (var ss in ng1.CombosList)
            {
                NGCombo cb = new NGCombo();
                cb.ServiceName = ss.ServiceName;
                cb.IsFixed = ss.IsFixed;
                cb.Name = ss.Name;
                cb.Key = ss.Key;
                cb.Value = ss.Value;
                foreach (var op in ss.Options)
                {
                    NGOPT op1 = new NGOPT();
                    op1.Key = op.Key;
                    op1.Value = op.Value;
                    cb.Options.Add(op1);
                }
                ng.CombosList.Add(cb);
            }

            foreach (string ss in ng1.MenuesList)
            {
                ng.MenuesList.Add(ss);
            }
            foreach (var mn1 in ng1.Menues)
            {
                NgMenu2 mn = new NgMenu2();
                mn.Index = mn1.Index;
                mn.Name = mn1.Name;
                foreach (var s1 in mn1.Submenues)
                {
                    NgSubMenu2 s = new NgSubMenu2();
                    foreach (var ts in s1.TabNameList)
                    {
                        s.TabNameList.Add(ts);
                    }
                    s.NetClassName = s1.NetClassName;
                    s.CreateThisForm = s1.CreateThisForm;
                    s.AllowAddNewFeature = s1.AllowAddNewFeature;
                    s.AllowEditFeature = s1.AllowEditFeature;
                    s.AllowMultiDelete = s1.AllowMultiDelete;
                    s.AllowDelete = s1.AllowDelete;
                    s.DeleteFormMessage = s1.DeleteFormMessage;
                    s.DeleteFormTitle = s1.DeleteFormTitle;
                    s.DisplayName = s1.DisplayName;
                    s.Index = s1.Index;
                    s.IsTabbed = s1.IsTabbed;
                    s.Menues = s1.Menues;

                    s.Name = s1.Name;
                    s.NumberOfRowsInPaging = s1.NumberOfRowsInPaging;
                    s.Tabs = ProcessTabs(s1);
                    NgFileProp2 m1 = new NgFileProp2();
                    m1.Name = s1.Model.Name;

                    foreach (var f1 in s1.Model.Fields)
                    {
                        NgField2 f = new NgField2();
                        f.IsIdentity = f1.IsIdentity;
                        f.DisplayName = f1.DisplayName;
                        f.ErrorMessage = f1.ErrorMessage;
                        f.IncludeInSearch = f1.IncludeInSearch;
                        f.SearchCondition = f1.SearchCondition;
                        f.Index = f1.Index;
                        f.IsValidationRequired = f1.IsValidationRequired;
                        f.OriginalName = f1.OriginalName;
                        f.TabName = f1.TabName;
                        f.TabsRequired = f1.TabsRequired;
                        f.ToDisplayInControls = f1.ToDisplayInControls;
                        f.ToDisplayInGrid = f1.ToDisplayInGrid;
                        f.Type = f1.Type;
                        f.Length = f1.Length;
                        f.NetType = f1.NetType;
                        f.ComboBoxSource = f1.ComboBoxSource;
                        f.RadioBoxSource = f1.RadioBoxSource;
                        f.ControlType = f1.ControlType;
                        f.IsPrimaryKey = f1.IsPrimaryKey;
                        m1.Fields.Add(f);
                    }
                    s.Model = m1;

                    mn.Submenues.Add(s);
                }
                ng.Menues.Add(mn);
            }
            return ng;
        }

        public static NgProject ToNgProgect(NgProject2 ng1)
        {
            NgProject ng = new NgProject();

            ng.Name = ng1.Name;
            ng.Path = ng1.Path;
            ng.MenuesList.Clear();
            ng.NetProject.DataBaseConnectionString = ng1.NetProject.DataBaseConnectionString;

            foreach (var ss in ng1.RadiosList)
            {
                NgRadio ch = new NgRadio();
                ch.Name = ss.Name;
                foreach (var op in ss.Options)
                {
                    NGOPT op1 = new NGOPT();
                    op1.Key = op.Key;
                    op1.Value = op.Value;
                    ch.Options.Add(op1);
                }
                ng.RadiosList.Add(ch);
            }

            foreach (var ss in ng1.CombosList)
            {
                NGCombo cb = new NGCombo();
                cb.ServiceName = ss.ServiceName;
                cb.IsFixed = ss.IsFixed;
                cb.Name = ss.Name;
                cb.Key = ss.Key;
                cb.Value = ss.Value;
                foreach (var op in ss.Options)
                {
                    NGOPT op1 = new NGOPT();
                    op1.Key = op.Key;
                    op1.Value = op.Value;
                    cb.Options.Add(op1);
                }
                ng.CombosList.Add(cb);
            }

            foreach (string ss in ng1.MenuesList)
            {
                ng.MenuesList.Add(ss);
            }
            foreach (var mn1 in ng1.Menues)
            {
                NgMenu mn = new NgMenu();
                mn.Index = mn1.Index;
                mn.Name = mn1.Name;
                foreach (var s1 in mn1.Submenues)
                {
                    NgSubMenu s = new NgSubMenu();
                    foreach (var ts in s1.TabNameList)
                    {
                        s.TabNameList.Add(ts);
                    }
                    s.NetClassName = s1.NetClassName;
                    s.CreateThisForm = s1.CreateThisForm;
                    s.AllowAddNewFeature = s1.AllowAddNewFeature;
                    s.AllowEditFeature = s1.AllowEditFeature;
                    s.AllowMultiDelete = s1.AllowMultiDelete;
                    s.AllowDelete = s1.AllowDelete;
                    s.DeleteFormMessage = s1.DeleteFormMessage;
                    s.DeleteFormTitle = s1.DeleteFormTitle;
                    s.DisplayName = s1.DisplayName;
                    s.Index = s1.Index;
                    s.IsTabbed = s1.IsTabbed;
                    s.Name = s1.Name;
                    s.NumberOfRowsInPaging = s1.NumberOfRowsInPaging;
                    s.Tabs = s1.Tabs;
                    s.Menues = s1.Menues;

                    NgFileProp m1 = new NgFileProp();
                    m1.Name = s1.Model.Name;
                    foreach (var f1 in s1.Model.Fields)
                    {
                        NgField f = new NgField();
                        f.IsIdentity = f1.IsIdentity;
                        f.DisplayName = f1.DisplayName;
                        f.ErrorMessage = f1.ErrorMessage;
                        f.IncludeInSearch = f1.IncludeInSearch;
                        f.SearchCondition = f1.SearchCondition;
                        f.Index = f1.Index;
                        f.Length = f1.Length;
                        f.IsValidationRequired = f1.IsValidationRequired;
                        f.OriginalName = f1.OriginalName;
                        f.TabName = f1.TabName;
                        f.TabsRequired = f1.TabsRequired;
                        f.ToDisplayInControls = f1.ToDisplayInControls;
                        f.ToDisplayInGrid = f1.ToDisplayInGrid;
                        f.Type = f1.Type;
                        f.NetType = f1.NetType;
                        f.ComboBoxSource = f1.ComboBoxSource;
                        f.RadioBoxSource = f1.RadioBoxSource;
                        f.IsPrimaryKey = f1.IsPrimaryKey;
                        //f.CheckBoxSource = f1.CheckBoxSource;
                        foreach (var ss in ng1.CombosList)
                        {
                            if (ss.Name == f1.ComboBoxSource)
                            {
                                f.ComboxBox = ss;
                            }
                        }
                        foreach (var ss in ng1.RadiosList)
                        {
                            if (ss.Name == f1.RadioBoxSource)
                            {
                                f.RadioButton = ss;
                            }
                        }
                        f.ControlType = f1.ControlType;
                        m1.Fields.Add(f);
                    }
                    s.Model = m1;

                    mn.Submenues.Add(s);
                }
                ng.Menues.Add(mn);
            }
            return ng;
        }

        public static NgProject FromXml(string strFileName)
        {
            NgProject result = new NgProject();
            try
            {
                var xml = CommonFunctions.ReadFromFile(strFileName);
                XmlSerializer serializer = new XmlSerializer(typeof(NgProject));

                using (TextReader reader = new StringReader(xml))
                {
                    result = (NgProject)serializer.Deserialize(reader);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public static void SaveXml(string strFileName, NgProject pr)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(NgProject));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, pr);
                    xml = sww.ToString(); // Your XML
                }
            }

            CommonFunctions.WriteToFile(strFileName, xml);
        }
        public static void SaveXml(string strFileName, NgProject2 pr)
        {
            var pr2 = CommonFunctions.ToNgProgect(pr);
            XmlSerializer xsSubmit = new XmlSerializer(typeof(NgProject));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, pr2);
                    xml = sww.ToString(); // Your XML
                }
            }

            CommonFunctions.WriteToFile(strFileName, xml);
        }

        public static string ReadFromFile(string fileName)
        {
            return System.IO.File.ReadAllText(fileName);
        }
        public static void WriteToFile(string fileName, string str)
        {
            File.WriteAllText(fileName, str);
        }
    }
    public class MyObject
    {

        public frmCodeZen refF;
        public MyObject(frmCodeZen f)
        {
            this.refF = f;
        }


        private string menues;
        [TypeConverter(typeof(MenuConverter))]
        public string Menues
        {
            get
            {
                return menues;
            }
            set
            {
                menues = value;
            }
        }
    }

    public class MenuConverter1 : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            MyObject refMyObject = context.Instance as MyObject;
            return new StandardValuesCollection(refMyObject.refF.Menues);
        }
    }


}
