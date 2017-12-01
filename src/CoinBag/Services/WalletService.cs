using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Helpers;
using CoinBag.Models;
using NBitcoin;
using NBitcoin.SPV;

namespace CoinBag.Services
{
    public class WalletService : IWalletService
    {
	    private readonly IPersistenceService persistenceService;
        private readonly ISettingService settingService;
	    private WalletHandler _currentWalletHandler;

	    public WalletService(IPersistenceService persistenceService, ISettingService settingService)
	    {
	        this.persistenceService = persistenceService;
	        this.settingService = settingService;
	    }

	    public WalletHandler CreateNew(string name, string passphrase = null)
	    {
	        var walletId = Guid.NewGuid();
		    var mnemonic = new Mnemonic(Wordlist.English, WordCount.TwentyFour);
		    var key = mnemonic.DeriveExtKey(passphrase);
	        var privKey = key.GetWif(Constants.CurrentNetwork);
	        var pubKey = key.Neuter().GetWif(Constants.CurrentNetwork);
	        var walletCreation = new WalletCreation
	        {
                Name = walletId.ToString(),
	            Network = Constants.CurrentNetwork,
	            RootKeys = new[] {pubKey.ExtPubKey}
	        };

	        return new WalletHandler
	        {
				Id = walletId,
	            Name = name,
	            Created = DateTime.UtcNow,
                EncodedMnemonic = mnemonic.ToString(),
                Wallet = new Wallet(walletCreation),
                MasterPrivKeyBase58 = privKey.ToString()
            };
        }

	    public async Task<WalletHandler> GetWallet(Guid walletId)
	    {
		    var walletHandler = await persistenceService.LoadObject<WalletHandler>(Path.Combine(Constants.WalletFolder, walletId.ToString(), Constants.WalletHandlerFile));
	        var wallet = await persistenceService.LoadFromStream(
	            Path.Combine(Constants.WalletFolder, walletId.ToString(), Constants.WalletFile), Wallet.Load);
	        walletHandler.Wallet = wallet;
	        return walletHandler;
	    }

        public async Task<WalletHandler> GetCurrentWallet()
        {
			if (_currentWalletHandler == null)
			{
				var setting = await settingService.GetSetting();
				if (setting?.CurrentWalletId == null)
				{
					return null;
				}
				_currentWalletHandler = await GetWallet(setting.CurrentWalletId.Value);
			}
			return _currentWalletHandler;
        }

        public async Task SaveWallet(WalletHandler walletHandler, bool makeDefault = false)
	    {
		    await persistenceService.SaveObject(walletHandler, Path.Combine(Constants.WalletFolder, walletHandler.Id.ToString(), Constants.WalletHandlerFile));
	        await persistenceService.SaveFromStream(
	            Path.Combine(Constants.WalletFolder, walletHandler.Id.ToString(), Constants.WalletFile),
	            walletHandler.Wallet.Save);
            if (makeDefault)
		    {
			    _currentWalletHandler = walletHandler;
		    }
	    }

        public async Task<string> GetNextUnusedAddress(WalletHandler walletHandler)
        {
            //if(walletHandler.CurrentAddresses == null)
            //    walletHandler.CurrentAddresses = new List<Address>();

            //var unusedAddress = walletHandler.CurrentAddresses.FirstOrDefault(x => !x.HasHistory);
            //if (unusedAddress == null)
            //{
            //    var masterKey = ExtKey.Parse(walletHandler.Master.ExtPrivateKeyWif);
            //    var currentIndex = walletHandler.CurrentAddresses.Count();
            //    var derivedKey = masterKey.Derive(currentIndex, true);
            //    unusedAddress = new Address
            //    {
            //        ExtPrivateKeyWif = derivedKey.ToString(Constants.SupportedNetworkType.GetNetwork()),
            //        PublicAddress = derivedKey.Neuter().PubKey.ToString(Constants.SupportedNetworkType.GetNetwork()),
            //        HDPath = ""
            //    };
            //    walletHandler.CurrentAddresses.Add(unusedAddress);
            //    await SaveWallet(walletHandler);
            //}
            //return unusedAddress.PublicAddress;
            return "";
        }
    }
}
