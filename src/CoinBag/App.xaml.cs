using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoinBag.Views;
using Xamarin.Forms;

namespace CoinBag
{
    public partial class App : Application
    {
        public App(GlobalSetup setup)
        {
            InitializeComponent();
            DI.ServiceProvider = setup.CreateServiceProvider();
            MainPage = new NavigationPage(new MainView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
