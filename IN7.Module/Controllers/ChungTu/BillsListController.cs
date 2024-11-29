using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp;
using IN7.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IN7.Module.Controllers.ChungTu
{
    public class BillsListController : ViewController
    {
        public ParametrizedAction Tungay;
        public ParametrizedAction Denngay;
        public BillsListController()
        {
            TargetViewId = "Bills_ListView";
            Tungay = new ParametrizedAction(this, "Tungay", "View", typeof(DateTime))
            {
                Caption = "Tu ngay",
                TargetViewId = "Bills_ListView"
            };
            Tungay.Execute += Tungay_Execute;

            Denngay = new ParametrizedAction(this, "Denngay", "View", typeof(DateTime))
            {
                Caption = "Den ngay",
                TargetViewId = "Bills_ListView"
            };
            Denngay.Execute += Denngay_Execute;
        }

        private void Denngay_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            if (e.ParameterCurrentValue != null)
            {
                _ = DateTime.TryParse(((DateTime)e.ParameterCurrentValue).ToShortDateString() + " 23:59:59", out DateTime ngay);
                ClsChung.fDenngay = ngay;
            }
        }

        private void Tungay_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            if (e.ParameterCurrentValue != null)
            {
                _ = DateTime.TryParse(((DateTime)e.ParameterCurrentValue).ToShortDateString() + " 00:00:00", out DateTime ngay);
                ClsChung.fTungay = ngay;
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Tungay.Value = DateTime.Now;
            Denngay.Value = DateTime.Now;
            ObjectSpace.Refreshing += ObjectSpace_Refreshing;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            ObjectSpace.Reloaded += ObjectSpace_Reloaded;
        }
        protected override void OnDeactivated()
        {
            ObjectSpace.Refreshing -= ObjectSpace_Refreshing;
            ObjectSpace.Reloaded -= ObjectSpace_Reloaded;
            base.OnDeactivated();
        }
        private void ObjectSpace_Reloaded(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void ObjectSpace_Refreshing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetFilter();
        }
        private void SetFilter()
        {
            CriteriaOperator cri = CriteriaOperator.Parse("CreatedAt>=? && CreatedAt<=?", ClsChung.fTungay, ClsChung.fDenngay);
            ((ListView)View).CollectionSource.Criteria["lọc"] = cri;
        }
    }
}
