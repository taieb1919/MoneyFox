﻿namespace MoneyManager.Windows.Views
{
    public sealed partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = Mvx.Resolve<MainViewModel>();
        }
    }
}