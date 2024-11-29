using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
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
    [System.ComponentModel.DisplayName("Chi Tiết Bán Hàng")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [NavigationItem(false)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class BillDetails(Session session) : BaseObject(session)
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }



        private Bills _Bill;
        [Association]
        public Bills Bill
        {
            get { return _Bill; }
            set { SetPropertyValue<Bills>(nameof(Bill), ref _Bill, value); }
        }


        private Products _Product;
        [Association]
        public Products Product
        {
            get { return _Product; }
            set
            {
                if (SetPropertyValue<Products>(nameof(Product), ref _Product, value)
                    && !IsLoading && !IsDeleted && value != null)
                {
                    Quantity = 1;
                    UnitPrice = value.Price;
                    Unit = value.Unit;
                }

            }
        }




        private int _Quantity;
        [XafDisplayName("Số lượng")]
        public int Quantity
        {
            get { return _Quantity; }
set { SetPropertyValue<int>(nameof(Quantity), ref _Quantity, value); }
        }


        private string _Unit;
        [XafDisplayName("Đơn vị"), Size(20)]
        public string Unit
        {
            get { return _Unit; }
            set { SetPropertyValue<string>(nameof(Unit), ref _Unit, value); }
        }


        private decimal _UnitPrice;
        [XafDisplayName("Đơn giá")]
        [ModelDefault("DisplayFormat", "{0:#,##0.00 ₫}")]
        [ModelDefault("EditMask", "n2")]
        public decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { SetPropertyValue<decimal>(nameof(UnitPrice), ref _UnitPrice, value); }
        }



        //[XafDisplayName("Tổng Giá")]
        //[ModelDefault("DisplayFormat", "{0:### ### ###}")]
        //[ModelDefault("EditMask", "{0:### ### ###}")]
        public decimal Price
        {
            get
            {
                return (decimal)Quantity * UnitPrice;
            }
        }
    }
}