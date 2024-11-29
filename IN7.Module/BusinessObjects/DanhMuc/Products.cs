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
    [System.ComponentModel.DisplayName("Sản Phẩm")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Name")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [NavigationItem("Danh mục")]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Products(Session session) : BaseObject(session)
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }



        private Categories _Catergory;
        [Association]
        public Categories Catergory
        {
            get { return _Catergory; }
            set { SetPropertyValue<Categories>(nameof(Catergory), ref _Catergory, value); }
        }




        [Association]
        public XPCollection<BillDetails> BillDetails
        {
            get { return GetCollection<BillDetails>(nameof(BillDetails)); }
        }



        [Association]
        public XPCollection<ImportProductDetails> ImportProductDetails
        {
            get { return GetCollection<ImportProductDetails>(nameof(ImportProductDetails)); }
        }




        private string _Name;
        [XafDisplayName("Tên sản phẩm"), Size(100)]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>(nameof(Name), ref _Name, value); }
        }



        private string _Unit;
        [XafDisplayName("Đơn vị"), Size(20)]
        public string Unit
        {
            get { return _Unit; }
            set { SetPropertyValue<string>(nameof(Unit), ref _Unit, value); }
        }



        private decimal _Price;
        [XafDisplayName("Giá")]
        [ModelDefault("DisplayFormat", "### ### ###")]
        [ModelDefault("EditMask", "### ### ###")]
        public decimal Price
        {
            get { return _Price; }
            set { SetPropertyValue<decimal>(nameof(Price), ref _Price, value); }
        }


        private string _Description;
        [XafDisplayName("Mô tả"), Size(20)]
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue<string>(nameof(Description), ref _Description, value); }
        }


        private string _Size;
        [XafDisplayName("Kích thước"), Size(50)]
        public string Size
        {
            get { return _Size; }
            set { SetPropertyValue<string>(nameof(Size), ref _Size, value); }
        }


        private int _Quantity;
        [XafDisplayName("Số lượng")]
        public int Quantity
        {
            get { return _Quantity; }
            set { SetPropertyValue<int>(nameof(Quantity), ref _Quantity, value); }
        }


        private string _ImageURL;
        [XafDisplayName("Ảnh"), Size(255)]
        public string ImageURL
        {
            get { return _ImageURL; }
            set { SetPropertyValue<string>(nameof(ImageURL), ref _ImageURL, value); }
        }


        private string _Warranty_period;
        [XafDisplayName("Thời Hạn Bảo Hành"), Size(255)]
        public string Warranty_period
        {
            get { return _Warranty_period; }
            set { SetPropertyValue<string>(nameof(Warranty_period), ref _Warranty_period, value); }
        }






    }

}