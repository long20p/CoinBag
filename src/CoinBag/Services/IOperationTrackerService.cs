using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NBitcoin.SPV;

namespace CoinBag.Services
{
    public interface IOperationTrackerService
    {
        Task<Tracker> LoadTracker();
        Task SaveTracker(Tracker tracker);
    }
}
