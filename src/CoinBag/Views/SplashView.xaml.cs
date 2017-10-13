using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Models;
using CoinBag.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoinBag.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashView : SplashViewPage
	{
		public SplashView()
		{
			InitializeComponent();
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        await Initialize();
	    }

	    private async Task Initialize()
	    {
            // Load wallet
            var currentWallet = await ViewModel.LoadWallet();
		    if (currentWallet == null)
		    {
			    await DisplayAlert("Creating new wallet", "No default wallet found. A new wallet will be created.", "OK");
			    currentWallet = await ViewModel.CreateNewWallet(true);
			    await DisplayAlert("New wallet created",
				    $"Please write down the mnemonic of your new wallet:{Environment.NewLine}{currentWallet.EncodedMnemonic}", "OK");
		    }

            MessagingCenter.Send(this, Messages.InitializationCompleted, currentWallet);
	    }
	}

    public class SplashViewPage : ContentPageBase<SplashViewModel> { }
}
