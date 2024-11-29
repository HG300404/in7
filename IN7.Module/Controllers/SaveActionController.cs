using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IN7.Module.Controllers
{
    public class SaveActionController : ViewController
    {
        public SaveActionController()
        {
            TargetViewId = "ApplicationUser_DetailView;Customers_DetailView;";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ModificationsController modificationsController = Frame.GetController<ModificationsController>();
            if (modificationsController != null)
            {
                modificationsController.SaveAndCloseAction.Active.SetItemValue("an", false);
                modificationsController.SaveAndNewAction.Active.SetItemValue("an", false);
            }
        }
        protected override void OnDeactivated()
        {
            ModificationsController modificationsController = Frame.GetController<ModificationsController>();
            if (modificationsController != null)
            {
                modificationsController.SaveAndCloseAction.Active.RemoveItem("an");
                modificationsController.SaveAndNewAction.Active.RemoveItem("an");
            }
            base.OnDeactivated();
        }
    }
}
