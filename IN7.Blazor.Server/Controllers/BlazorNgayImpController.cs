using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp;
using IN7.Module.Controllers.ChungTu;
using Microsoft.AspNetCore.Components;

namespace IN7.Blazor.Server.Controllers
{
    public class BlazorNgayImpController : WindowController
    {
        private ParametrizedAction tungayImp;
        private ParametrizedAction denngayImp;

        protected override void OnActivated()
        {
            base.OnActivated();
            Controller controller = Frame.GetController<ImportProductListController>();
            if (controller != null)
            {
                tungayImp = Frame.GetController<ImportProductListController>().TungayImp;
                if (tungayImp != null) { tungayImp.CustomizeControl += Tungay_CustomizeControl; }
                denngayImp = Frame.GetController<ImportProductListController>().DenngayImp;
                if (denngayImp != null) { denngayImp.CustomizeControl += Denngay_CustomizeControl; }
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
            if (tungayImp != null) { tungayImp.CustomizeControl -= Tungay_CustomizeControl; tungayImp = null; }
            if (denngayImp != null) { denngayImp.CustomizeControl -= Denngay_CustomizeControl; denngayImp = null; }
            base.OnDeactivated();
        }
    }
}
