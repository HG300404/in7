using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using IN7.Module.BusinessObjects.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IN7.Module.BusinessObjects.ChungTu
{
    [DefaultClassOptions]
    [System.ComponentModel.DisplayName("Nhập Hàng")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [NavigationItem("Chứng Từ")]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ImportProducts(Session session) : BaseObject(session)
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }



        private Employees _Employee;
        [Association]
        public Employees Employee
        {
            get { return _Employee; }
            set { SetPropertyValue<Employees>(nameof(Employee), ref _Employee, value); }
        }




        private Suppliers _Supplier;
        [Association]
        public Suppliers Supplier
        {
            get { return _Supplier; }
            set { SetPropertyValue<Suppliers>(nameof(Supplier), ref _Supplier, value); }
        }




        [DevExpress.Xpo.Aggregated, Association]
        public XPCollection<ImportProductDetails> ImportProductDetails
        {
            get { return GetCollection<ImportProductDetails>(nameof(ImportProductDetails)); }
        }



        private string _ContactName;
        [XafDisplayName("Tên Nguoi LHe"), Size(100)]
        public string ContactName
        {
            get { return _ContactName; }
            set { SetPropertyValue<string>(nameof(ContactName), ref _ContactName, value); }
        }


        private string _ContactEmail;
        [XafDisplayName("Mail Nguoi LHe"), Size(100)]
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { SetPropertyValue<string>(nameof(ContactEmail), ref _ContactEmail, value); }
        }


        private string _Phone;
        [XafDisplayName("SĐT"), Size(20)]
        public string Phone
        {
            get { return _Phone; }
            set { SetPropertyValue<string>(nameof(Phone), ref _Phone, value); }
        }


        private decimal _Total;
        [XafDisplayName("Tong")]
        [ModelDefault("DisplayFormat", "{0:### ### ###}")]
        [ModelDefault("EditMask", "{0:### ### ###}")]
        public decimal Total
        {
            get { return _Total; }
            set { SetPropertyValue<decimal>(nameof(Total), ref _Total, value); }
        }

        private int _Type;
        [XafDisplayName("Loại hình trả")]
        public int Type
        {
            get { return _Type; }
            set { SetPropertyValue<int>(nameof(Type), ref _Type, value); }
        }

        private int _Tax;
        [XafDisplayName("Thuế")]
        public int Tax
        {
            get { return _Tax; }
            set { SetPropertyValue<int>(nameof(Tax), ref _Tax, value); }
        }

        private decimal _ShippingPrice;
        [XafDisplayName("Tiền Ship ")]
        [ModelDefault("DisplayFormat", "{0:# ### ###}")]
        [ModelDefault("EditMask", "{0:# ### ###}")]
        public decimal ShippingPrice
        {
            get { return _ShippingPrice; }
            set { SetPropertyValue<decimal>(nameof(ShippingPrice), ref _ShippingPrice, value); }
        }

        private string _PaymentMethod;
        [XafDisplayName("Phương thức TT"), Size(20)]
        public string PaymentMethod
        {
            get { return _PaymentMethod; }
            set { SetPropertyValue<string>(nameof(PaymentMethod), ref _PaymentMethod, value); }
        }

        private decimal _Interest;
        [XafDisplayName("Lãi")]
        [ModelDefault("DisplayFormat", "{0:C2}")]
        [ModelDefault("EditMask", "{0:C2}")]

        public decimal Interest
        {
            get { return _Interest; }
            set { SetPropertyValue<decimal>(nameof(Interest), ref _Interest, value); }
        }


        private string _Time;
        [XafDisplayName("Thời hạn trả")]
        public string Time
        {
            get { return _Time; }
            set { SetPropertyValue<string>(nameof(Time), ref _Time, value); }
        }
    }
}