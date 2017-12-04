using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Services;
using Xamarin.Forms;
using ZXing;
using ZXing.Common;

namespace CoinBag.ViewModels
{
    public class ReceiveCoinViewModel : ViewModelBase
    {
        private IWalletService walletService;

        public ReceiveCoinViewModel(IWalletService walletService)
        {
            this.walletService = walletService;
            Title = "Get coin";
        }

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
    }
}
