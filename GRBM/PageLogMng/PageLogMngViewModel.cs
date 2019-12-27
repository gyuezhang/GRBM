using Stylet;

namespace GRBM
{
    public class PageLogMngViewModel : Screen
    {
        public PageLogMngViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }
        private WndMainViewModel wndMainVM { get; set; }

    }
}
