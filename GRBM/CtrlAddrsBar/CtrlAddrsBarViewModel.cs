
using Stylet;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GRBM
{
    public class CtrlAddrsBarViewModel : Screen
    {
        public CtrlAddrsBarViewModel(WndMainViewModel _wndMainVM)
        {
            wndMainVM = _wndMainVM;
        }
        private WndMainViewModel wndMainVM { get; set; }

    }
}
