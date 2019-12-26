using System;
using MaterialDesignThemes.Wpf;
using Stylet;

namespace GRBM
{
    public class WndLoginViewModel : Screen
    {
        #region Bindings

        public SnackbarMessageQueue messageQueueBd { get; set; } = new SnackbarMessageQueue(TimeSpan.FromSeconds(0.6));
        public int pageIndexBd { get; set; } = 0;
        public string pwdBd { get; set; }
        public string ipBd { get; set; }

        #endregion Bindings

        #region Actions

        public bool CanSettingCmd => pageIndexBd == 0;
        public void SettingCmd()
        {
            pageIndexBd = 1;
        }

        public void LoginCmd()
        {

        }

        public void TestLinkCmd()
        {

        }

        public void BackCmd()
        {

        }

        public void QuitCmd()
        {
            this.RequestClose();
        }

        #endregion Actions

    }
}
