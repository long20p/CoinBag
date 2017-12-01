using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Helpers;
using CoinBag.Models;

namespace CoinBag.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Title = "Coin bag";

			RecentTransactions = new ObservableRangeCollection<TransactionHistory>();
	        RecentTransactions.Add(new TransactionHistory
	        {
		        Amount = 0.0754M,
		        Direction = TransactionDirection.In,
		        Address = new Address {PublicAddress = "1A067jh43f43rtedg55t3"}
	        });
	        RecentTransactions.Add(new TransactionHistory
	        {
		        Amount = 1.52M,
		        Direction = TransactionDirection.Out,
		        Address = new Address { PublicAddress = "1fdg5hj574g296gdrfv2" }
	        });
		}

		private WalletHandler _currentWalletHandler;
		public WalletHandler CurrentWalletHandler
		{
			get { return _currentWalletHandler; }
			set
			{
				SetProperty(ref _currentWalletHandler, value);

			}
		}

		private decimal totalBalance;
		public decimal TotalBalance
		{
			get { return totalBalance; }
			set { SetProperty(ref totalBalance, value); }
		}

	    public ObservableRangeCollection<TransactionHistory> RecentTransactions { get; set; }
	}
}
