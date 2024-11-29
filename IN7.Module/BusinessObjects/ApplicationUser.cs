using System.ComponentModel;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using IN7.Module.BusinessObjects;

namespace IN7.Module.BusinessObjects;

[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(FullName))]
[CurrentUserDisplayImage(nameof(Photo))]
public class ApplicationUser : PermissionPolicyUser, ISecurityUserWithLoginInfo, ISecurityUserLockout
{
    private int accessFailedCount;
    private DateTime lockoutEnd;

    public ApplicationUser(Session session) : base(session) { }

    [Browsable(false)]
    public int AccessFailedCount
    {
        get { return accessFailedCount; }
        set { SetPropertyValue(nameof(AccessFailedCount), ref accessFailedCount, value); }
    }

    [Browsable(false)]
    public DateTime LockoutEnd
    {
        get { return lockoutEnd; }
        set { SetPropertyValue(nameof(LockoutEnd), ref lockoutEnd, value); }
    }

    [Browsable(false)]
    [DevExpress.Xpo.Aggregated, Association("User-LoginInfo")]
    public XPCollection<ApplicationUserLoginInfo> LoginInfo
    {
        get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
    }

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey)
    {
        ApplicationUserLoginInfo result = new ApplicationUserLoginInfo(Session);
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }
    //customize

    private MediaDataObject photo;
    [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit,
        DetailViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 300,
        ListViewImageEditorCustomHeight = 40)]
    public MediaDataObject Photo
    {
        get { return photo; }
        set { SetPropertyValue(nameof(Photo), ref photo, value); }
    }

    [VisibleInListView(true)]
    private string _FullName;
    [XafDisplayName("Họ và Tên")]
    public string FullName
    {
        get { return _FullName; }
        set { SetPropertyValue<string>(nameof(FullName), ref _FullName, value); }

    }
    [VisibleInListView(true)]
    private string _Gender;
    [XafDisplayName("Giới tính")]
    public string Gender
    {
        get { return _Gender; }
        set { SetPropertyValue<string>(nameof(Gender), ref _Gender, value); }
    }
    [VisibleInListView(true)]
    private string _Phone;
    [XafDisplayName("Điện thoại")]
    public string Phone
    {
        get { return _Phone; }
        set { SetPropertyValue<string>(nameof(Phone), ref _Phone, value); }
    }
    [VisibleInListView(true)]
    private string _Address;
    [XafDisplayName("Địa chỉ")]
    public string Address
    {
        get { return _Address; }
        set { SetPropertyValue<string>(nameof(Address), ref _Address, value); }
    }

}
