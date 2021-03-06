using NBitcoin.DataEncoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBitcoin
{
	/// <summary>
	/// Base58 representation of a pubkey hash and base class for the representation of a script hash
	/// </summary>
	public class BitcoinPubKeyAddress : BitcoinAddress, IBase58Data
	{
		public BitcoinPubKeyAddress(string base58, Network expectedNetwork = null)
			: base(Validate(base58, ref expectedNetwork), expectedNetwork)
		{
			var decoded = Encoders.Base58Check.DecodeData(base58);
			_KeyId = new KeyId(new uint160(decoded.Skip(expectedNetwork.GetVersionBytes(Base58Type.PUBKEY_ADDRESS, true).Length).ToArray()));
		}

		private static string Validate(string base58, ref Network expectedNetwork)
		{
			if(base58 == null)
				throw new ArgumentNullException("base58");
			var networks = expectedNetwork == null ? Network.GetNetworks() : new[] { expectedNetwork };
			var data = Encoders.Base58Check.DecodeData(base58);
			foreach(var network in networks)
			{
				var versionBytes = network.GetVersionBytes(Base58Type.PUBKEY_ADDRESS, false);
				if(versionBytes != null && data.StartWith(versionBytes))
				{
					if(data.Length == versionBytes.Length + 20)
					{
						expectedNetwork = network;
						return base58;
					}
				}
			}
			throw new FormatException("Invalid BitcoinPubKeyAddress");
		}

		

		public BitcoinPubKeyAddress(KeyId keyId, Network network) :
			base(NotNull(keyId) ?? Network.CreateBase58(Base58Type.PUBKEY_ADDRESS, keyId.ToBytes(), network), network)
		{
			_KeyId = keyId;
		}

		private static string NotNull(KeyId keyId)
		{
			if(keyId == null)
				throw new ArgumentNullException("keyId");
			return null;
		}

		public bool VerifyMessage(string message, string signature)
		{
			var key = PubKey.RecoverFromMessage(message, signature);
			return key.Hash == Hash;
		}

		KeyId _KeyId;
		public KeyId Hash
		{
			get
			{
				return _KeyId;
			}
		}


		public Base58Type Type
		{
			get
			{
				return Base58Type.PUBKEY_ADDRESS;
			}
		}

		protected override Script GeneratePaymentScript()
		{
			return PayToPubkeyHashTemplate.Instance.GenerateScriptPubKey((KeyId)this.Hash);
		}
	}
}
