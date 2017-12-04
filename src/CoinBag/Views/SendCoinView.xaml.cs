using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Models;
using CoinBag.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoinBag.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SendCoinView : SendCoinViewPage
	{
		public SendCoinView()
		{
			InitializeComponent ();
		    MessagingCenter.Subscribe<SendCoinViewModel, string>(this, Messages.TransactionFailed, async (_, error) =>
		    {
		        await DisplayAlert("Transaction failed", $"Cannot complete transaction. Error: {error}", "OK");
		    });

            MessagingCenter.Subscribe<SendCoinViewModel>(this, Messages.TransactionSucceeded, async _ =>
            {
                await DisplayAlert("Transaction sent", "Transaction sent successfully", "OK");
            });
		}
	}

    public class SendCoinViewPage : ContentPageBase<SendCoinViewModel> { }
}
