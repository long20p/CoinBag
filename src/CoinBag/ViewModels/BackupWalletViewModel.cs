using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoinBag.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CoinBag.ViewModels
{
    public class BackupWalletViewModel : ViewModelBase
    {
        private IWalletService walletService;
        private IFileService fileService;
        private INotificationService notificationService;

        public BackupWalletViewModel(IWalletService walletService, IFileService fileService, INotificationService notificationService)
        {
            this.walletService = walletService;
            this.fileService = fileService;
            this.notificationService = notificationService;
            Title = "Backup Wallet";
            BackupCommand = new Command(async () => await BackupCommandExecute());
        }

        public ICommand BackupCommand { get; set; }

        public async Task BackupCommandExecute()
        {
            var walletHandler = await walletService.GetCurrentWallet();
            var fileName = $"coinbag_backup_{walletHandler.Name}_{DateTime.Now:yyyyMMddhhmmss}";
            using (var memStream = new MemoryStream())
            using (var writer = new BinaryWriter(memStream))
            {
                var serializedWallet = JsonConvert.SerializeObject(walletHandler);
                writer.Write(serializedWallet);
                await fileService.SaveToBackupFolder(fileName, memStream.ToArray());
            }

            notificationService.ShowInfo("Wallet was backed up successfully");
        }
    }
}
