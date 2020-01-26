using System;
using GRModel;
using GRSocket;
using GRUtil;
using MaterialDesignThemes.Wpf;
using Stylet;

namespace GRBM
{
    public class WndLoginViewModel : Screen
    {
        private IWindowManager _windowManager;

        public WndLoginViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;

            GRSocketHandler.ConnState += GRSocketHandler_ConnState;
            GRSocketHandler.adminLogin += GRSocketHandler_adminLogin;

            ipBd = C_BmCfg.GetIp();
            GRSocket.GRSocket.Conn(ipBd);
        }

        #region SocketHandler

        private void GRSocketHandler_adminLogin(E_ResState state)
        {
            switch (state)
            {
                case E_ResState.OK:
                    messageQueueBd.Enqueue("登录成功");
                    GRSocketHandler.ConnState -= GRSocketHandler_ConnState;
                    GRSocketHandler.adminLogin -= GRSocketHandler_adminLogin;
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        var wndMainViewModel = new WndMainViewModel(_windowManager);
                        this._windowManager.ShowWindow(wndMainViewModel);
                        this.RequestClose();
                    }));
                    break;
                case E_ResState.FAILED:
                    messageQueueBd.Enqueue("密码错误");
                    break;
                default:
                    break;
            }
        }

        private void GRSocketHandler_ConnState(E_ResState state)
        {
            switch (state)
            {
                case E_ResState.OK:
                    C_BmCfg.SetIp(ipBd);
                    messageQueueBd.Enqueue("已成功连接到服务器");
                    break;
                case E_ResState.FAILED:
                    break;
                default:
                    break;
            }
        }

        #endregion SocketHandler

        #region Bindings

        public SnackbarMessageQueue messageQueueBd { get; set; } = new SnackbarMessageQueue(TimeSpan.FromSeconds(0.6));
        public int pageIndexBd { get; set; } = 0;
        public string pwdBd { get; set; }
        public string ipBd { get; set; }

        #endregion Bindings

        #region Actions

        public bool CanSettingCmd => pageIndexBd == 0;
        public void SettingCmd()
        {
            pageIndexBd = 1;
        }

        public void LoginCmd()
        {
            if(pwdBd == null || pwdBd.Length == 0)
            {
                messageQueueBd.Enqueue("密码不能为空");
                return;
            }

            GRSocketAPI.AdminLogin(C_Md5.GetHash(pwdBd));
            messageQueueBd.Enqueue("正在登录。。。");
        }

        public void TestLinkCmd()
        {
            GRSocket.GRSocket.Conn(ipBd);
        }

        public void BackCmd()
        {
            pageIndexBd = 0;
        }

        public void QuitCmd()
        {
            this.RequestClose();
        }

        #endregion Actions

    }
}
