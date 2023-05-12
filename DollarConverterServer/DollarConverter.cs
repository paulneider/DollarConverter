namespace DollarConverterServer;

static class DollarConverter
{
    internal static string Convert(double value)
    {
        if (value < 0 || value > 999_999_999.99)
            return "Unsupported input.";

        string numeral = string.Empty;

        int millionsQuantity = (int)Math.Floor(value / 1_000_000);
        if (millionsQuantity > 0)
        {
            numeral = $"{GetNumeral1To999(millionsQuantity)} million";
        }

        int millions = millionsQuantity * 1_000_000;
        double valueWithOutMillions = value - millions;
        int thousandsQuantity = (int)Math.Floor(valueWithOutMillions / 1_000);
        if (thousandsQuantity > 0)
        {
            numeral = numeral.AddSpaceIfNotEmpty() + $"{GetNumeral1To999(thousandsQuantity)} thousand";
        }

        int thousands = thousandsQuantity * 1_000;
        double valueWithOutThousands = valueWithOutMillions - thousands;
        int quantity = (int)Math.Floor(valueWithOutThousands);
        if (quantity > 0)
        {
            if (quantity == 1 && string.IsNullOrEmpty(numeral))
            {
                numeral = "one dollar";
            }
            else
            {
                numeral = numeral.AddSpaceIfNotEmpty() + $"{GetNumeral1To999(quantity)} dollars";
            }
        }
        else if (string.IsNullOrEmpty(numeral))
        {
            numeral = "zero dollars";
        }
        else
        {
            numeral += " dollars";
        }

        double cents = valueWithOutThousands - quantity;
        int centQuantity = (int)(cents * 100);
        if (centQuantity > 1)
        {
            numeral = numeral.AddSpaceIfNotEmpty() + $"and {GetNumeral1To999(centQuantity)} cents";
        }
        else if (centQuantity == 1)
        {
            numeral = numeral.AddSpaceIfNotEmpty() + "and one cent";
        }

        return numeral;
    }
    static string GetNumeral1To999(int value)
    {
        if (value < 1 || value > 999)
            throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 1 and 999.");

        string numeral = string.Empty;

        int hundredQuantity = (int)Math.Floor((double)value / 100);
        if (hundredQuantity > 0)
        {
            numeral = $"{GetNumeral1To9(hundredQuantity)} hundred";
        }

        int hundred = hundredQuantity * 100;
        int inputWithOutHundred = value - hundred;
        if (inputWithOutHundred == 0)
            return numeral;

        int tensQuantity = (int)Math.Floor((double)inputWithOutHundred / 10);
        if (inputWithOutHundred < 10)
        {
            // 1 - 9
            numeral = numeral.AddSpaceIfNotEmpty() + GetNumeral1To9(inputWithOutHundred);
        }
        else if (inputWithOutHundred > 10 && inputWithOutHundred < 20)
        {
            // 11 - 19
            numeral = numeral.AddSpaceIfNotEmpty() + GetNumeral11To19(inputWithOutHundred);
        }
        else
        {
            // 10 & 20 - 99
            numeral = numeral.AddSpaceIfNotEmpty() + GetTensNumeral(tensQuantity);

            int tens = tensQuantity * 10;
            int inputWithOutTens = inputWithOutHundred - tens;
            if (inputWithOutTens <= 9 && inputWithOutTens >= 1)
                numeral += $"-{GetNumeral1To9(inputWithOutTens)}";
        }

        return numeral;
    }
    static string GetNumeral1To9(int value) =>
        value switch
        {
            1 => "one",
            2 => "two",
            3 => "three",
            4 => "four",
            5 => "five",
            6 => "six",
            7 => "seven",
            8 => "eight",
            9 => "nine",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 1 and 9.")
        };
    static string GetNumeral11To19(int value) =>
        value switch
        {
            11 => "eleven",
            12 => "twelve",
            13 => "thirteen",
            14 => "fourteen",
            15 => "fifteen",
            16 => "sixteen",
            17 => "seventeen",
            18 => "eighteen",
            19 => "nineteen",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 11 and 19.")
        };
    static string GetTensNumeral(int value) =>
        value switch
        {
            1 => "ten",
            2 => "twenty",
            3 => "thirty",
            4 => "forty",
            5 => "fifty",
            6 => "sixty",
            7 => "seventy",
            8 => "eighty",
            9 => "ninety",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 1 and 9.")
        };
    static string AddSpaceIfNotEmpty(this string value)
    {
        return string.IsNullOrEmpty(value) ? value : value + " ";
    }
}