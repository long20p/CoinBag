using System;
using System.Collections.Generic;
using System.Text;
using CoinBag.Helpers;
using CoinBag.Models;
using NBitcoin;

namespace CoinBag
{
    public static class Constants
    {
		//Files
	    public const string SettingFile = "Settings.json";
        public const string WalletHandlerFile = "handler.dat";
        public const string WalletFile = "wallet.dat";
        public const string AddressManagerFile = "addrman.dat";
        public const string ChainFile = "chain.dat";
        public const string TrackerFile = "tracker.dat";

		//Folders
	    public const string WalletFolder = "Wallet";

		//Settings
	    public const NetworkType SupportedNetworkType = NetworkType.TestNet;

        public static Network CurrentNetwork => SupportedNetworkType.GetNetwork();

        //Pages
        public const string MainPage = "Overview";
        public const string BackupWalletPage = "Backup Wallet";
        public const string RestoreWalletPage = "Restore Wallet";
        public const string SendCoinPage = "Send Coin";
        public const string GetCoinPage = "Get Coin";
    }
}
