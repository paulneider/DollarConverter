using System.Windows.Input;

namespace DollarConverterClient;

internal class MainViewModel : ViewModelBase
{
    private double input;
    public double Input
    {
        get => input;
        set
        {
            input = value;
            OnPropertyChanged();
        }
    }

    private string output;
    public string Output
    {
        get => output;
        set
        {
            output = value;
            OnPropertyChanged();
        }
    }

    public ICommand ConvertCommand { get; init; }


    public MainViewModel()
    {
        ConvertCommand = new Command(Convert);
    }


    private void Convert(object param)
    {
        try
        {
            Mouse.OverrideCursor = Cursors.Wait;

            System.Threading.Thread.Sleep(3000);
        }
        finally
        {
            Mouse.OverrideCursor = null;
        }

        Output = "nine hundred ninety-nine million nine hundred\r\nninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents";
    }

}
