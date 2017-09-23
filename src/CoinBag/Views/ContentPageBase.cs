using System;
using System.Collections.Generic;
using System.Text;
using CoinBag.ViewModels;
using Xamarin.Forms;

namespace CoinBag.Views
{
    public abstract class ContentPageBase<T> : ContentPage where T : ViewModelBase
    {
        protected ContentPageBase()
        {
            BindingContext = DependencyService.Get<T>();
        }
    }
}
