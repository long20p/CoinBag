using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Models;
using CoinBag.Services;

namespace CoinBag.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
	    private ISettingService settingService;
	    private IWalletService walletService;

        public SplashViewModel(IWalletService walletService, ISettingService settingService)
        {
	        this.walletService = walletService;
	        this.settingService = settingService;
	        AppName = "CoinBag";
        }

        private string appName;
        public string AppName
        {
            get { return appName; }
            set { SetProperty(ref appName, value); }
        }

        public async Task<Wallet> LoadWallet()
        {
            return await walletService.GetCurrentWallet();
        }

	    public async Task<Wallet> CreateNewWallet(bool makeDefault = false)
	    {
		    var wallet = walletService.CreateNew();
		    await walletService.SaveWallet(wallet);
		    if (makeDefault)
		    {
				var setting = await settingService.GetSetting();
			    setting.CurrentWalletId = wallet.Id;
			    await settingService.SaveSetting(setting);
		    }
		    return wallet;
	    }
    }
}
