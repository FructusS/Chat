using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media;
using ExCSS;

namespace AvaloniaChat.Desktop
{
    public class BitmapValueConverter : IValueConverter
    {
        public static BitmapValueConverter Instance = new BitmapValueConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                string assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                var uri = new Uri($"avares://{assemblyName}/Assets/avatar_icon.png");
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                return new Bitmap(assets.Open(uri));
            }
            if (value is byte[] rawBytes && targetType == typeof(IImage))
            {
                if (rawBytes.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(rawBytes);
                    return new Bitmap(ms);
                }
                else
                {
                    string assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                    var uri = new Uri($"avares://{assemblyName}/Assets/avatar_icon.png");
                    var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                    return new Bitmap(assets.Open(uri));
                }
            }
            else
            {
                return value;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
