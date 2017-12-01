using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;

namespace CoinBag.Services
{
    public interface IChainService
    {
        Task<ConcurrentChain> LoadChain();
        Task SaveChain(ConcurrentChain chain);
    }
}
