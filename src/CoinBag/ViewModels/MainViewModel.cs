using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Helpers;
using CoinBag.Models;
using CoinBag.Services;
using NBitcoin.Protocol.Behaviors;
using NBitcoin.SPV;

namespace CoinBag.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IAddressManagerService addressManagerService;
        private IChainService chainService;
        private IOperationTrackerService trackerService;
        private IWalletService walletService;

        public MainViewModel(IAddressManagerService addressManagerService, IChainService chainService, IOperationTrackerService trackerService, IWalletService walletService)
        {
            this.addressManagerService = addressManagerService;
            this.chainService = chainService;
            this.trackerService = trackerService;
            this.walletService = walletService;

            Title = "Coin bag";

			RecentTransactions = new ObservableRangeCollection<TransactionDetail>();
		}

		private WalletHandler _currentWalletHandler;
		public WalletHandler CurrentWalletHandler
		{
			get { return _currentWalletHandler; }
			set
			{
				SetProperty(ref _currentWalletHandler, value);
			}
		}

        private decimal totalBalance;
        public decimal TotalBalance
        {
            get { return totalBalance; }
            set { SetProperty(ref totalBalance, value); }
        }

        public ObservableRangeCollection<TransactionDetail> RecentTransactions { get; set; }

        public void UpdateTransactions()
        {
            var trans = CurrentWalletHandler.Wallet.GetTransactions().Select(tx => new TransactionDetail(tx)).ToList();
            var newTrans = trans.Except(RecentTransactions).ToList();
            if (newTrans.Any())
            {
                RecentTransactions.AddRange(newTrans);
            }
        }

        public void PeriodicRefresh()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(2000);
                    UpdateTransactions();
                }
            });
        }

        public void PeriodicSave()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(60000);
                    lock (App.SaveLock)
                    {
                        var templateBehaviors = CurrentWalletHandler.Wallet.Group.NodeConnectionParameters.TemplateBehaviors;
                        addressManagerService.SaveAddressManager(templateBehaviors.Find<AddressManagerBehavior>().AddressManager);
                        chainService.SaveChain(templateBehaviors.Find<ChainBehavior>().Chain);
                        trackerService.SaveTracker(templateBehaviors.Find<TrackerBehavior>().Tracker);
                        walletService.SaveWallet(CurrentWalletHandler);
                    }
                }
            });
        }
    }
}
