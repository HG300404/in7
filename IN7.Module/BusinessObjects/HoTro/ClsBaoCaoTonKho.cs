using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraExport.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IN7.Module.BusinessObjects.HoTro
{
    [NavigationItem("Thống kê")]
    [DomainComponent]
    [ImageName("doanhthu")]
    [System.ComponentModel.DisplayName("Báo cáo tồn kho")]
    public class RptTonKhoDinhKy
    {
        [DevExpress.ExpressApp.Data.Key]
        public Guid Oid { get; set; }

        [XafDisplayName("Tên Sản phẩm")]
        public string TenSP { get; set; }

        [XafDisplayName("Loại Sản phẩm")]
        public string LoaiSP { get; set; }

        [XafDisplayName("Số lượng còn")]
        [ModelDefault("DisplayFormat", "{0:### ### ###}")]
        public int? SoLuongCon { get; set; }

        [XafDisplayName("Ghi chú")]
        public string GhiChu { get; set; }

    }
    public class GetBaocao
    {
        public static IBindingList GetDoanhthu(IObjectSpace objectSpace)
        {
            BindingList<RptTonKhoDinhKy> reportData = new();
            Session session = ((XPObjectSpace)objectSpace).Session;

            // Câu lệnh SQL để tính toán tồn kho
            string sql = @"
                SELECT 
                    dbo.Products.Oid AS ProductID,
                    dbo.Products.Name AS ProductName,
                    dbo.Categories.Name AS CategoryName,
                    (dbo.Products.Quantity - ISNULL(SUM(dbo.BillDetails.Quantity), 0)) AS RemainingStock
                FROM dbo.Products
                LEFT JOIN dbo.Categories ON dbo.Products.Catergory = dbo.Categories.Oid
                LEFT JOIN dbo.BillDetails ON dbo.Products.Oid = dbo.BillDetails.Product
                GROUP BY dbo.Products.Oid, dbo.Products.Name, dbo.Categories.Name, dbo.Products.Quantity";

            SelectedData results = session.ExecuteQuery(sql);
            foreach (SelectStatementResultRow row in results.ResultSet[0].Rows)
            {
                int? remainingStock = row.Values[3] != null ? (int)row.Values[3] : 0;

                RptTonKhoDinhKy reportItem = new()
                {
                    Oid = (Guid)row.Values[0],
                    TenSP = row.Values[1] as string,
                    LoaiSP = row.Values[2] as string,
                    SoLuongCon = remainingStock,
                    GhiChu = remainingStock == 1 ? "Gần hết" : null // Xác định ghi chú
                };

                reportData.Add(reportItem);
            }
            return reportData;
        }
    }

}