using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using IN7.Module.BusinessObjects;
using IN7.Module.BusinessObjects.HoTro;
using DevExpress.XtraExport.Xls;
using QLKD.Module;
using DevExpress.ExpressApp.Security.ClientServer;

namespace IN7.Module;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class IN7Module : ModuleBase {
    public IN7Module() {
		// 
		// IN7Module
		// 
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifference));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifferenceAspect));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.BaseObject));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.FileData));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.FileAttachmentBase));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.Event));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.Resource));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.CloneObject.CloneObjectModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Dashboards.DashboardsModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Notifications.NotificationsModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Office.OfficeModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.ReportsModuleV2));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Scheduler.SchedulerModuleBase));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        // Manage various aspects of the application UI and behavior at the module level.
        ApplicationHelper.Instance.Initialize(application);
        application.LoggedOn += Application_LoggedOn;
        application.SetupComplete += Application_SetupComplete;
        ClsChung.fTungay = DateTime.Now;
        ClsChung.fDenngay = DateTime.Now;
    }
    private void Application_SetupComplete(object sender, EventArgs e)
    {
        Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
    }
    private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e)
    {
        if (e.ObjectSpace is NonPersistentObjectSpace space)
        {
            IObjectSpace additionalObjectSpace = Application.CreateObjectSpace(typeof(ApplicationUser));
            space.AdditionalObjectSpaces.Add(additionalObjectSpace);

            space.ObjectGetting += ObjectSpace_ObjectGetting;
            e.ObjectSpace.Disposed += (s, args) =>
            {
                ((NonPersistentObjectSpace)s).ObjectGetting -= ObjectSpace_ObjectGetting;
                additionalObjectSpace.Dispose();
            };
        }
        if (e.ObjectSpace is NonPersistentObjectSpace nonPersistentObjectSpace)
        {
            nonPersistentObjectSpace.ObjectsGetting += NonPersistentObjectSpace_ObjectsGetting;
        }
    }
    private void NonPersistentObjectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
    {
        NonPersistentObjectSpace obs = (NonPersistentObjectSpace)sender;
        XPObjectSpace objectSpace = (XPObjectSpace)obs.AdditionalObjectSpaces[0];
        if (e.ObjectType == typeof(RptTonKhoDinhKy))
        {
            e.Objects = GetBaocao.GetDoanhthu(objectSpace);
        }
    }

    private void ObjectSpace_ObjectGetting(object sender, ObjectGettingEventArgs e)
    {
        if (e.SourceObject is IObjectSpaceLink)
        {
            e.TargetObject = e.SourceObject;
            ((IObjectSpaceLink)e.TargetObject).ObjectSpace = (IObjectSpace)sender;
        }
    }

    private void Application_LoggedOn(object sender, LogonEventArgs e)
    {
        XafApplication app = (XafApplication)sender;

        IObjectSpaceProvider objectSpaceProvider = app.ObjectSpaceProviders[0];
        ((SecuredObjectSpaceProvider)objectSpaceProvider).AllowICommandChannelDoWithSecurityContext = true;
    }
    public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
        base.CustomizeTypesInfo(typesInfo);
        CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
    }
}
