using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin;

namespace CoinBag.Models
{
    public class TransactionHistory : IdentifiableEntity
    {
        public Address Address { get; set; }
        public DateTime TransactionTime { get; set; }
	    public string TransactionId { get; set; }
	    public TransactionDirection Direction { get; set; }
    }
}
