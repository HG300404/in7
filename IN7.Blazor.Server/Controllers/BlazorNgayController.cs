using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp;
using IN7.Module.Controllers.ChungTu;
using Microsoft.AspNetCore.Components;

namespace IN7.Blazor.Server.Controllers
{
    public class BlazorNgayController : WindowController
    {
        private ParametrizedAction tungay;
        private ParametrizedAction denngay;

        protected override void OnActivated()
        {
            base.OnActivated();
            Controller controller = Frame.GetController<BillsListController>();
            if (controller != null)
            {
                tungay = Frame.GetController<BillsListController>().Tungay;
                if (tungay != null) { tungay.CustomizeControl += Tungay_CustomizeControl; }
                denngay = Frame.GetController<BillsListController>().Denngay;
                if (denngay != null) { denngay.CustomizeControl += Denngay_CustomizeControl; }
            }

        }
        private void Denngay_CustomizeControl(object sender, CustomizeControlEventArgs e)
        {
            if (e.Control is DxToolbarItemParametrizedActionControl actionControl &&
                        actionControl.EditModel is DxDateEditModel dateEditModel)
            {
                dateEditModel.Format = "dd/MM/yyyy";
                dateEditModel.CssClass += "width120";
                actionControl.ButtonModel.CssClass += "d-none"; //make button invisble;
                DxDateEditModel<DateTime> dateEditModel1 = (DxDateEditModel<DateTime>)actionControl.EditModel;
                var defaultDateChanged = dateEditModel1.DateChanged;
                dateEditModel1.DateChanged = EventCallback.Factory.Create<DateTime>(this, async date => {
                    await defaultDateChanged.InvokeAsync(date);
                    ((ParametrizedAction)sender).DoExecute(date);
                });
            }

        }

        private void Tungay_CustomizeControl(object sender, CustomizeControlEventArgs e)
        {
            if (e.Control is DxToolbarItemParametrizedActionControl actionControl &&
                        actionControl.EditModel is DxDateEditModel dateEditModel)
            {
                dateEditModel.Format = "dd/MM/yyyy";
                dateEditModel.CssClass += " width120";
                actionControl.ButtonModel.CssClass += "d-none"; //make button invisble;
                DxDateEditModel<DateTime> dateEditModel1 = (DxDateEditModel<DateTime>)actionControl.EditModel;
                var defaultDateChanged = dateEditModel1.DateChanged;
                dateEditModel1.DateChanged = EventCallback.Factory.Create<DateTime>(this, async date => {
                    await defaultDateChanged.InvokeAsync(date);
                    ((ParametrizedAction)sender).DoExecute(date);
                });
            }
        }
        protected override void OnDeactivated()
        {
            if (tungay != null) { tungay.CustomizeControl -= Tungay_CustomizeControl; tungay = null; }
            if (denngay != null) { denngay.CustomizeControl -= Denngay_CustomizeControl; denngay = null; }
            base.OnDeactivated();
        }
    }
}
