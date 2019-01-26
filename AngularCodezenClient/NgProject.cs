using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularCodezenClient
{
    public class FilePr
    {
        public string Name { get; set; }
        public string ChangedName { get; set; }
    }
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
        public NgNetProject NetProject = new NgNetProject();
        public List<string> MenuesList = new List<string>();
        public List<NgMenu> Menues = new List<NgMenu>();
        public List<NGCombo> CombosList = new List<NGCombo>();
        public List<NgCheckBox> CheckBoxesList = new List<NgCheckBox>();
        public List<NgRadio> RadiosList = new List<NgRadio>();

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
        public bool IsIdentity { get; set; }
        public string Length { set; get; }
        public NgSubMenu Parent = new NgSubMenu();
        public NgField()
        {
        }
        public NgField(string displayName)
        {
            this.DisplayName = displayName;
        }
      
        public string DisplayName { get; set; }

      
        public string OriginalName { get; set; }

        
        public string Type { get; set; }

        public string NetType { get; set; }

       
        public ControlType ControlType { get; set; }

      
        public bool ToDisplayInGrid { get; set; }

      
        public bool ToDisplayInControls { get; set; }

        public string ErrorMessage { get; set; }

      
        public bool IsValidationRequired { get; set; }

        
        public int Index { set; get; }
        public NGCombo ComboxBox = new NGCombo();
        public NgRadio RadioButton = new NgRadio();
        public bool IncludeInSearch { set; get; }

        public SearchCondition SearchCondition { set; get; }

        public string TabName { set; get; }

      
        public bool TabsRequired { set; get; }
        public bool IsPrimaryKey { set; get; }
        public string ComboBoxSource { set; get; }
        public string RadioBoxSource { set; get; }
    }
    public class NGOPT
    {
        public NGOPT()
        { }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public enum SearchCondition
    {
        EqualTo,
        NotEqualTo,
        GraterThan,
        LessThen,
        Like
    }

    public enum ControlType
    {
        TextBox,
        ComboBox,
        Checkbox,
        MultilineTextBox,
        DatePicker,
        RadioButtons
    }
    public class NgRadio
    {
        public string Name { set; get; }
        public List<NGOPT> Options = new List<NGOPT>();
        public NgRadio()
        { }

        public List<string> Values = new List<string>();
    }
    public class NgCheckBox
    {
        public string Name { set; get; }
       
        public List<NGOPT> Options = new List<NGOPT>();
        public List<string> Values = new List<string>();

        public NgCheckBox()
        { }

    }
    public class NGCombo
    {
        public string Name { set; get; }
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
        public List<string> TabNameList = new List<string>();
        public NgSubMenu()
        {
        }


        public NgMenu Parent = new NgMenu();
        public string NetClassName { get; set; }

        public NgSubMenu(string name)
        {
            this.Name = name;
        }

        public NgFileProp Module = new NgFileProp();
        
        
        public int Index { set; get; }

      
        public string Name { get; set; }

        private string menues;
       
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

        public string DisplayName { get; set; }
        public NgFileProp Model = new NgFileProp();

        public List<NgSearchField> SearchFields = new List<NgSearchField>();

        public string DeleteFormTitle { set; get; }

        public string DeleteFormMessage { set; get; }

        public bool CreateThisForm { set; get; }
        public bool IsTabbed { set; get; }

        public int NumberOfRowsInPaging { set; get; }
        
        public bool AllowDelete { set; get; }
        public bool AllowMultiDelete { set; get; }
        public bool AllowAddNewFeature { set; get; }

        public bool AllowEditFeature { set; get; }

        public List<string> Tabs = new List<string>();
    }
}
