using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Data;

namespace IN7.Module.BusinessObjects
{
    [DomainComponent]
    [NavigationItem("Chứng Từ")]
    [System.ComponentModel.DisplayName("Doanh Thu Sản Phẩm")]
    public class DoanhThuSanPhamRpt : NonPersistentBaseObject
    {
        [Key]
        public int Oid { get; set; }

        // Thuộc tính cho sản phẩm
        [XafDisplayName("Sản phẩm")]
        public string ProductName { get; set; }

        [XafDisplayName("Tổng doanh thu")]
        [ModelDefault("DisplayFormat", "{0:# ### ###;-# ### ###;0}")]
        [ModelDefault("EditMask", "# ### ###")]
        public decimal TotalRevenue { get; set; }

        [XafDisplayName("Số lượng còn lại")]
        public int AvailableQuantity { get; set; }

        [XafDisplayName("Số lượng bán")]
        public int QuantitySold { get; set; }

        [XafDisplayName("Số lượng nhập")]
        public int QuantityImported { get; set; }

        [XafDisplayName("Tổng chi phí")]
        [ModelDefault("DisplayFormat", "{0:# ### ###;-# ### ###;0}")]
        [ModelDefault("EditMask", "# ### ###")]
        public decimal TotalCost { get; set; }

        [XafDisplayName("Lợi nhuận")]
        [ModelDefault("DisplayFormat", "{0:# ### ###;-# ### ###;0}")]
        [ModelDefault("EditMask", "# ### ###")]
        public decimal Profit { get; set; }

    }
}
