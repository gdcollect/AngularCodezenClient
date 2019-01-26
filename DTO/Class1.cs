using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    
    public class NgColumn
    {
        public NgColumn()
        { }
        public string Name { set; get; }
        public string DataType { set; get; }
        public string Length { set; get; }
        public bool IsNullable { set; get; }
        public bool IsPrimary { set; get; }
        public bool IsAuto { set; get; }
    }
    public class NgTable
    {
        public NgTable()
        { }
        public string Name { set; get; }
        public int Id { set; get; }

        public List<NgColumn> Columns = new List<NgColumn>();
    }
    public class NgProject
    {
        public NgProject()
        { }
        public string Name { set; get; }
        public string Path { set; get; }
        public List<NgMenu> Menues = new List<NgMenu>();

    }
    public class NgFileProperties
    {
        public NgFileProperties()
        { }
        public string Name { get; set; }

    }
    public class NgFileProp
    {
        public NgFileProp()
        { }
        public string Name { get; set; }

        public List<NgField> Fields = new List<NgField>();

    }
    public class NgMenu
    {
        public NgMenu()
        {
        }
        public NgMenu(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }

        public int Index { set; get; }

        public List<NgSubMenu> Submenues = new List<NgSubMenu>();
    }
    public class NgField
    {
        public NgSubMenu Parent = new NgSubMenu();
        public NgField()
        {
        }
        public NgField(string displayName)
        {
            this.DisplayName = displayName;
        }
        [ReadOnly(false)]
        [Description("Text to be displayed in Add/Update Form")]
        [Category("Add/Update")]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [ReadOnly(true)]
        [Description("Original Column Name of the Table.")]
        [Category("General")]
        [DisplayName("Original Name")]
        public string OriginalName { get; set; }

        [ReadOnly(true)]
        [Description("Original Data Type")]
        [Category("General")]
        [DisplayName("Data Type")]
        public string Type { get; set; }

        [ReadOnly(false)]
        [Description("Control to be displayed in Add/Update form")]
        [Category("Add/Update")]
        [DisplayName("Control Type")]
        public ControlType ControlType { get; set; }

        [ReadOnly(false)]
        [Description("Display in Grid")]
        [Category("View Form")]
        [DisplayName("Display in Grid")]
        public bool ToDisplayInGrid { get; set; }

        [ReadOnly(false)]
        [Description("Display while Add / Edit")]
        [Category("Add/Update")]
        [DisplayName("Display while Add/Edit")]
        public bool ToDisplayInControls { get; set; }

        [ReadOnly(false)]
        [Description("Error Message")]
        [Category("Validation")]
        [DisplayName("Error Message")]
        public string ErrorMessage { get; set; }

        [ReadOnly(false)]
        [Description("Is Required")]
        [Category("Validation")]
        [DisplayName("Is Required")]
        public bool IsValidationRequired { get; set; }

        [ReadOnly(true)]
        [Description("Index")]
        [Category("General")]
        [DisplayName("Index")]
        public int Index { set; get; }
        public NGCombo ComboxBox = new NGCombo();

        [ReadOnly(false)]
        [Description("Tab Name")]
        [Category("View Form")]
        [DisplayName("Include in search ")]
        public bool IncludeInSearch { set; get; }

        [ReadOnly(false)]
        [Description("Tab Name")]
        [Category("General")]
        [DisplayName("Tab Name")]
        public string TabName { set; get; }

        [ReadOnly(false)]
        [Description("Tabs Required")]
        [Category("General")]
        [DisplayName("Tabs Required")]
        public bool TabsRequired { set; get; }
    }
    public class NGOPT
    {
        public NGOPT()
        { }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public enum ControlType
    {
        TextBox,
        ComboBox,
        Checkbox,
        List,
        MultilineTextBox,
        DatePicker
    }
    public class NgRadio
    {
        public NgRadio()
        { }

        public List<string> Values = new List<string>();
    }
    public class NgCheckBox
    {
        public NgCheckBox()
        { }

        public List<string> Values = new List<string>();
    }
    public class NGCombo
    {
        public NGCombo()
        { }

        public bool IsFixed { set; get; }
        public List<NGOPT> Options = new List<NGOPT>();

        public string ServiceName { set; get; }
        public string Key { set; get; }
        public string Value { set; get; }
    }
    public class NgSearchField
    {
        public NgSearchField()
        { }
        public string DisplayName { set; get; }
        public string ControlType { set; get; }

        public NGCombo ComboBox = new NGCombo();
        public int Index { set; get; }
    }
    public class Tab
    {
        public Tab()
        { }
        public int Index { set; get; }
        public string Name { set; get; }

        public List<NgField> Fields = new List<NgField>();
    }
   
    public class NgSubMenu
    {


        public NgMenu Parent = new NgMenu();
        public NgSubMenu()
        {
        }
        public NgSubMenu(string name)
        {
            this.Name = name;
        }

        public NgFileProp Module = new NgFileProp();

        [ReadOnly(true)]
        [Description("Index to display.")]
        [Category("General")]
        [DisplayName("Index")]
        public int Index { set; get; }

        [ReadOnly(true)]
        [Description("Field Name from the Table")]
        [Category("General")]
        [DisplayName("Table Name")]
        public string Name { get; set; }

        [ReadOnly(false)]
        [Description("Menu Name under this form should be displayed.")]
        [Category("General")]
        [DisplayName("Menu Name")]
        public string MenuName { get; set; }

        [ReadOnly(false)]
        [Description("Display Text for this column in Add/Update Form")]
        [Category("Add/Update Form")]
        [DisplayName("Display Text")]
        public string DisplayName { get; set; }
        public NgFileProp Model = new NgFileProp();

        public List<NgSearchField> SearchFields = new List<NgSearchField>();

        [ReadOnly(false)]
        [Description("Delete Form Title.")]
        [Category("Delete Form")]
        [DisplayName("Delete Form Title")]
        public string DeleteFormTitle { set; get; }

        [ReadOnly(false)]
        [Description("Delete Form Message.")]
        [Category("Delete Form")]
        [DisplayName("Delete Form Message")]
        public string DeleteFormMessage { set; get; }

        [ReadOnly(false)]
        [Description("Are there tabs in the add/update form.")]
        [Category("Add/Update Form")]
        [DisplayName("Are there tabs")]
        public bool IsTabbed { set; get; }

        [ReadOnly(false)]
        [Description("Number Of Rows In Paging in View Form.")]
        [Category("View Form")]
        [DisplayName("Number Of Rows In Paging")]
        public int NumberOfRowsInPaging { set; get; }

        [ReadOnly(false)]
        [Description("Allow Multi Delete in View Form.")]
        [Category("View Form")]
        [DisplayName("Allow Multi Delete")]
        public bool AllowMultiDelete { set; get; }

        [ReadOnly(false)]
        [Description("Allow Add New Feature in View Form.")]
        [Category("View Form")]
        [DisplayName("Allow Add New Feature")]
        public bool AllowAddNewFeature { set; get; }

        [ReadOnly(false)]
        [Description("Allow Edit Feature in View Form.")]
        [Category("View Form")]
        [DisplayName("Allow Edit Feature")]
        public bool AllowEditFeature { set; get; }

        public List<string> Tabs = new List<string>();
        //public List<Field> Fields = new List<Field>();
    }
}
