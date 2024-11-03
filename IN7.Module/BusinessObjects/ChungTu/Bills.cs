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
    [System.ComponentModel.DisplayName("Bán Hàng")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [NavigationItem("Chứng Từ")]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Bills(Session session) : BaseObject(session)
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            if (Session.IsNewObject(this))
            {
                CreatedAt = DateTime.Now;
            }
        }



        [DevExpress.Xpo.Aggregated, Association]
        public XPCollection<BillDetails> BillDetails
        {
            get { return GetCollection<BillDetails>(nameof(BillDetails)); }
        }




        private Customers _Customer;
        [XafDisplayName("Khách Hàng")]
        [Association]
        public Customers Customer
        {
            get { return _Customer; }
            set { SetPropertyValue(nameof(Customer), ref _Customer, value); }
        }


        private Employees _Employee;
        [XafDisplayName("Nhân Viên")]
        [Association]
        public Employees Employee
        {
            get { return _Employee; }
            set { SetPropertyValue(nameof(Employee), ref _Employee, value); }
        }



        [XafDisplayName("Giá SP")]
        [ModelDefault("DisplayFormat", "{0:### ### ###}")]
        [ModelDefault("EditMask", "{0:### ### ###}")]
        public decimal ProductPrice
        {
            get
            {
                decimal price = 0;
                foreach (BillDetails item in BillDetails)
                {
                    price += item.Price;
                }
                return price;
            }

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


        [XafDisplayName("Tổng Tiền")]
        [ModelDefault("DisplayFormat", "{0:### ### ###}")]
        [ModelDefault("EditMask", "{0:### ### ###}")]
        public decimal TotalAmount
        {
            get
            {
                if (Tax != 0)
                {
                    decimal price = ProductPrice * Tax + ShippingPrice;
                    return price;
                }
                else
                {
                    decimal price = ProductPrice + ShippingPrice;
                    return price;
                }
            }

        }


        private decimal _Deposit;
        [XafDisplayName("Tiền Cọc")]
        [ModelDefault("DisplayFormat", "{0:### ### ###}")]
        [ModelDefault("EditMask", "{0:### ### ###}")]
        public decimal Deposit
        {
            get { return _Deposit; }
            set { SetPropertyValue<decimal>(nameof(Deposit), ref _Deposit, value); }
        }


        private string _PaymentMethod;
        [XafDisplayName("Phương thức TT"), Size(20)]
        public string PaymentMethod
        {
            get { return _PaymentMethod; }
            set { SetPropertyValue<string>(nameof(PaymentMethod), ref _PaymentMethod, value); }
        }



        private int _Tax;
        [XafDisplayName("Thuế")]
        [ModelDefault("DisplayFormat", "{0:C2}")]
        [ModelDefault("EditMask", "{0:C2}")]
        public int Tax
        {
            get { return _Tax; }
            set { SetPropertyValue<int>(nameof(Tax), ref _Tax, value); }
        }


        private int _Type;
        [XafDisplayName("Loại hình trả")]
        public int Type
        {
            get { return _Type; }
            set { SetPropertyValue<int>(nameof(Type), ref _Type, value); }
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



        private DateTime _CreatedAt;
        [XafDisplayName("Ngày Đặt")]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy HH:mm}")]
        [ModelDefault("EditMask", "{0:dd/MM/yyyy HH:mm}")]
        public DateTime CreatedAt
        {
            get { return _CreatedAt; }
            set { SetPropertyValue<DateTime>(nameof(CreatedAt), ref _CreatedAt, value); }
        }



        //private int _NBill;
        //[XafDisplayName("Số HĐ")]
        //[RuleUniqueValue]
        //public int NBill
        //{
        //    get { return _NBill; }
        //    set { SetPropertyValue<int>(nameof(NBill), ref _NBill, value); }
        //}

    }
}