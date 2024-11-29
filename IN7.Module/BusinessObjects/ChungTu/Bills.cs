using DevExpress.CodeParser;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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
using System.ComponentModel.DataAnnotations;
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


        [DevExpress.Xpo.Association]
        public XPCollection<InstallmentBill> InstallmentBills
        {
            get { return GetCollection<InstallmentBill>(nameof(InstallmentBills)); }
        }


        [DevExpress.Xpo.Aggregated, DevExpress.Xpo.Association]
        public XPCollection<BillDetails> BillDetails
        {
            get { return GetCollection<BillDetails>(nameof(BillDetails)); }
        }




        private Customers _Customer;
        [XafDisplayName("Khách Hàng")]
        [DevExpress.Xpo.Association]
        public Customers Customer
        {
            get { return _Customer; }
            set { SetPropertyValue(nameof(Customer), ref _Customer, value); }
        }


        private Employees _Employee;
        [XafDisplayName("Nhân Viên")]
        [DevExpress.Xpo.Association]
        public Employees Employee
        {
            get { return _Employee; }
            set { SetPropertyValue(nameof(Employee), ref _Employee, value); }
        }

        private decimal _Tax = 0.1m; // Gán giá trị mặc định là 10%

        [XafDisplayName("Thuế")]
        [ModelDefault("DisplayFormat", "{0:P0}")] // Hiển thị dạng phần trăm
        public decimal Tax
        {
            get { return _Tax; }
            set
            {
                // Không cho phép thay đổi giá trị của Tax
                _Tax = 0.1m;
            }
        }

        

        private decimal _ProductPrice;
        [XafDisplayName("Giá SP")]
        [ModelDefault("DisplayFormat", "{0:#,##0.00 ₫}")]
        [ModelDefault("EditMask", "n2")]
        public decimal ProductPrice
        {
            get
            {
                if (!IsSaving && !IsLoading)
                {
                    ProductPrice = CalculateProductPrice();
                }
                return _ProductPrice;
            }
            set
            {
                SetPropertyValue(nameof(ProductPrice), ref _ProductPrice, value);  
            }
        }


        // Phương thức riêng biệt để tính toán giá trị ProductPrice
        private decimal CalculateProductPrice()
        {
            decimal price = 0;
            if (BillDetails != null)
            {
                foreach (BillDetails item in BillDetails)
                {
                    price += item.Price;
                }
            }
            else return 0;
            // Tính toán giá trị sau khi thêm thuế
            return price * (1 + Tax);
        }





        private decimal _TotalAmount;

        [XafDisplayName("Tổng Tiền")]
        [ModelDefault("DisplayFormat", "{0:#,##0.00 ₫}")]
        [ModelDefault("EditMask", "n2")]
        public decimal TotalAmount
        {
            get
            {
                if (!IsLoading && !IsSaving && Type != null){
                    if (Type == PaymentType.OneTime)
                    {
                        TotalAmount = ProductPrice;
                    } else if (Type == PaymentType.Installment)
                    {
                        if (Deposit != null)
                        {
                            TotalAmount = Deposit + (MoneyMonth * (int)Option);
                        }
                        
                    }
                }
                return _TotalAmount;
            }
            set
            {
                SetPropertyValue(nameof(TotalAmount), ref _TotalAmount, value);
                
            }
        }

        public enum OptionType
        {
            [XafDisplayName("3 tháng - 30%")]
            Three = 3,
            [XafDisplayName("6 tháng - 20%")]
            Six = 6,
            [XafDisplayName("9 tháng - 15%")]
            Nine = 9,
            [XafDisplayName("12 tháng - 10%")]
            Twelve = 12
        }

        private OptionType _Option;
        [XafDisplayName("Kỳ hạn trả góp")]
        public OptionType Option
        {
            get { return _Option; }
            set
            {
                if (SetPropertyValue(nameof(Option), ref _Option, value))
                {
                    decimal minimumPercentage = 0m;
                    switch (value)
                    {
                        case OptionType.Three:
                            minimumPercentage = 30m;
                            break;
                        case OptionType.Six:
                            minimumPercentage = 20m;
                            break;
                        case OptionType.Nine:
                            minimumPercentage = 15m;
                            break;
                        case OptionType.Twelve:
                            minimumPercentage = 10m;
                            break;
                    }

                    // Tính toán Minimum theo công thức mới
                    Minimum = ProductPrice * (minimumPercentage / 100);

                    // Cập nhật Time khi chọn Option
                    Time = DateTime.Now.AddMonths((int)value).ToString("dd/MM/yyyy HH:mm");
                }
            }
        }

        private decimal _Minimum;
        [XafDisplayName("Cọc tối thiểu")]
        [ModelDefault("DisplayFormat", "{0:#,##0.00 ₫}")]
        [ModelDefault("EditMask", "n2")]
        [ReadOnly(true)]
        public decimal Minimum
        {
            get { return _Minimum; }
            set { SetPropertyValue(nameof(Minimum), ref _Minimum, value); }
        }

        private decimal _Deposit;
        [XafDisplayName("Tiền Cọc")]
        [ModelDefault("DisplayFormat", "{0:#,##0.00 ₫}")]
        [ModelDefault("EditMask", "n2")]
        //[RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, "Minimum",
        //CustomMessageTemplate = "Tiền Cọc phải lớn hơn hoặc bằng Cọc tối thiểu")]
        public decimal Deposit
        {
            get { return _Deposit; }
            set
            {
                if (SetPropertyValue<decimal>(nameof(Deposit), ref _Deposit, value))
                {
                    decimal minimumPercentage = 0m;
                    if (Option != 0 && (int)Option != 0)
                    {
                        switch (Option)
                        {
                            case OptionType.Three:
                                minimumPercentage = 30m;
                                break;
                            case OptionType.Six:
                                minimumPercentage = 20m;
                                break;
                            case OptionType.Nine:
                                minimumPercentage = 15m;
                                break;
                            case OptionType.Twelve:
                                minimumPercentage = 10m;
                                break;
                        }

                        // Đảm bảo rằng Option không bằng 0 trước khi thực hiện phép chia
                        if ((int)Option != 0)
                        {
                            MoneyMonth = ((ProductPrice - Deposit) * (1 + minimumPercentage / 100)) / (int)Option;
                        }
                        else
                        {
                            MoneyMonth = 0; // Hoặc giá trị mặc định khác nếu cần
                        }
                    }
                    else
                    {
                        MoneyMonth = 0; // Hoặc giá trị mặc định khác nếu cần
                    }


                }
            }
        }

        private decimal _MoneyMonth;
        [XafDisplayName("Tiền tháng")]
        [ModelDefault("DisplayFormat", "{0:#,##0.00 ₫}")]
        [ModelDefault("EditMask", "n2")]
        public decimal MoneyMonth
        {
            get { return _MoneyMonth; }
            set { SetPropertyValue<decimal>(nameof(MoneyMonth), ref _MoneyMonth, value); }
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


        public enum PaymentType
        {
            [XafDisplayName("Trả hết")]
            OneTime,
            [XafDisplayName("Trả góp")]
            Installment
        }


        private string _Time;
        [XafDisplayName("Thời hạn trả")]
        public string Time
        {
            get { return _Time; }
            set { SetPropertyValue<string>(nameof(Time), ref _Time, value); }
        }

        private PaymentType _Type;
        [XafDisplayName("Loại hình trả")]
        public PaymentType Type
        {
            get { return _Type; }
            set
            {
                SetPropertyValue(nameof(Type), ref _Type, value);
            }
        }


        public enum StatusOption
        {
            [XafDisplayName("Đã đặt")]
            Ordered,
            [XafDisplayName("Giao hàng")]
            Delivering,
            [XafDisplayName("Hoàn thành")]
            Completed
        }

        private StatusOption _Status = StatusOption.Ordered;
        public StatusOption Status
        {
            get { return _Status; }
            set { SetPropertyValue(nameof(Status), ref _Status, value); }
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


        //private DateTime _UpdatedAt;
        //[XafDisplayName("Ngày Sửa")]
        //[ModelDefault("DisplayFormat", "{0:dd/MM/yyyy HH:mm}")]
        //[ModelDefault("EditMask", "{0:dd/MM/yyyy HH:mm}")]
        //public DateTime UpdatedAt
        //{
        //    get { return _UpdatedAt; }
        //    set { SetPropertyValue<DateTime>(nameof(UpdatedAt), ref _UpdatedAt, value); }
        //}

    }
}