using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adapt.Presentation;
using CoinBag.Models;
using CoinBag.Views;
using NBitcoin.Protocol;
using Xamarin.Forms;

namespace CoinBag
{
    public partial class App : Application
    {
        public static readonly object SaveLock = new object();

        public App(GlobalSetup setup, IPresentationFactory presentationFactory)
        {
            InitializeComponent();
            PresentationFactory = presentationFactory;
            DI.ServiceProvider = setup.CreateServiceProvider();
            RootPage = new RootPage();
            MainPage = RootPage;
        }

        public static IPresentationFactory PresentationFactory { get; private set; }

        public static NodesGroup NodesGroup { get; set; }

        public static RootPage RootPage { get; private set; }

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
