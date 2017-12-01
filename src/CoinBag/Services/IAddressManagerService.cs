using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NBitcoin.Protocol;

namespace CoinBag.Services
{
    public interface IAddressManagerService
    {
        AddressManager LoadAddressManager();
        void SaveAddressManager(AddressManager addrManager);
    }
}
