using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NBitcoin.SPV;

namespace CoinBag.Services
{
    public class OperationTrackerService : IOperationTrackerService
    {
        private IPersistenceService persistenceService;

        public OperationTrackerService(IPersistenceService persistenceService)
        {
            this.persistenceService = persistenceService;
        }

        public async Task<Tracker> LoadTracker()
        {
            try
            {
                return await persistenceService.LoadFromStream(Constants.TrackerFile, Tracker.Load) ?? new Tracker();
            }
            catch (Exception e)
            {
                return new Tracker();
            }
        }

        public async Task SaveTracker(Tracker tracker)
        {
            await persistenceService.SaveFromStream(Constants.TrackerFile, tracker.Save);
        }
    }
}
