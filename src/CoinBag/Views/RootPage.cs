﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoinBag.Views
{
    public class RootPage : MasterDetailPage
    {
        Dictionary<string, NavigationPage> Pages { get; set; }

        public RootPage()
        {
            Pages = new Dictionary<string, NavigationPage>();
            Master = new MenuView(this);
            NavigateTo(Constants.MainPage);
        }

        public void NavigateTo(string pageName)
        {
            try
            {
                if (!Pages.ContainsKey(pageName))
                {
                    switch (pageName)
                    {
                        case Constants.MainPage:
                            Pages.Add(pageName, new NavigationPage(new MainView()));
                            break;
                        case Constants.BackupWalletPage:
                            Pages.Add(pageName, new NavigationPage(new BackupWalletView()));
                            break;
                    }
                }

                Detail = Pages[pageName];
                IsPresented = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}