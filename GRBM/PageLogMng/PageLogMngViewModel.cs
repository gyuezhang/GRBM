using GRModel;
using GRSocket;
using GRUtil;
using Stylet;
using System.Collections.Generic;

namespace GRBM
{
    public class PageLogMngViewModel : Screen
    {
        public PageLogMngViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
            pageBarVmBd = new CtrlPageBarViewModel(wndMainVM);

            RefreshCmd();
        }

        private WndMainViewModel wndMainVM { get; set; }

        #region SocketHandler

        private void GRSocketHandler_getLogs(RES_STATE state, List<C_Log> logs)
        {
            GRSocketHandler.getLogs -= GRSocketHandler_getLogs;

            switch (state)
            {
                case RES_STATE.OK:
                    pageBarVmBd.Init(logs);
                    break;
                case RES_STATE.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("获取日志失败");
                    break;
                default:
                    break;
            }
        }

        #endregion SocketHandler

        #region Bindings        

        public CtrlPageBarViewModel pageBarVmBd { get; set; }

        #endregion Bindings

        #region Actions

        public void RefreshCmd()
        {
            GRSocketHandler.getLogs += GRSocketHandler_getLogs;
            GRSocketAPI.GetLogs();
        }

        #endregion Actions
    }
}
