using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Spreadsheet;
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
    [System.ComponentModel.DisplayName("LS Trả góp")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    public class Installment_Import(Session session) : BaseObject(session)
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (Session.IsNewObject(this))
            {
                CreatedAt = DateTime.Now;
            }
        }


        private ImportProducts _ImportProduct;
        [XafDisplayName("ID Nhập Hàng")]
        [Association]
        public ImportProducts ImportProduct
        {
            get { return _ImportProduct; }
            set
            {
                bool modified = SetPropertyValue<ImportProducts>(nameof(ImportProduct), ref _ImportProduct, value);
                if (!IsLoading && !IsSaving && modified)
                {
                    UpdateAmount();
                }
            }
        }

        private void UpdateAmount()
        {
            // Logic tính toán số lượng Amount dựa trên các ImportProduct hiện có
            if (ImportProduct != null)
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("[ImportProduct.Oid] = ?", ImportProduct.Oid);
                int count = Session.GetObjects(Session.GetClassInfo<Installment_Import>(), criteria, null, 0, false, false).Count;
                Amount = count + 1;
                Cost = ImportProduct.MoneyMonth;
            }
        }

        private decimal _Cost;
        [XafDisplayName("Giá Tiền")]
        [ModelDefault("DisplayFormat", "{0:#,##0.00 ₫}")]
        [ModelDefault("EditMask", "n2")]
        public decimal Cost
        {
            get { return _Cost; }
            set { SetPropertyValue<decimal>(nameof(Cost), ref _Cost, value); }
        }


        private int _Amount;
        [XafDisplayName("Số Lần")]
        public int Amount
        {
            get { return _Amount; }
            set { SetPropertyValue<int>(nameof(Amount), ref _Amount, value); }
        }


        private DateTime _CreatedAt;
        [XafDisplayName("Ngày Trả")]
        public DateTime CreatedAt
        {
            get { return _CreatedAt; }
            set { SetPropertyValue<DateTime>(nameof(CreatedAt), ref _CreatedAt, value); }
        }




    }
}