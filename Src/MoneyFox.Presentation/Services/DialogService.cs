﻿using MoneyFox.Application.Common.Interfaces;
using System.Threading.Tasks;
using MoneyFox.Application.Resources;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace MoneyFox.Presentation.Services
{
    public class DialogService : IDialogService
    {
        public async Task ShowMessage(string title, string message) {
            await MaterialDialog.Instance.AlertAsync(message,
                                                     title,
                                                     Strings.OkLabel,
                                                     GetAlertDialogConfiguration());
        }

        public async Task<bool> ShowConfirmMessageAsync(string title, string message, string positiveButtonText = null, string negativeButtonText = null) {

            bool? wasConfirmed = await MaterialDialog.Instance.ConfirmAsync(message,
                                                     title,
                                                     Strings.OkLabel,
                                                     Strings.CancelLabel,
                                                     GetAlertDialogConfiguration());

            return wasConfirmed ?? false;
        }

        private IMaterialModalPage loadingDialog;

        /// <inheritdoc />
        public async Task ShowLoadingDialogAsync(string message = null)
        {
            loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: message ?? Strings.LoadingLabel, GetLoadingDialogConfiguration());
        }

        /// <inheritdoc />
        public async Task HideLoadingDialogAsync()
        {
            await loadingDialog.DismissAsync();
        }

        private static MaterialAlertDialogConfiguration GetAlertDialogConfiguration() 
        {
            return new MaterialAlertDialogConfiguration 
            {
                BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.BACKGROUND),
                TitleTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
                MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
                TintColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
                ButtonAllCaps = false
            };
        }
        private static MaterialLoadingDialogConfiguration GetLoadingDialogConfiguration() 
        {
            return new MaterialLoadingDialogConfiguration
            {
                BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.BACKGROUND),
                MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
                TintColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
            };
        }
    }
}
