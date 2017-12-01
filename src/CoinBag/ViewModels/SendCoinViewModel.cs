using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoinBag.Services;
using NBitcoin;
using Xamarin.Forms;
using ZXing.Mobile;

namespace CoinBag.ViewModels
{
    public class SendCoinViewModel : ViewModelBase
    {
	    private IWalletService walletService;

        public SendCoinViewModel(IWalletService walletService)
        {
	        this.walletService = walletService;
	        Title = "Send Coin";
            ScanAddressCommand = new Command(async () => await ScanAddressCommandExecute());
            SendCommand = new Command(async () => await SendCommandExecute());
        }

		public ICommand SendCommand { get; set; }
        public ICommand ScanAddressCommand { get; set; }


        private string destAddress;
		public string DestinationAddress
		{
			get { return destAddress; }
			set { SetProperty(ref destAddress, value); }
		}

		private decimal amount;
		public decimal Amount
		{
			get { return amount; }
			set { SetProperty(ref amount, value); }
		}

		private decimal fee;
		public decimal Fee
		{
			get { return fee; }
			set { SetProperty(ref fee, value); }
		}

        private async Task ScanAddressCommandExecute()
        {
            var scanner = new MobileBarcodeScanner();
            var result = await scanner.Scan();
            if (result != null)
            {
                DestinationAddress = result.Text;
            }
        }

        private async Task SendCommandExecute()
	    {
		    var currentWallet = await walletService.GetCurrentWallet();
		    var transaction = new Transaction();
			

	    }
	}
}
