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
            BrowseBtn.Clicked += BrowseBtn_Clicked;
		}

        private async void BrowseBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var filePicker = App.PresentationFactory.CreateFilePicker();
                var fileData = await filePicker.PickAndOpenFileForReading();
                //var s = fileData.FileName;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }

    public class RestoreWalletViewPage : ContentPageBase<RestoreWalletViewModel> { }
}
