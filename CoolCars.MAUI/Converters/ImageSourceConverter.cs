using System.Globalization;
using CoolCars.MAUI.Handlers;

namespace CoolCars.MAUI.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageUrl = value as string;
            
            if (string.IsNullOrEmpty(imageUrl))
            {
                return ImageHandler.DefaultImageUrl;
            }
            
            // Check if the URL is valid
            if (!Uri.TryCreate(imageUrl, UriKind.Absolute, out _))
            {
                return ImageHandler.DefaultImageUrl;
            }
            
            return imageUrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
