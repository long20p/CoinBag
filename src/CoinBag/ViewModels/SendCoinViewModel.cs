using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoinBag.Models;
using CoinBag.Services;
using NBitcoin;
using Xamarin.Forms;
using ZXing.Mobile;

namespace CoinBag.ViewModels
{
    public class SendCoinViewModel : ViewModelBase
    {
	    private IWalletService walletService;
        private ICoinSelector coinSelector;

        public SendCoinViewModel(IWalletService walletService, ICoinSelector coinSelector)
        {
	        this.walletService = walletService;
            this.coinSelector = coinSelector;
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
            //var trans = currentWallet.Wallet.GetTransactions();
            var receivedCoins = currentWallet.Wallet.GetTransactions().SelectMany(x => x.ReceivedCoins);
            var knownScriptPubKeys = currentWallet.Wallet.GetKnownScripts(true);

	        var transaction = new Transaction();
	        var targetAmount = new Money(Amount, MoneyUnit.BTC);
            var feeToInclude = new Money(Fee, MoneyUnit.BTC);

            try
	        {
                var privKeys = new List<Key>();
	            var masterKey = currentWallet.MasterPrivKey.ExtKey;
	            var inputCoins = coinSelector.Select(receivedCoins, targetAmount + feeToInclude).ToList();
	            //Input
	            foreach (var inputCoin in inputCoins)
	            {
	                var txIn = new TxIn
	                {
	                    PrevOut = inputCoin.Outpoint,
	                    ScriptSig = inputCoin.TxOut.ScriptPubKey
	                };
	                transaction.AddInput(txIn);
                    //Get private key for current pub key
	                var keyPath = knownScriptPubKeys.First(x => x.Key == inputCoin.TxOut.ScriptPubKey).Value;
	                privKeys.Add(masterKey.Derive(keyPath).PrivateKey);
	            }

                //Output
	            var receivingAddress = BitcoinAddress.Create(DestinationAddress);
	            transaction.AddOutput(targetAmount, receivingAddress);
	            IMoney seed = new Money(0);
	            var inputAmount = inputCoins.Aggregate(seed, (sum, coin) => sum.Add(coin.Amount)) as Money;
	            var changeAmount = inputAmount - targetAmount - feeToInclude;
	            if (changeAmount > 0)
	            {
	                var changeAddress = await walletService.GetUnusedAddress();
	                transaction.AddOutput(changeAmount, changeAddress);
	            }

                //Sign
	            for (int i = 0; i < inputCoins.Count; i++)
	            {
	                transaction.Sign(privKeys[i], inputCoins[i]);
	            }

                //Broadcast
	            await currentWallet.Wallet.BroadcastTransactionAsync(transaction);

                MessagingCenter.Send(this, Messages.TransactionSucceeded);
	        }
	        catch (Exception e)
	        {
	            Console.WriteLine(e);
	            MessagingCenter.Send(this, Messages.TransactionFailed, e.Message);
            }
	    }

        private IEnumerable<ICoin> GetCoinsForKey(Script scriptPubKey, IEnumerable<ICoin> coins)
        {
            var outpoints = new List<ICoin>();
            foreach (var coin in coins)
            {
                if(coin.TxOut.ScriptPubKey == scriptPubKey)
                    outpoints.Add(coin);
            }

            return outpoints;
        }
	}
}
