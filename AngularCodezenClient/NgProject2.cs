using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularCodezenClient
{
    public class NgColumn2
    {
        public NgColumn2()
        { }
        public string Name { set; get; }
        public string DataType { set; get; }
        public string Length { set; get; }
        public bool IsNullable { set; get; }
        public bool IsPrimary { set; get; }
        public bool IsAuto { set; get; }
    }
    public class NgTable2
    {
        public NgTable2()
        { }
        public string Name { set; get; }
        public int Id { set; get; }

        public List<NgColumn2> Columns = new List<NgColumn2>();
    }

    public class NgNetProject
    {
        public string SolutionName { set; get; }
        public string DataLayerName { set; get; }
        public string DataLayerId { set; get; }
        public string ProjectName { set; get; }
        public string ProjectId { set; get; }
        public string DataBaseConnectionString { get; set; }
        public string Id { set; get; }
    }
    public class NgProject2
    {
        public frmCodeZen refF;
        public NgProject2()
        { }
        public string Name { set; get; }
        public string Path { set; get; }

        public NgNetProject NetProject = new NgNetProject();
        
        public List<string> MenuesList = new List<string>();
        public List<NgMenu2> Menues = new List<NgMenu2>();
        public List<NGCombo> CombosList = new List<NGCombo>();
        public List<NgRadio> RadiosList = new List<NgRadio>();
        public List<string> ComboServiceList = new List<string>();
        public List<string> RadioButtonServiceList = new List<string>();
    }
    public class NgFileProperties2
    {
        public NgFileProperties2()
        { }
        public string Name { get; set; }
    }
    public class NgFileProp2
    {
        public NgFileProp2()
        { }
        public string Name { get; set; }
        public List<NgField2> Fields = new List<NgField2>();
    }
    public class NgMenu2
    {
        public NgMenu2()
        {
        }
        public NgMenu2(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }

        public int Index { set; get; }

        public List<NgSubMenu2> Submenues = new List<NgSubMenu2>();
    }
    public class NgField2
    {
        public frmCodeZen refF;
        public NgSubMenu2 Parent = new NgSubMenu2();
        public NgField2()
        {
        }
        public NgField2(string displayName)
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
        [Description("Length")]
        [Category("General")]
        [DisplayName("Length")]
        public string Length { set; get; }

        [ReadOnly(true)]
        [Description("Original Data Type")]
        [Category("General")]
        [DisplayName("Data Type")]
        public string Type { get; set; }

        [ReadOnly(true)]
        [Description("Net Data Type")]
        [Category("General")]
        [DisplayName("Net Data Type")]
        public string NetType { get; set; }

        [ReadOnly(false)]
        [Description("IsPrimaryKey")]
        [Category("General")]
        [DisplayName("Is Primary Key")]
        public bool IsPrimaryKey { get; set; }

        [ReadOnly(true)]
        [Description("IsIdentity")]
        [Category("General")]
        [DisplayName("Is Identity Key")]
        public bool IsIdentity { get; set; }

        [ReadOnly(false)]
        [Description("Control to be displayed in Add/Update form")]
        [Category("Control")]
        [DisplayName("Control Type")]
        public ControlType ControlType { get; set; }

        [ReadOnly(false)]
        [Description("In the view form if this field is required set it to true. For better look and feel use only 6 fields to display in the grid.")]
        [Category("View Form")]
        [DisplayName("Display in Grid")]
        public bool ToDisplayInGrid { get; set; }

        [ReadOnly(false)]
        [Description("Is it required to be displayed while adding and editing.")]
        [Category("Add/Update")]
        [DisplayName("Display while Add/Edit")]
        public bool ToDisplayInControls { get; set; }

        [ReadOnly(false)]
        [Description("The error message needs to be displayed. This is aplicable when the 'Is Required' is set to true.")]
        [Category("Validation")]
        [DisplayName("Error Message")]
        public string ErrorMessage { get; set; }

        [ReadOnly(false)]
        [Description("While Adding and Editing is this field required.")]
        [Category("Validation")]
        [DisplayName("Is Required")]
        public bool IsValidationRequired { get; set; }

        [ReadOnly(true)]
        [Description("Index")]
        [Category("General")]
        [DisplayName("Index")]
        public int Index { set; get; }
        public NGCombo ComboxBox = new NGCombo();
        public NgRadio RadioButton = new NgRadio();

        [ReadOnly(false)]
        [Description("In the view form to filter the records if this field is required the set it to true.")]
        [Category("Search ")]
        [DisplayName("Include in search ")]
        public bool IncludeInSearch { set; get; }


        [ReadOnly(false)]
        [Description("Select the desired search condition type.")]
        [Category("Search ")]
        [DisplayName("Search Condition Type")]
        public SearchCondition SearchCondition { set; get; }

        private string tabName;

        [ReadOnly(false)]
        [Description("Name of the Tab in which it needs to be placed. To Manage the List of tabs for this form use From Menu bar -->Manage-->Tabs")]
        [Category("General")]
        [DisplayName("Tab Name")]
        [TypeConverter(typeof(TabConverter))]
        public string TabName
        {
            get
            {
                return tabName;
            }
            set
            {
                tabName = value;
            }
        }

        private string comboBoxSource;

        [ReadOnly(false)]
        [Description("Select this when the control type is ComboBox and to manage the comboxsources from menubar -->Manage--> ComboBox Looup service.")]
        [Category("Control")]
        [DisplayName("ComboBox Source")]
        [TypeConverter(typeof(ComboBoxConverter))]
        public string ComboBoxSource
        {
            get
            {
                return comboBoxSource;
            }
            set
            {
                comboBoxSource = value;
            }
        }

        

        private string radioBoxSource;

        [ReadOnly(false)]
        [Description("Select this when the control type is RadioButtons and to manage the Radio Button Source from menubar -->Manage--> Radio Buttobn Looup service.")]
        [Category("Control")]
        [DisplayName("Radio Button Source")]
        [TypeConverter(typeof(RadioButtonConverter))]
        public string RadioBoxSource
        {
            get
            {
                return radioBoxSource;
            }
            set
            {
                radioBoxSource = value;
            }
        }
        [ReadOnly(true)]
        [Description("Tabs Required")]
        [Category("General")]
        [DisplayName("Tabs Required")]
        public bool TabsRequired { set; get; }
    }
   
    public class NgSearchField2
    {
        public NgSearchField2()
        { }
        public string DisplayName { set; get; }
        public string ControlType { set; get; }

        public NGCombo ComboBox = new NGCombo();
        public int Index { set; get; }
    }
    public class Tab2
    {
        public Tab2()
        { }
        public int Index { set; get; }
        public string Name { set; get; }

        public List<NgField2> Fields = new List<NgField2>();
    }
    public class ComboBoxConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            NgField2 refMyObject = context.Instance as NgField2;
            return new StandardValuesCollection(refMyObject.refF.ComboService);
        }
    }
    public class RadioButtonConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            NgField2 refMyObject = context.Instance as NgField2;
            return new StandardValuesCollection(refMyObject.refF.RadioButtonService);
        }
    }
    public class CheckBoxConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            NgField2 refMyObject = context.Instance as NgField2;
            return new StandardValuesCollection(refMyObject.refF.CheckBoxService);
        }
    }
    public class TabConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            NgField2 refMyObject = context.Instance as NgField2;
            return new StandardValuesCollection(refMyObject.refF.Tabs);
        }
    }
    public class MenuConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            NgSubMenu2 refMyObject = context.Instance as NgSubMenu2;
            return new StandardValuesCollection(refMyObject.refF.Menues);
        }
    }
    public class NgSubMenu2
    {
        public List<string> TabNameList = new List<string>();
        public NgSubMenu2()
        {
        }

        public NgMenu2 Parent = new NgMenu2();
        public frmCodeZen refF;
        public NgSubMenu2(string name)
        {
            this.Name = name;
        }
        public NgFileProp2 Module = new NgFileProp2();

        [ReadOnly(true)]
        [Description("Index to display.")]
        [Category("General")]
        [DisplayName("Index")]
        public int Index { set; get; }

        [ReadOnly(true)]
        [Description("Database table name")]
        [Category("General")]
        [DisplayName("Table Name")]
        public string Name { get; set; }
        private string menues;
        [ReadOnly(false)]
        [Description("Menu Name under this form should be displayed. For managing the list of menus use From Menu bar -->Manage-->Menues.")]
        [Category("General")]
        [DisplayName("Menu Name")]
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

        [ReadOnly(false)]
        [Description("Name of the Model Name. Change this only when required.")]
        [Category("Net Form")]
        [DisplayName("Net ClassName")]
        public string NetClassName { get; set; }

        [ReadOnly(false)]
        [Description("Display Text for this column in Add/Update Form")]
        [Category("Add/Update Form")]
        [DisplayName("Display Text")]
        public string DisplayName { get; set; }
        public NgFileProp2 Model = new NgFileProp2();
        public List<NgSearchField2> SearchFields = new List<NgSearchField2>();
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

        [ReadOnly(true)]
        [Description("Create this form for Viewing and Add/Edit operations.")]
        [Category("General")]
        [DisplayName("Create this form")]
        public bool CreateThisForm { set; get; }

        [ReadOnly(true)]
        [Description("Are there tabs in the add/update form.")]
        [Category("Add/Update Form")]
        [DisplayName("Tabs Exists")]
        public bool IsTabbed { set; get; }

        [ReadOnly(false)]
        [Description("Number Of Rows In Paging in View Form.")]
        [Category("View Form")]
        [DisplayName("Number Of Rows In Paging")]
        public int NumberOfRowsInPaging { set; get; }

        [ReadOnly(false)]
        [Description("Allow Delete in View Form.Make it flase if is not required to delete any records form the DB.")]
        [Category("View Form")]
        [DisplayName("Allow Delete")]
        public bool AllowDelete { set; get; }

        

        [ReadOnly(false)]
        [Description("Allow Multi Delete in View Form.Make it flase if is not required to delete many records at a time.")]
        [Category("View Form")]
        [DisplayName("Allow Multi Delete")]
        public bool AllowMultiDelete { set; get; }

        [ReadOnly(false)]
        [Description("Allow Add New Feature in View Form.Make it flase if is not required new redords to be added.")]
        [Category("View Form")]
        [DisplayName("Allow Add New Feature")]
        public bool AllowAddNewFeature { set; get; }

        [ReadOnly(false)]
        [Description("Allow Edit Feature in View Form.Make it flase if is not required to edit the records.")]
        [Category("View Form")]
        [DisplayName("Allow Edit Feature")]
        public bool AllowEditFeature { set; get; }
        public List<string> Tabs = new List<string>();
    }
}
