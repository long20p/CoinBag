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
	public partial class MenuView : MenuViewPage
	{
	    private RootPage root;
	    private List<MenuItem> menuItems;

		public MenuView (RootPage root)
		{
			InitializeComponent ();
		    this.root = root;
		    CoinBagMenuListView.ItemsSource = menuItems = new List<MenuItem>
		    {
                new MenuItem{PageName = Constants.MainPage},
                new MenuItem{PageName = Constants.BackupWalletPage},
                new MenuItem{PageName = Constants.RestoreWalletPage},
                new MenuItem{PageName = Constants.SendCoinPage},
                new MenuItem{PageName = Constants.GetCoinPage}
		    };

		    CoinBagMenuListView.SelectedItem = menuItems[0];
		    CoinBagMenuListView.ItemSelected += (sender, args) =>
		    {
                root.NavigateTo((args.SelectedItem as MenuItem).PageName);
		    };
		}
	}

    public class MenuItem
    {
        public string IconSource { get; set; }
        public string PageName { get; set; }
    }

    public class MenuViewPage : ContentPageBase<MenuViewModel> { }
}
