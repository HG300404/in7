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
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [NavigationItem("Chứng Từ")]
    public class ImportProducts(Session session) : BaseObject(session)
    {
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Khởi tạo giá trị mặc định
            TrangThai = TrangThaiNhapHang.DaDatHang;
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
        [XafDisplayName("Tên Người Liên Hệ"), Size(100)]
        public string ContactName
        {
            get { return _ContactName; }
            set { SetPropertyValue<string>(nameof(ContactName), ref _ContactName, value); }
        }

        private string _ContactEmail;
        [XafDisplayName("Email Người Liên Hệ"), Size(100)]
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

        private decimal _Total;
        [XafDisplayName("Tổng Tiền")]
        [ModelDefault("DisplayFormat", "{0:### ### ###}")]
        [ModelDefault("EditMask", "{0:### ### ###}")]
        public decimal Total
        {
            get { return _Total; }
            set { SetPropertyValue<decimal>(nameof(Total), ref _Total, value); }
        }

        private string _PaymentMethod;
        [XafDisplayName("Phương thức Thanh Toán"), Size(20)]
        public string PaymentMethod
        {
            get { return _PaymentMethod; }
            set { SetPropertyValue<string>(nameof(PaymentMethod), ref _PaymentMethod, value); }
        }

        private string _Time;
        [XafDisplayName("Thời hạn trả")]
        public string Time
        {
            get { return _Time; }
            set { SetPropertyValue<string>(nameof(Time), ref _Time, value); }
        }

        // Thêm Trường Trạng Thái
        private TrangThaiNhapHang _TrangThai;
        [XafDisplayName("Trạng Thái Nhập Hàng")]
        public TrangThaiNhapHang TrangThai
        {
            get { return _TrangThai; }
            set { SetPropertyValue<TrangThaiNhapHang>(nameof(TrangThai), ref _TrangThai, value); }
        }

        // Enum cho Trạng Thái Nhập Hàng
        public enum TrangThaiNhapHang
        {
            [XafDisplayName("Đã đặt hàng")]
            DaDatHang,

            [XafDisplayName("Đã giao hàng")]
            DaGiaoHang,
        }

        // Phương thức Cập Nhật Trạng Thái Đơn Hàng
        public void CapNhatTrangThai(TrangThaiNhapHang trangThaiMoi)
        {
            TrangThai = trangThaiMoi;
            Save();
        }
    }
}
