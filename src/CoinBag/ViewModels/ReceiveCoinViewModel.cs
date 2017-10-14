using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Services;

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
            var wallet = await walletService.GetCurrentWallet();
            ReceivingAddress = await walletService.GetNextUnusedAddress(wallet);
        }
    }
}
