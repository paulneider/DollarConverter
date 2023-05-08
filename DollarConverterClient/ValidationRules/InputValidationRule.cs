using System.Globalization;
using System.Windows.Controls;

namespace DollarConverterClient;

internal class InputValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string? stringValue = value as string;
        if (stringValue is null)
        {
            return new ValidationResult(false, "Keine Zeichenkette.");
        }

        if (double.TryParse(stringValue, new CultureInfo("de-de"), out double input))
        {
            if (input < 0)
            {
                return new ValidationResult(false, "Zahl muss größer als 0 sein.");
            }
            else if (input > 999_999_999.99)
            {
                return new ValidationResult(false, "Zahl muss kleiner als 999.999.999,99 sein.");
            }
        }
        else
        {
            return new ValidationResult(false, "Keine Zahl.");
        }


        return new ValidationResult(true, string.Empty);
    }
}
