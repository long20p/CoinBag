using System;
using System.Collections.Generic;
using System.Text;

namespace CoinBag.Models
{
    public abstract class IdentifiableEntity
    {
	    public Guid Id { get; set; }
    }
}
