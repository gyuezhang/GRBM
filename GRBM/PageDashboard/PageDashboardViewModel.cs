using Stylet;

namespace GRBM
{
    class PageDashboardViewModel : Screen
    {
        public PageDashboardViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }
        private WndMainViewModel wndMainVM { get; set; }

    }
}
