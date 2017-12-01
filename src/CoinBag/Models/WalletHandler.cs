using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;
using NBitcoin.SPV;

namespace CoinBag.Models
{
    public class WalletHandler : IdentifiableEntity
    {
	    public string Name { get; set; }
	    public DateTime Created { get; set; }
		public string EncodedMnemonic { get; set; }
        public string MasterPrivKeyBase58 { get; set; }

        [IgnoreDataMember]
	    public Wallet Wallet { get; set; }

        private BitcoinExtKey masterPrivKey;
        [IgnoreDataMember]
        public BitcoinExtKey MasterPrivKey
        {
            get
            {
                if(masterPrivKey == null)
                    masterPrivKey = new BitcoinExtKey(MasterPrivKeyBase58);
                return masterPrivKey;
            }
        }
    }
}
