using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Helpers;
using CoinBag.Models;
using NBitcoin;

namespace CoinBag.Services
{
    public class WalletService : IWalletService
    {
	    private readonly IPersistenceService persistenceService;
        private readonly ISettingService settingService;

	    public WalletService(IPersistenceService persistenceService, ISettingService settingService)
	    {
	        this.persistenceService = persistenceService;
	        this.settingService = settingService;
	    }

	    public Wallet CreateNew(string passphrase = null)
	    {
		    var mnemonic = new Mnemonic(Wordlist.English, WordCount.TwentyFour);
		    var key = mnemonic.DeriveExtKey(passphrase);
	        var master = new Address
	        {
	            ExtPrivateKeyWif = key.ToString(Constants.SupportedNetworkType.GetNetwork()),
				PublicAddress = key.PrivateKey.PubKey.GetAddress(Constants.SupportedNetworkType.GetNetwork()).ToString()
	        };

	        return new Wallet
	        {
				Id =Guid.NewGuid(),
	            Name = "Default",
	            Created = DateTime.UtcNow,
                EncodedMnemonic = mnemonic.ToString(),
	            Master = master
            };
        }

	    public async Task<Wallet> GetWallet(Guid walletId)
	    {
		    return await persistenceService.LoadObject<Wallet>(Path.Combine(Constants.WalletFolder, walletId.ToString()));
	    }

        public async Task<Wallet> GetCurrentWallet()
        {
            var setting = await settingService.GetSetting();
            if (setting?.CurrentWalletId == null)
            {
                return null;
            }
            return await GetWallet(setting.CurrentWalletId.Value);
        }

        public async Task SaveWallet(Wallet wallet)
	    {
		    await persistenceService.SaveObject(wallet, Path.Combine(Constants.WalletFolder, wallet.Id.ToString()));
	    }
    }
}
