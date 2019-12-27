using Stylet;

namespace GRBM
{
    class PageSettingViewModel : Screen
    {
        public PageSettingViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }
        private WndMainViewModel wndMainVM { get; set; }

    }
}
