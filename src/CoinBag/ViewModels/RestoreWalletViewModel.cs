using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoinBag.ViewModels
{
    public class RestoreWalletViewModel : ViewModelBase
    {
        public RestoreWalletViewModel()
        {
            Title = "Restore Wallet";
            BrowseFileCommand = new Command(BrowseFileCommandExecute);
        }

        public ICommand BrowseFileCommand { get; set; }

        private void BrowseFileCommandExecute()
        {
            
        }
    }
}
