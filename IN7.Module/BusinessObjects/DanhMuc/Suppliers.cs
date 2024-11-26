using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
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
    [System.ComponentModel.DisplayName("Nhà Cung Cấp")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Name")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [NavigationItem("Danh mục")]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Suppliers(Session session) : BaseObject(session)
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }



        [Association]
        public XPCollection<ImportProducts> ImportProducts
        {
            get { return GetCollection<ImportProducts>(nameof(ImportProducts)); }
        }



        private string _Name;
        [XafDisplayName("Tên Nhà Cung Cấp"), Size(100)]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>(nameof(Name), ref _Name, value); }
        }



        private string _ContactName;
        [XafDisplayName("Tên Người Liên Hệ"), Size(100)]
        public string ContactName
        {
            get { return _ContactName; }
            set { SetPropertyValue<string>(nameof(ContactName), ref _ContactName, value); }
        }


        private string _ContactEmail;
        [XafDisplayName("Mail Nhà Cung Cấp"), Size(100)]
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { SetPropertyValue<string>(nameof(ContactEmail), ref _ContactEmail, value); }
        }


        private string _Phone;
        [XafDisplayName("Số Điện Thoại"), Size(20)]
        public string Phone
        {
            get { return _Phone; }
            set { SetPropertyValue<string>(nameof(Phone), ref _Phone, value); }
        }



        private string _Address;
        [XafDisplayName("Địa Chỉ"), Size(255)]
        public string Address
        {
            get { return _Address; }
            set { SetPropertyValue<string>(nameof(Address), ref _Address, value); }
        }








    }

}