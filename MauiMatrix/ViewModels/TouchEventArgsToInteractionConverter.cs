using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace MauiMatrix.ViewModels;

public class TouchEventArgsToInteractionConverter : BaseConverterOneWay<TouchEventArgs?, object?>
{
    public TouchEventArgsToInteractionConverter()
    {
    }

    public override object DefaultConvertReturnValue
    {
        get;
        set;
    } = null;

    /// <summary>
	/// Converts/Extracts the incoming value from <see cref="TouchEventArgs"/> object and returns the value of <see cref="ItemTappedEventArgs.Item"/> property from it.
	/// </summary>
	/// <param name="value">The value to convert.</param>
	/// <param name="culture">(Not Used)</param>
	/// <returns>A <see cref="TouchEventArgs.Item"/> object from object of type <see cref="TouchEventArgs"/>.</returns>
	[return: NotNullIfNotNull("value")]
    public override object? ConvertFrom(TouchEventArgs? value, CultureInfo? culture = null) =>
        value switch
        {
            null => null,
            _ => new InteractionParams
            {
                X = (int)value.Touches[0].X,
                Y = (int)value.Touches[0].Y
            }
        };

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is TouchEventArgs args)
        {
            return new InteractionParams
            {
                X = (int)args.Touches[0].X,
                Y = (int)args.Touches[0].Y
            };
        }
        return new InteractionParams
        {
            X = -1,
            Y = -1
        };
    }
}
