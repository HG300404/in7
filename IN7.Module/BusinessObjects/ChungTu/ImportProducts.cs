using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraRichEdit.Import.Html;
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
    {
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Khởi tạo giá trị mặc định
            TrangThai = TrangThaiNhapHang.DaDatHang;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            if (Session.IsNewObject(this))
            {
                CreatedAt = DateTime.Now;
        }
        }



        private Employees _Employee;
        [XafDisplayName("Nhân Viên")]
        [Association]
        public Employees Employee
        {
            get { return _Employee; }
            set { SetPropertyValue<Employees>(nameof(Employee), ref _Employee, value); }
        }




        private Suppliers _Supplier;
        [XafDisplayName("Nhà Cung Cấp")]
        [Association]
        public Suppliers Supplier
        {
            get { return _Supplier; }
            set {
                if (SetPropertyValue<Suppliers>(nameof(Supplier), ref _Supplier, value)
                    && !IsLoading && !IsDeleted && value != null)
                {
                    ContactName = value.ContactName;
                    ContactEmail = value.ContactEmail;
                    Phone = value.Phone;
        }
            }
        }




        [DevExpress.Xpo.Aggregated, Association]
        public XPCollection<ImportProductDetails> ImportProductDetails
        {
            get { return GetCollection<ImportProductDetails>(nameof(ImportProductDetails)); }
        }

        private string _ContactName;
        [XafDisplayName("Tên Người LHe"), Size(100)]
        public string ContactName
        {
            get { return _ContactName; }
            set { SetPropertyValue<string>(nameof(ContactName), ref _ContactName, value); }
        }

        private string _ContactEmail;
        [XafDisplayName("Mail Người LHe"), Size(100)]
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
        [XafDisplayName("Tổng Tiền")]
        [ModelDefault("DisplayFormat", "{0:### ### ###}")]
        [ModelDefault("EditMask", "{0:### ### ###}")]
        public decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                if (SetPropertyValue(nameof(Total), ref _Total, value)
                    && !IsLoading && !IsSaving && !IsDeleted && ImportProductDetails != null)
                {
                    value = CalculateTotal();
                }
            }
        }

        // Phương thức tính toán giá trị Total
        private decimal CalculateTotal()
        {
            decimal price = 0;
            if (ImportProductDetails != null)
            {
                foreach (ImportProductDetails item in ImportProductDetails)
                {
                    price += item.Price;
                }
            }
            // Tính toán giá trị sau khi thêm thuế
            return price;
        }

        public enum PaymentType
        {
            [XafDisplayName("Trả hết")]
            OneTime,
            [XafDisplayName("Trả góp")]
            Installment
        }

        private PaymentType _Type;
        [XafDisplayName("Loại hình trả")]
        public PaymentType Type
        {
            get { return _Type; }
            set { SetPropertyValue(nameof(Type), ref _Type, value); }
        }


        public enum Method
        {
            [XafDisplayName("Tiền mặt")]
            Cash,
            [XafDisplayName("Chuyển khoản")]
            BankTransfer
        }


        private Method _PaymentMethod;
        [XafDisplayName("Phương thức TT")]
        public Method PaymentMethod
        {
            get { return _PaymentMethod; }
            set { SetPropertyValue(nameof(PaymentMethod), ref _PaymentMethod, value); }
        }


        private decimal _MoneyMonth;
        [XafDisplayName("Tiền tháng")]
        [ModelDefault("DisplayFormat", "{0:### ### ###}")]
        [ModelDefault("EditMask", "{0:### ### ###}")]
        public decimal MoneyMonth
        {
            get { return _MoneyMonth; }
            set { SetPropertyValue<decimal>(nameof(MoneyMonth), ref _MoneyMonth, value); }
        }

        // Thêm Trường Trạng Thái
        private TrangThaiNhapHang _TrangThai;
        [XafDisplayName("Trạng Thái Nhập Hàng")]
        public TrangThaiNhapHang TrangThai
        private decimal _Deposit;
        [XafDisplayName("Tiền Cọc")]
        [ModelDefault("DisplayFormat", "{0:### ### ###}")]
        [ModelDefault("EditMask", "{0:### ### ###}")]
        public decimal Deposit
        {
            get { return _Deposit; }
            set
            {
                if (SetPropertyValue<decimal>(nameof(Deposit), ref _Deposit, value)
                   && Total != 0)
        {
            get { return _TrangThai; }
            set { SetPropertyValue<TrangThaiNhapHang>(nameof(TrangThai), ref _TrangThai, value); }
                    MoneyMonth = Math.Round((Total - value) * 0.1m / 6, 1, MidpointRounding.AwayFromZero);
                }
            }
        }

        // Enum cho Trạng Thái Nhập Hàng
        public enum TrangThaiNhapHang
        private DateTime _Time;
        [XafDisplayName("Thời hạn trả")]
        public DateTime Time
        {
            [XafDisplayName("Đã đặt hàng")]
            DaDatHang,
            get { return _Time; }
            set { SetPropertyValue<DateTime>(nameof(Time), ref _Time, value); }
        }

            [XafDisplayName("Đã giao hàng")]
            DaGiaoHang,
        }

        // Phương thức Cập Nhật Trạng Thái Đơn Hàng
        public void CapNhatTrangThai(TrangThaiNhapHang trangThaiMoi)
        private DateTime _CreatedAt;
        [XafDisplayName("Ngày Đặt")]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy HH:mm}")]
        [ModelDefault("EditMask", "{0:dd/MM/yyyy HH:mm}")]
        public DateTime CreatedAt
        {
            TrangThai = trangThaiMoi;
            Save();
            get { return _CreatedAt; }
            set { SetPropertyValue<DateTime>(nameof(CreatedAt), ref _CreatedAt, value); }
        }
    }
}
