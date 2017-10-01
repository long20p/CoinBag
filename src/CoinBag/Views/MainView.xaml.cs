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
    public partial class MainView : MainViewPage
    {
        public MainView()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<SplashView, Wallet>(this, Messages.InitializationCompleted, async (sender, wallet) =>
            {
                ViewModel.CurrentWallet = wallet;
                await Navigation.PopModalAsync();
                MessagingCenter.Unsubscribe<SplashView, Wallet>(this, Messages.InitializationCompleted);
            });
            Navigation.PushModalAsync(new SplashView());
        }
    }

    public class MainViewPage : ContentPageBase<MainViewModel> { }
}
