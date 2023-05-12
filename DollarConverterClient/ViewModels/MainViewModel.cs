using Grpc.Net.Client;
using GrpcConverter;
using System;
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
    private string output = string.Empty;
    public string Output
    {
        get => output;
        set
        {
            output = value;
            OnPropertyChanged();
        }
    }
    public Command ConvertCommand { get; init; }
    private readonly GrpcChannel channel;
    bool canConvert = true;
    public MainViewModel()
    {
        channel = GrpcChannel.ForAddress("http://localhost:5000");
        ConvertCommand = new Command(Convert, CanConvert);
    }

    private async void Convert(object? param)
    {
        try
        {
            Mouse.OverrideCursor = Cursors.Wait;
            canConvert = false;
            ConvertCommand.NotifyCanExecutedChanged();

            Converter.ConverterClient client = new Converter.ConverterClient(channel);
            ConvertReply reply = await client.ConvertDollarAsync(new ConvertRequest { Value = Input });

            Output = reply.Message;
        }
        finally
        {
            Mouse.OverrideCursor = null;
            canConvert = true;
            ConvertCommand.NotifyCanExecutedChanged();
        }
    }
    private bool CanConvert(object? param)
    {
        return canConvert;
    }
}
