using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.Helpers;

namespace CoinBag.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

    }
}
