using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
	    private Wallet currentWallet;

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
			if (currentWallet == null)
			{
				var setting = await settingService.GetSetting();
				if (setting?.CurrentWalletId == null)
				{
					return null;
				}
				currentWallet = await GetWallet(setting.CurrentWalletId.Value);
			}
			return currentWallet;
        }

        public async Task SaveWallet(Wallet wallet, bool makeDefault = false)
	    {
		    await persistenceService.SaveObject(wallet, Path.Combine(Constants.WalletFolder, wallet.Id.ToString()));
		    if (makeDefault)
		    {
			    currentWallet = wallet;
		    }
	    }

        public async Task<string> GetNextUnusedAddress(Wallet wallet)
        {
            if(wallet.CurrentAddresses == null)
                wallet.CurrentAddresses = new List<Address>();

            var unusedAddress = wallet.CurrentAddresses.FirstOrDefault(x => !x.HasHistory);
            if (unusedAddress == null)
            {
                var masterKey = ExtKey.Parse(wallet.Master.ExtPrivateKeyWif);
                var currentIndex = wallet.CurrentAddresses.Count();
                var derivedKey = masterKey.Derive(currentIndex, true);
                unusedAddress = new Address
                {
                    ExtPrivateKeyWif = derivedKey.ToString(Constants.SupportedNetworkType.GetNetwork()),
                    PublicAddress = derivedKey.Neuter().ToString(Constants.SupportedNetworkType.GetNetwork()),
                    HDPath = ""
                };
                wallet.CurrentAddresses.Add(unusedAddress);
                await SaveWallet(wallet);
            }
            return unusedAddress.PublicAddress;
        }
    }
}
