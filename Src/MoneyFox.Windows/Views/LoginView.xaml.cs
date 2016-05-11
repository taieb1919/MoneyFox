﻿using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MoneyFox.Shared.Interfaces;
using MoneyFox.Shared.Resources;
using MvvmCross.Platform;

namespace MoneyFox.Windows.Views
{
    public sealed partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void PasswordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            if (!Mvx.Resolve<IPasswordStorage>().ValidatePassword(PasswordBox.Password))
            {
                Mvx.Resolve<IDialogService>().ShowMessage(Strings.PasswordWrongTitle, Strings.PasswordWrongMessage);
                return;
            }

            AppShell.Current.SetLoggedInView();
            AppShell.Current.AppMyFrame.Navigate(typeof(MainView));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }

            base.OnNavigatingFrom(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Frame.BackStack.Clear();
        }
    }
}