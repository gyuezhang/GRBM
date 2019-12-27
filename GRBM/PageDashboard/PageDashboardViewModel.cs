using Stylet;

namespace GRBM
{
    public class PageDashboardViewModel : Screen
    {
        public PageDashboardViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }
        private WndMainViewModel wndMainVM { get; set; }

    }
}
