using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;

namespace CoinBag.Services
{
    public class ChainService : IChainService
    {
        private IPersistenceService persistenceService;

        public ChainService(IPersistenceService persistenceService)
        {
            this.persistenceService = persistenceService;
        }

        public async Task<ConcurrentChain> LoadChain()
        {
            var chain = new ConcurrentChain(Constants.CurrentNetwork);
             await persistenceService.LoadFromStream(Constants.ChainFile, stream =>
            {
                chain.Load(stream);
                return chain;
            });

            return chain;
        }

        public async Task SaveChain(ConcurrentChain chain)
        {
            await persistenceService.SaveFromStream(Constants.ChainFile, chain.WriteTo);
        }
    }
}
