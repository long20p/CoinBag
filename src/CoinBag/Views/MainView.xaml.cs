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
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<SplashView>(this, Messages.InitializationCompleted, async (sender) =>
            {
                await Navigation.PopModalAsync();
            });
            await Navigation.PushModalAsync(new SplashView());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SplashView>(this, Messages.InitializationCompleted);
        }
    }

    public class MainViewPage : ContentPageBase<MainViewModel> { }
}
