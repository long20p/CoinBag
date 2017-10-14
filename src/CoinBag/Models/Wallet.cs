using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBag.Models
{
    public class Wallet : IdentifiableEntity
    {
	    public string Name { get; set; }
	    public DateTime Created { get; set; }
		public string EncodedMnemonic { get; set; }
	    public Address Master { get; set; }
	    public List<Address> CurrentAddresses { get; set; } = new List<Address>();
    }
}
