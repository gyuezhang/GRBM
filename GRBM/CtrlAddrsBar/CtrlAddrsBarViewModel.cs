
using Stylet;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GRBM
{
    public class CtrlAddrsBarViewModel : Screen
    {
        public CtrlAddrsBarViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
            Update(E_Page.Dashboard);
        }
        private WndMainViewModel wndMainVM { get; set; }

        #region Bindings

        public List<C_AddrsBarItem> itemsBd { get; set; } = new List<C_AddrsBarItem>();

        #endregion Bindings

        #region Actions

        public void chipCmd(string pageId)
        {
            try
            {
                E_Page id = (E_Page)Enum.Parse(typeof(E_Page), pageId);
                wndMainVM.SelectPage(id);
            }
            catch
            {

            }
        }

        #endregion Actions

        public void Update(E_Page id)
        {
            itemsBd.Clear();
            switch (id)
            {
                case E_Page.Dashboard:
                    itemsBd.Add(new C_AddrsBarItem(E_Page.Dashboard));
                    break;
                case E_Page.GroupMng:
                    itemsBd.Add(new C_AddrsBarItem(E_Page.GroupMng));
                    break;
                case E_Page.DeptMng:
                    itemsBd.Add(new C_AddrsBarItem(E_Page.GroupMng));
                    itemsBd.Add(new C_AddrsBarItem(E_Page.DeptMng));
                    break;
                case E_Page.UserMng:
                    itemsBd.Add(new C_AddrsBarItem(E_Page.GroupMng));
                    itemsBd.Add(new C_AddrsBarItem(E_Page.UserMng));
                    break;
                case E_Page.LogMng:
                    itemsBd.Add(new C_AddrsBarItem(E_Page.LogMng));
                    break;
                case E_Page.AnmMng:
                    itemsBd.Add(new C_AddrsBarItem(E_Page.AnmMng));
                    break;
                case E_Page.Setting:
                    itemsBd.Add(new C_AddrsBarItem(E_Page.Setting));
                    break;
                case E_Page.Setting_AdminResetPwd:
                    itemsBd.Add(new C_AddrsBarItem(E_Page.Setting));
                    itemsBd.Add(new C_AddrsBarItem(E_Page.Setting_AdminResetPwd));
                    break;
                default:
                    break;
            }
            itemsBd[itemsBd.Count - 1].VHasChild = Visibility.Collapsed;
        }
    }
}
