using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoinBag.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReceiveCoinView : ReceiveCoinViewPage
    {
		public ReceiveCoinView ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.GetUnusedAddress();
        }
    }

    public class ReceiveCoinViewPage : ContentPageBase<ReceiveCoinViewModel> { }
}
