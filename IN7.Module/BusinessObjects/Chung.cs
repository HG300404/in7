using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using IN7.Module.BusinessObjects.ChungTu;
using IN7.Module.BusinessObjects.HoTro;
using Microsoft.Identity.Client;

namespace IN7.Module.BusinessObjects
{
    public class Chung
    {
        public static IBindingList getDoanhThu(IObjectSpace objectSpace)
        {
            BindingList<DoanhThuRpt> objects = new();

            Session session = ((XPObjectSpace)objectSpace).Session;

            XPCollection<Bills> bills = new XPCollection<Bills>(session);
            XPCollection<ImportProducts> importProducts = new XPCollection<ImportProducts>(session);

            var groupedData = bills
                .GroupBy(b => new { Year = b.CreatedAt.Year, Month = b.CreatedAt.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    TotalRevenue = g.Sum(x => x.TotalAmount),
                    TotalCost = 0m
                }).ToList();

            var groupedData2 = importProducts
                .GroupBy(b => new { Year = b.CreatedAt.Year, Month = b.CreatedAt.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    TotalRevenue = 0m,
                    TotalCost = g.Sum(x => x.Total),
                }).ToList();
            Console.WriteLine($"Danh sách có {groupedData2.Count} phần tử.");


            var combinedData = groupedData
                .Select(item => new
                {
                    Year = item.Year,
                    Month = item.Month,
                    TotalRevenue = item.TotalRevenue,
                    TotalCost = item.TotalCost,
                })
                .Concat(groupedData2
                    .Select(item => new
                    {
                        Year = item.Year,
                        Month = item.Month,
                        TotalRevenue = item.TotalRevenue,
                        TotalCost = item.TotalCost
                    }).AsEnumerable()
                )
                .GroupBy(item => new { Year = item.Year, Month = item.Month })
                .Select(group => new
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    TotalRevenue = group.Sum(item => item.TotalRevenue),
                    TotalCost = group.Sum(item => item.TotalCost)
                })
                .OrderBy(item => item.Year).ThenBy(item => item.Month)
                .ToList();

            Console.WriteLine($"Danh sách có {combinedData.Count} phần tử.");


            foreach (var result in combinedData)
            {
                DoanhThuRpt obj = new()
                {
                    Year = result.Year,
                    Month = result.Month,
                    TotalRevenue = result.TotalRevenue,
                    TotalCost = result.TotalCost,
                    Profit = result.TotalRevenue - result.TotalCost
                };
                objects.Add(obj);
            }
            return objects;
        }

        public static IBindingList getDoanhThuSanPham(IObjectSpace objectSpace)
        {
            BindingList<DoanhThuSanPhamRpt> objects = new();

            Session session = ((XPObjectSpace)objectSpace).Session;

            string sql = @"
                SELECT 
                    p.Oid AS ProductId,
                    p.Name AS ProductName,
                    p.Quantity AS AvailableQuantity,
                    COALESCE(SUM(bd.Quantity), 0) AS QuantitySold,
                    COALESCE(SUM(bd.Quantity * bd.UnitPrice), 0) AS TotalRevenue,
                    COALESCE(SUM(ipd.Quantity), 0) AS QuantityImported,
                    COALESCE(SUM(ipd.Quantity * ipd.UnitPrice), 0) AS TotalCost
                FROM 
                    Products p
                LEFT JOIN 
                    BillDetails bd ON p.Oid = bd.Product
                LEFT JOIN 
                    ImportProductDetails ipd ON p.Oid = ipd.Product
                GROUP BY 
                    p.Oid, p.Name, p.Quantity
            ";

            SelectedData results = session.ExecuteQuery(sql);

            foreach (SelectStatementResultRow row in results.ResultSet[0].Rows)
            {
                DoanhThuSanPhamRpt obj = new()
                {
                    ProductName = row.Values[1] as string,
                    AvailableQuantity = (int)row.Values[2],
                    QuantitySold = (int)row.Values[3],
                    TotalRevenue = (decimal)row.Values[4],
                    QuantityImported = (int)row.Values[5],
                    TotalCost = (decimal)row.Values[6],
                    Profit = (decimal)row.Values[4] - (decimal)row.Values[6]
                };
                objects.Add(obj);
            }
            return objects;
        }
    }
}
