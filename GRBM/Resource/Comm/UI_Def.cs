using Stylet;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace GRBM
{
    public enum E_Page
    {
        Dashboard,
        GroupMng,
            DeptMng,
            UserMng,
        LogMng,
        Setting,
            Setting_AdminResetPwd,
    }

    public class C_AddrsBarItem : PropertyChangedBase
    {
        public E_Page PageId { get; set; }
        private string _cont;
        private PackIconKind _iconKind;
        private Visibility _vHasChild;
        private string _strPageId;

        public string StrPageId
        {
            get { return _strPageId; }
            set
            {
                SetAndNotify(ref _strPageId, value);
            }
        }

        public string Cont
        {
            get { return _cont; }
            set
            {
                SetAndNotify(ref _cont, value);
            }
        }

        public PackIconKind IconKind
        {
            get { return _iconKind; }
            set
            {
                SetAndNotify(ref _iconKind, value);
            }
        }

        public Visibility VHasChild
        {
            get { return _vHasChild; }
            set
            {
                SetAndNotify(ref _vHasChild, value);
            }
        }

        public C_AddrsBarItem(E_Page id)
        {
            PageId = id;
            Cont = GetPageIdName();
            IconKind = GetIconKind();
            VHasChild = Visibility.Visible;
            StrPageId = id.ToString();
        }

        private string GetPageIdName()
        {
            switch (PageId)
            {
                case E_Page.Dashboard:
                    return "仪表板";
                case E_Page.GroupMng:
                    return "组织管理";
                case E_Page.DeptMng:
                    return "部门管理";
                case E_Page.UserMng:
                    return "用户管理";
                case E_Page.LogMng:
                    return "日志管理";
                case E_Page.Setting:
                    return "设置";
                case E_Page.Setting_AdminResetPwd:
                    return "管理员重置密码";
                default:
                    return "";
            }
        }

        private PackIconKind GetIconKind()
        {
            switch (PageId)
            {
                case E_Page.Dashboard:
                    return PackIconKind.ViewDashboard;
                case E_Page.GroupMng:
                    return PackIconKind.AccountsGroup;
                case E_Page.DeptMng:
                    return PackIconKind.DepartureBoard;
                case E_Page.UserMng:
                    return PackIconKind.User;
                case E_Page.LogMng:
                    return PackIconKind.DateRange;
                case E_Page.Setting:
                    return PackIconKind.Settings;
                case E_Page.Setting_AdminResetPwd:
                    return PackIconKind.PasswordReset;
                default:
                    return PackIconKind.Brightness1;
            }
        }

    }
}
