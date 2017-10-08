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

        public BackupWalletViewModel(IWalletService walletService, IFileService fileService)
        {
            this.walletService = walletService;
            this.fileService = fileService;
            Title = "Backup Wallet";
            BackupCommand = new Command(async () => await BackupCommandExecute());
        }

        public ICommand BackupCommand { get; set; }

        public async Task BackupCommandExecute()
        {
            var wallet = await walletService.GetCurrentWallet();
            var fileName = $"coinbag_backup_{wallet.Name}_{DateTime.Now:yyyyMMddhhmmss}";
            using (var memStream = new MemoryStream())
            using (var writer = new BinaryWriter(memStream))
            {
                writer.Write(JsonConvert.SerializeObject(wallet));
                await fileService.SaveToDownloads(fileName, memStream.ToArray());
            }

            //TODO: notification
        }
    }
}
