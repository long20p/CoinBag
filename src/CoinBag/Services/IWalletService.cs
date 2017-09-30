using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Models;

namespace CoinBag.Services
{
    public interface IWalletService
    {
	    Wallet CreateNew(string passphrase = null);
	    Task<Wallet> GetWallet(Guid walletId);
	    Task SaveWallet(Wallet wallet);
    }
}
