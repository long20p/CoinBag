using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Models;
using NBitcoin;

namespace CoinBag.Services
{
    public interface IWalletService
    {
	    WalletHandler CreateNew(string name, string passphrase = null);
	    Task<WalletHandler> GetWallet(Guid walletId);
        Task<WalletHandler> GetCurrentWallet();
        Task<BitcoinAddress> GetUnusedAddress();

        Task SaveWallet(WalletHandler walletHandler, bool makeDefault = false);
        Task<string> GetNextUnusedAddress(WalletHandler walletHandler);
    }
}
