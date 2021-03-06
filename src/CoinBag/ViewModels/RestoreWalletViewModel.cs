using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Adapt.Presentation;
using CoinBag.Models;
using CoinBag.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CoinBag.ViewModels
{
    public class RestoreWalletViewModel : ViewModelBase
    {
        private ISettingService settingService;
        private IWalletService walletService;
        private INotificationService notificationService;

        public RestoreWalletViewModel(IWalletService walletService, ISettingService settingService, INotificationService notificationService)
        {
            this.walletService = walletService;
            this.settingService = settingService;
            this.notificationService = notificationService;
            Title = "Restore Wallet";
            BrowseFileCommand = new Command(async ()=> await BrowseFileCommandExecute());
            RestoreWalletCommand = new Command(async ()=> await RestoreWalletCommandExecute());
        }

        public ICommand BrowseFileCommand { get; set; }
        public ICommand RestoreWalletCommand { get; set; }

        private string selectedFilePath;
        public string SelectedFilePath
        {
            get { return selectedFilePath; }
            set { SetProperty(ref selectedFilePath, value); }
        }

        public byte[] SelectedFile { get; private set; }

        private async Task BrowseFileCommandExecute()
        {
            var filePicker = App.PresentationFactory.CreateFilePicker();
            using (var fileData = await filePicker.PickAndOpenFileForReading())
            {
                if (fileData?.FileStream is FileStream)
                {
                    SelectedFilePath = (fileData.FileStream as FileStream).Name;
                    SelectedFile = new byte[fileData.FileStream.Length];
                    fileData.FileStream.Read(SelectedFile, 0, (int)fileData.FileStream.Length);
                }
            }
        }

        private async Task RestoreWalletCommandExecute()
        {
            if (SelectedFile == null)
            {
                notificationService.ShowInfo("No file selected");
                return;
            }

            using (var memStream = new MemoryStream(SelectedFile))
            using(var reader = new BinaryReader(memStream))
            {
                var raw = reader.ReadString();
                var wallet = JsonConvert.DeserializeObject<WalletHandler>(raw);
                await walletService.SaveWallet(wallet);
                var setting = await settingService.GetSetting();
                setting.CurrentWalletId = wallet.Id;
                await settingService.SaveSetting(setting);
            }

            notificationService.ShowInfo("Wallet was restored successfully");
        }
    }
}
