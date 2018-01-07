using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoinBag.Services;
using Xamarin.Forms;
using ZXing;
using ZXing.Common;

namespace CoinBag.ViewModels
{
    public class ReceiveCoinViewModel : ViewModelBase
    {
        private IWalletService walletService;
        private IClipboardService clipboardService;
        private INotificationService notificationService;

        public ReceiveCoinViewModel(IWalletService walletService, IClipboardService clipboardService, INotificationService notificationService)
        {
            this.walletService = walletService;
            this.clipboardService = clipboardService;
            this.notificationService = notificationService;
            Title = "Get coin";
            CopyAddressCommand = new Command(CopyAddressCommandExecute);
        }

        public ICommand CopyAddressCommand { get; set; }

        private string receivingAddress;
        public string ReceivingAddress
        {
            get { return receivingAddress; }
            set { SetProperty(ref receivingAddress, value); }
        }

        public async Task GetUnusedAddress()
        {
            var bitcoinAddr = await walletService.GetUnusedAddress();
            ReceivingAddress = bitcoinAddr?.ToString();
        }

        private void CopyAddressCommandExecute()
        {
            clipboardService.CopyToClipboard(ReceivingAddress);
            notificationService.ShowInfo("Address copied to clipboard");
        }
    }
}
