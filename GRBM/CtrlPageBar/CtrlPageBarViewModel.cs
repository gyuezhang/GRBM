using Stylet;

namespace GRBM
{
    class CtrlPageBarViewModel : Screen
    {
        public CtrlPageBarViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }
        private WndMainViewModel wndMainVM { get; set; }

    }
}
