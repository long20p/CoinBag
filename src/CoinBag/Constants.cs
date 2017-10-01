using System;
using System.Collections.Generic;
using System.Text;
using CoinBag.Models;

namespace CoinBag
{
    public static class Constants
    {
		//Files
	    public const string SettingFile = "Settings.json";

		//Folders
	    public const string WalletFolder = "Wallet";

		//Settings
	    public const NetworkType SupportedNetworkType = NetworkType.TestNet;

        //Pages
        public const string MainPage = "Overview";
        public const string BackupWalletPage = "Backup Wallet";
        public const string RestoreWalletPage = "Restore Wallet";
        public const string SendCoinPage = "Send Coin";
        public const string GetCoinPage = "Get Coin";
    }
}
