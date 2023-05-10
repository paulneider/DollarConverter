using System;
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
            return new ValidationResult(false, "Not a string.");
        }

        if (double.TryParse(stringValue, new CultureInfo("de-de"), out double input))
        {
            if (input < 0)
            {
                return new ValidationResult(false, "The number must be greater than or equal to 0.");
            }
            else if (input > 999_999_999.99)
            {
                return new ValidationResult(false, "The number must be less than or equal to 999.999.999,99.");
            }

            double decimals = input * 100;
            if (Math.Ceiling(decimals) > decimals)
            {
                return new ValidationResult(false, "The number may have a maximum of two decimal places.");
            }
        }
        else
        {
            return new ValidationResult(false, "No number.");
        }


        return new ValidationResult(true, string.Empty);
    }
}
