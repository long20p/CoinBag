﻿using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace CoinBag.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var bytes = (byte[]) value;
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
