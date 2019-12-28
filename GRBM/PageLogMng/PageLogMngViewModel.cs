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

            GRSocketHandler.getLogs += GRSocketHandler_getLogs;

            GRSocketAPI.GetLogs();
        }

        private WndMainViewModel wndMainVM { get; set; }

        #region SocketHandler

        private void GRSocketHandler_getLogs(RES_STATE state, List<C_Log> logs)
        {
            switch (state)
            {
                case RES_STATE.OK:
                    logLstBd = logs;
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

        public List<C_Log> logLstBd { get; set; } = new List<C_Log>();
        
        #endregion Bindings

        #region Actions

        public void RefreshCmd()
        {
            GRSocketAPI.GetLogs();
        }

        #endregion Actions
    }
}
