using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Spreadsheet;
using DevExpress.Xpo;
using IN7.Module.BusinessObjects.ChungTu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IN7.Module.BusinessObjects.DanhMuc
{
    [DefaultClassOptions]
    [System.ComponentModel.DisplayName("Nhân Viên")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Name")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [NavigationItem("Danh mục")]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Employees(Session session) : BaseObject(session)
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [Association]
        public XPCollection<Bills> Bills
        {
            get { return GetCollection<Bills>(nameof(Bills)); }
        }



        [Association]
        public XPCollection<ImportProducts> ImportProducts
        {
            get { return GetCollection<ImportProducts>(nameof(ImportProducts)); }
        }




        private string _Name;
        [XafDisplayName("Tên nhân viên"), Size(100)]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>(nameof(Name), ref _Name, value); }
        }


        private string _Gender;
        [XafDisplayName("Gender"), Size(100)]
        public string Gender
        {
            get { return _Gender; }
            set { SetPropertyValue<string>(nameof(Gender), ref _Gender, value); }
        }


        private string _Role;
        [XafDisplayName("Vai trò"), Size(40)]
        public string Role
        {
            get { return _Role; }
            set { SetPropertyValue<string>(nameof(Role), ref _Role, value); }
        }



        private string _Phone;
        [XafDisplayName("Số điện thoại"), Size(20)]
        public string Phone
        {
            get { return _Phone; }
            set { SetPropertyValue<string>(nameof(Phone), ref _Phone, value); }
        }


        private string _Address;
        [XafDisplayName("Địa chỉ"), Size(255)]
        public string Address
        {
            get { return _Address; }
            set { SetPropertyValue<string>(nameof(Address), ref _Address, value); }
        }



        private int _Salary;
        [XafDisplayName("Lương"), Size(12)]
        public int Salary
        {
            get { return _Salary; }
            set { SetPropertyValue<int>(nameof(Salary), ref _Salary, value); }
        }
    }
}