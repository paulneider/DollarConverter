namespace DollarConverterServer;

static class DollarConverter
{
    internal static string Convert(double value)
    {
        if (value < 0 || value > 999_999_999.99)
            return "Unsupported input.";

        List<string> numerals = new List<string>();

        int millionsQuantity = (int)Math.Floor(value / 1_000_000);
        if (millionsQuantity > 0)
        {
            numerals.AddRange(GetNumerals1To999(millionsQuantity));
            numerals.Add("million");
        }

        int millions = millionsQuantity * 1_000_000;
        double valueWithOutMillions = value - millions;
        int thousandsQuantity = (int)Math.Floor(valueWithOutMillions / 1_000);
        if (thousandsQuantity > 0)
        {
            numerals.AddRange(GetNumerals1To999(thousandsQuantity));
            numerals.Add("thousand");
        }

        int thousands = thousandsQuantity * 1_000;
        double valueWithOutThousands = valueWithOutMillions - thousands;
        int quantity = (int)Math.Floor(valueWithOutThousands);
        if (quantity > 0)
        {
            if (quantity == 1 && !numerals.Any())
            {
                numerals.Add("one dollar");
            }
            else
            {
                numerals.AddRange(GetNumerals1To999(quantity));
                numerals.Add("dollars");
            }
        }
        else if (numerals.Any())
        {
            numerals.Add("dollars");
        }
        else
        {
            numerals.Add("zero dollars");
        }

        double cents = valueWithOutThousands - quantity;
        int centQuantity = (int)(cents * 100);
        if (centQuantity > 1)
        {
            numerals.Add("and");
            numerals.AddRange(GetNumerals1To999(centQuantity));
            numerals.Add("cents");
        }
        else if (centQuantity == 1)
        {
            numerals.Add("and one cent");
        }

        return string.Join(" ", numerals);
    }
    static IEnumerable<string> GetNumerals1To999(int value)
    {
        if (value < 1 || value > 999)
            throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 1 and 999.");

        List<string> numerals = new List<string>();

        int hundredQuantity = (int)Math.Floor((double)value / 100);
        if (hundredQuantity > 0)
        {
            numerals.Add(GetNumeral1To9(hundredQuantity));
            numerals.Add("hundred");
        }

        int hundred = hundredQuantity * 100;
        int inputWithOutHundred = value - hundred;
        if (inputWithOutHundred == 0)
            return numerals;

        int tensQuantity = (int)Math.Floor((double)inputWithOutHundred / 10);
        if (inputWithOutHundred < 10)
        {
            // 1 - 9
            numerals.Add(GetNumeral1To9(inputWithOutHundred));
        }
        else if (inputWithOutHundred > 10 && inputWithOutHundred < 20)
        {
            // 11 - 19
            numerals.Add(GetNumeral11To19(inputWithOutHundred));
        }
        else
        {
            // 10 & 20 - 99
            string tensNumeral = GetTensNumeral(tensQuantity);

            int tens = tensQuantity * 10;
            int inputWithOutTens = inputWithOutHundred - tens;
            if (inputWithOutTens <= 9 && inputWithOutTens >= 1)
            {
                tensNumeral += $"-{GetNumeral1To9(inputWithOutTens)}";
            }

            numerals.Add(tensNumeral);
        }

        return numerals;
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
}