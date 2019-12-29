using Stylet;

namespace GRBM
{
    public class CtrlPageBarViewModel : Screen
    {
        public CtrlPageBarViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }
        private WndMainViewModel wndMainVM { get; set; }

        #region Bindings

        public int allCntBd { get; set; }
        public int pIndexBd { get; set; }
        public int pLastBd { get; set; }
        public int pJumpBd { get; set; }
        #endregion Bindings

        #region Actions

        public void JumpBmd()
        {

        }

        #endregion Actions

        private int unitLen { get; set; } = 100;
    }
}
