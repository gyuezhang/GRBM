using Stylet;

namespace GRBM
{
    class PageLogMngViewModel : Screen
    {
        public PageLogMngViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }
        private WndMainViewModel wndMainVM { get; set; }

    }
}
