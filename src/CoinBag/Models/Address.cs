using System;
using System.Collections.Generic;
using System.Text;

namespace CoinBag.Models
{
    public class Address : IdentifiableEntity
    {
        public string ExtPrivateKeyWif { get; set; }
		public string PublicAddress { get; set; }
        public string HDPath { get; set; }
        public bool HasHistory { get; set; }
    }
}
