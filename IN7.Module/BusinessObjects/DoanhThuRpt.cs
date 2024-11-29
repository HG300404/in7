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
    [System.ComponentModel.DisplayName("Doanh Thu")]
    public class DoanhThuRpt : NonPersistentBaseObject
    {
        [Key]
        public int Oid { get; set; }

        [XafDisplayName("Năm")]
        [ModelDefault("DisplayFormat", "####")]
        public int Year { get; set; }

        [XafDisplayName("Tháng")]
        public int Month { get; set; }

        [XafDisplayName("Tổng doanh thu")]
        [ModelDefault("DisplayFormat", "{0:# ### ###;-# ### ###;0}")]
        [ModelDefault("EditMask", "# ### ###")]
        public decimal TotalRevenue { get; set; }

        [XafDisplayName("Tổng chi phí nhập vào")]
        [ModelDefault("DisplayFormat", "{0:# ### ###;-# ### ###;0}")]
        [ModelDefault("EditMask", "# ### ###")]
        public decimal TotalCost { get; set; }

        [XafDisplayName("Lợi nhuận")]
        [ModelDefault("DisplayFormat", "{0:# ### ###;-# ### ###;0}")]
        [ModelDefault("EditMask", "# ### ###")]
        public decimal Profit { get; set; }

    }
}
