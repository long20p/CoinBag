using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adapt.Presentation;
using CoinBag.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoinBag.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BackupWalletView : BackupWalletViewPage
    {
		public BackupWalletView ()
		{
			InitializeComponent ();
		}
    }

    public class BackupWalletViewPage : ContentPageBase<BackupWalletViewModel> { }
}
