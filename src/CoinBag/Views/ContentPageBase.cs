using System;
using System.Collections.Generic;
using System.Text;
using CoinBag.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace CoinBag.Views
{
    public abstract class ContentPageBase<T> : ContentPage where T : ViewModelBase
    {
        protected ContentPageBase()
        {
            ViewModel = DI.ServiceProvider.GetService<T>();
            BindingContext = ViewModel;
            Title = ViewModel.Title;
        }

        public T ViewModel { get; set; }
    }
}
