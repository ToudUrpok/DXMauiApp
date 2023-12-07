using System.Globalization;

namespace DXMauiApp.Views;

public partial class Cars : ContentView
{
	public Cars()
	{
		InitializeComponent();
	}
}

public class BoolToColorConverter : IValueConverter
{
    public Color FalseSource { get; set; }
    public Color TrueSource { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is bool))
        {
            return null;
        }
        return (bool)value ? TrueSource : FalseSource;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}