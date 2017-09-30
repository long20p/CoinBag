using System;
using System.Collections.Generic;
using System.Text;

namespace CoinBag.Models
{
    public class Address : IdentifiableEntity
    {
        public string ExtPrivateKeyWif { get; set; }
        public string HDPath { get; set; }
    }
}
