using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Models;
using CoinBag.Services;
using NBitcoin;
using NBitcoin.Protocol;
using NBitcoin.Protocol.Behaviors;
using NBitcoin.SPV;

namespace CoinBag.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
	    private ISettingService settingService;
	    private IWalletService walletService;
        private IAddressManagerService addressManagerService;
        private IChainService chainService;
        private IOperationTrackerService trackerService;

        public SplashViewModel(IWalletService walletService, ISettingService settingService,
            IAddressManagerService addressManagerService, IChainService chainService,
            IOperationTrackerService trackerService)
        {
            this.walletService = walletService;
            this.settingService = settingService;
            this.addressManagerService = addressManagerService;
            this.chainService = chainService;
            this.trackerService = trackerService;
            AppName = "CoinBag";
        }

        private string appName;
        public string AppName
        {
            get { return appName; }
            set { SetProperty(ref appName, value); }
        }

        public async Task InitializeConnection(Wallet currentWallet)
        {
            await Task.Factory.StartNew(() =>
            {
                var parameters = new NodeConnectionParameters();
                parameters.TemplateBehaviors.Add(new AddressManagerBehavior(addressManagerService.LoadAddressManager()));
                parameters.TemplateBehaviors.Add(new ChainBehavior(chainService.LoadChain().Result));
                parameters.TemplateBehaviors.Add(new TrackerBehavior(trackerService.LoadTracker().Result));

                var group = new NodesGroup(Constants.CurrentNetwork, parameters,
                    new NodeRequirement {RequiredServices = NodeServices.Network})
                {
                    MaximumNodeConnection = 5
                };
                group.Connect();

                currentWallet.Configure(group);
                currentWallet.Connect();
            });
        }

        public async Task<WalletHandler> LoadWallet()
        {
            return await walletService.GetCurrentWallet();
        }

	    public async Task<WalletHandler> CreateNewWallet(bool makeDefault = false)
	    {
		    var walletHandler = walletService.CreateNew("Default");
		    await walletService.SaveWallet(walletHandler);
		    if (makeDefault)
		    {
				var setting = await settingService.GetSetting();
			    setting.CurrentWalletId = walletHandler.Id;
			    await settingService.SaveSetting(setting);
		    }
		    return walletHandler;
	    }
    }
}
