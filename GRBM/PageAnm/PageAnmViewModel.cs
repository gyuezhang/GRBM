using GRModel;
using GRSocket;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRBM
{
    public class PageAnmViewModel : Screen
    {
        public PageAnmViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
                    GRSocketHandler.getAnms += GRSocketHandler_getAnms;
                    GRSocketAPI.GetAnms();

        }

        private WndMainViewModel wndMainVM { get; set; }

        #region SocketHandler

        private void GRSocketHandler_addAnm(E_ResState state)
        {
            GRSocketHandler.addAnm -= GRSocketHandler_addAnm;
            switch (state)
            {
                case E_ResState.OK:
                    GRSocketHandler.getAnms += GRSocketHandler_getAnms;
                    GRSocketAPI.GetAnms();
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("添加公告失败");
                    break;
                default:
                    break;
            }

        }

        private void GRSocketHandler_getAnms(E_ResState state, List<C_Anm> anms)
        {
                    GRSocketHandler.getAnms -= GRSocketHandler_getAnms;
            switch (state)
            {
                case E_ResState.OK:
                    anmLstBd = anms;
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("获取公告失败");
                    break;
                default:
                    break;
            }

        }

        private void GRSocketHandler_delAnm(E_ResState state)
        {
            GRSocketHandler.delAnm -= GRSocketHandler_delAnm;
            switch(state)
            {
                case E_ResState.OK:
                    GRSocketHandler.getAnms += GRSocketHandler_getAnms;
                    GRSocketAPI.GetAnms();
                    break;
                case E_ResState.FAILED:
                    wndMainVM.messageQueueBd.Enqueue("删除公告失败");
                    break;
                default:
                    break;
            }
        }

        #endregion SocketHandler

        #region Bindings        

        public List<C_Anm> anmLstBd { get; set; } = new List<C_Anm>();
        public C_Anm cAnmBd { get; set; } = new C_Anm();
        public C_Anm curAnm { get; set; } = new C_Anm();
        #endregion Bindings

        #region Actions
        public void cAnmCmd ()
        {
            cAnmBd.CTime = DateTime.Now;
            GRSocketHandler.addAnm += GRSocketHandler_addAnm;
            GRSocketAPI.AddAnm(cAnmBd);
        
        }

        public void delAnmCmd()
        {
            GRSocketHandler.delAnm += GRSocketHandler_delAnm;
            GRSocketAPI.DelAnm(curAnm.Id);
        }


        #endregion Actions
    }
}
