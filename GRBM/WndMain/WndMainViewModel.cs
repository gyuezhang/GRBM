using System;
using System.Windows;
using Stylet;

namespace GRBM
{
    public class WndMainViewModel : Screen
    {
        private IWindowManager _windowManager;

        public WndMainViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;

            //重置最大窗口尺寸（此处避免运行过程中任务栏显隐）
            maxHeightBd = SystemParameters.WorkArea.Height + 7;
            maxWidthBd = SystemParameters.WorkArea.Width + 7;
        }

        #region SocketHandler
        #endregion SocketHandler

        #region Bindings

        public double maxHeightBd { get; set; } = SystemParameters.WorkArea.Height + 7;
        public double maxWidthBd { get; set; } = SystemParameters.WorkArea.Width + 7;
        public WindowState windowStateBd { get; set; }
        public Thickness marginBd { get; set; } = new Thickness(0, 0, 0, 0);
        public Visibility dragImgVisibilityBd { get; set; } = Visibility.Visible;

        #endregion Bindings

        #region Actions

        public void StateChangedCmd()
        {
            //重置最大窗口尺寸（此处避免运行过程中任务栏显隐）
            maxHeightBd = SystemParameters.WorkArea.Height + 7;
            maxWidthBd = SystemParameters.WorkArea.Width + 7;
            marginBd = (windowStateBd == WindowState.Maximized) ? new Thickness(7, 7, 0, 0) : new Thickness(0, 0, 0, 0);
            dragImgVisibilityBd = (windowStateBd != WindowState.Maximized) ? Visibility.Visible : Visibility.Hidden;
        }
        #endregion Actions

    }
}
