﻿namespace MoneyManager.Windows.Views
{
    public sealed partial class BackupView
    {
        public BackupView()
        {
            InitializeComponent();
            DataContext = Mvx.Resolve<BackupViewModel>();
        }
    }
}