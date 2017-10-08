using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adapt.Presentation;
using CoinBag.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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
