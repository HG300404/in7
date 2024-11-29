using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IN7.Module.BusinessObjects.ChungTu
{
    [DefaultClassOptions]
    [System.ComponentModel.DisplayName("LS Trả góp")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    public class InstallmentBill(Session session) : BaseObject(session)
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (Session.IsNewObject(this))
            {
                CreatedAt = DateTime.Now;
            }

        }

        private Bills _Bill;
        [XafDisplayName("ID Đặt Hàng")]
        [Association]
        public Bills Bill
        {
            get { return _Bill; }
            set {
                bool modified = SetPropertyValue(nameof(Bill), ref _Bill, value); 
                if (!IsLoading && !IsSaving && modified) 
                { 
                    UpdateAmount(); 
                }
            }
        }

        private void UpdateAmount()
        {
            // Logic tính toán số lượng Amount dựa trên các bill hiện có
            if (Bill != null)
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("[Bill.Oid] = ?", Bill.Oid);
                int count = Session.GetObjects(Session.GetClassInfo<InstallmentBill>(), criteria, null, 0, false, false).Count;
                Amount = count + 1;
                Cost = Bill.MoneyMonth;
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