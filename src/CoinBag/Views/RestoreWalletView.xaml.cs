using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinBag.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoinBag.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RestoreWalletView : RestoreWalletViewPage
	{
		public RestoreWalletView ()
		{
			InitializeComponent ();
		}
	}

    public class RestoreWalletViewPage : ContentPageBase<RestoreWalletViewModel> { }
}
