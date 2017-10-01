using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CoinBag.Models;
using Xamarin.Forms;

namespace CoinBag.Converters
{
    public class TransactionDirectionToSignConverter : IValueConverter
    {
	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    var direction = (TransactionDirection) value;
		    if (direction == TransactionDirection.In)
			    return "+";
		    return "-";
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
		    throw new NotImplementedException();
	    }
    }
}
