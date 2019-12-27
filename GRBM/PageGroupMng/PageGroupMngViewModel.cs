using Stylet;

namespace GRBM
{
    class PageGroupMngViewModel : Screen
    {
        public PageGroupMngViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }
        private WndMainViewModel wndMainVM { get; set; }

    }
}
