using System;
using System.Collections.Generic;
using System.Text;
using CoinBag.Models;
using NBitcoin;

namespace CoinBag.Helpers
{
    public static class BitcoinExtensions
    {
	    public static Network GetNetwork(this NetworkType networkType)
	    {
		    switch (networkType)
		    {
				case NetworkType.RegTest:
					return Network.RegTest;
				case NetworkType.TestNet:
					return Network.TestNet;
				case NetworkType.Main:
					return Network.Main;
				default:
					throw new InvalidOperationException($"{networkType} is not a supported type");
		    }
	    }
    }
}
