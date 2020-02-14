using GRModel;
using GRSocket;
using GRUtil;
using Stylet;
using System;

namespace GRBM
{
    public class PageSettingViewModel : Screen
    {
        public PageSettingViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }

        private WndMainViewModel wndMainVM { get; set; }

        #region SocketHandler

        private void GRSocketHandler_adminResetPwd(E_ResState state)
        {
            GRSocketHandler.adminResetPwd -= GRSocketHandler_adminResetPwd;
            switch (state)
            {
                case E_ResState.OK:
                    wndMainVM.messageQueueBd.Enqueue("重置密码成功，建议尽快重新登录");
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("重置密码失败");
                    break;
                default:
                    break;
            }
        }

        #endregion SocketHandler

        #region Bindings        

        public int pageIndexBd { get; set; } = 0;
        public string pwdOldBd { get; set; } = "";
        public string pwdNewBd { get; set; } = "";
        public string pwdNew2Bd { get; set; } = "";

        #endregion Bindings

        #region Actions

        public void SelectPageCmd(string cmdPara)
        {
            wndMainVM.SelectPage((E_Page)Enum.Parse(typeof(E_Page), cmdPara, true));
        }

        public void changePwdOKCmd()
        {
            if (pwdOldBd == "")
            {
                wndMainVM.messageQueueBd.Enqueue("旧密码不能为空");
                return;
            }

            if (pwdNewBd == "")
            {
                wndMainVM.messageQueueBd.Enqueue("新密码不能为空");
                return;
            }

            if (pwdNewBd != pwdNew2Bd)
            {
                wndMainVM.messageQueueBd.Enqueue("新密码不一致");
                return;
            }

            if (pwdOldBd == pwdNewBd)
            {
                wndMainVM.messageQueueBd.Enqueue("新旧密码一致，未作出更改");
                return;
            }

            GRSocketHandler.adminResetPwd += GRSocketHandler_adminResetPwd;
            GRSocketAPI.AdminResetPwd(C_Md5.GetHash(pwdOldBd), C_Md5.GetHash(pwdNewBd));
        }

        #endregion Actions
    }
}
