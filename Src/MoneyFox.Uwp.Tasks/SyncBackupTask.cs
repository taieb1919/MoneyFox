﻿using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Microsoft.Identity.Client;
using MoneyFox.Application;
using MoneyFox.Application.Adapters;
using MoneyFox.Application.Backup;
using MoneyFox.Application.Constants;
using MoneyFox.Application.Facades;
using MoneyFox.Presentation;
using MoneyFox.Uwp.Business;

namespace MoneyFox.Uwp.Tasks
{
    /// <inheritdoc />
    /// <summary>
    ///     Background task to sync the backup with OneDrive.
    /// </summary>
    public sealed class SyncBackupTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            Debug.WriteLine("Sync Backup started.");
            ExecutingPlatform.Current = AppPlatform.UWP;

            var settingsFacade = new SettingsFacade(new SettingsAdapter());

            if (!settingsFacade.IsBackupAutouploadEnabled || !settingsFacade.IsLoggedInToBackupService) return;

            try
            {
                IPublicClientApplication pca = PublicClientApplicationBuilder
                                               .Create(ServiceConstants.MSAL_APPLICATION_ID)
                                               .WithRedirectUri($"msal{ServiceConstants.MSAL_APPLICATION_ID}://auth")
                                               .Build();

                var backupService = new BackupService(new OneDriveService(pca),
                                                      new WindowsFileStore(),
                                                      settingsFacade,
                                                      new ConnectivityAdapter(),
                                                      ViewModelLocator.MessengerInstance);

                DateTime backupDate = await backupService.GetBackupDateAsync();

                if (settingsFacade.LastDatabaseUpdate > backupDate) return;

                await backupService.RestoreBackupAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine("Sync Backup failed.");
            }
            finally
            {
                settingsFacade.LastExecutionTimeStampSyncBackup = DateTime.Now;
                Debug.WriteLine("Sync Backup finished.");
                deferral.Complete();
            }
        }
    }
}
