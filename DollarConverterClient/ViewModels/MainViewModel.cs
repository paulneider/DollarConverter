using Grpc.Net.Client;
using GrpcConverter;
using System;
using System.Configuration;
using System.DirectoryServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DollarConverterClient;

internal class MainViewModel : ViewModelBase
{
    private readonly GrpcChannel channel;

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
    private bool isEnabled = true;
    public bool IsEnabled
    {
        get => isEnabled;
        set
        {
            isEnabled = value;
            OnPropertyChanged();
        }
    }
    public Command ConvertCommand { get; init; }
    public string? ServerAddress { get; init; }

    public MainViewModel()
    {
        ServerAddress = ConfigurationManager.AppSettings.Get("ServerAddress");
        ConvertCommand = new Command(Convert, CanConvert);
        
        try
        {
            channel = GrpcChannel.ForAddress(ServerAddress);
        }
        catch (Exception)
        {
            IsEnabled = false;
            Output = "ERROR: The set server URL is not valid.";
        }
    }

    private async void Convert(object? param)
    {
        await ConvertAsync();
    }
    private async Task ConvertAsync()
    {
        try
        {
            Mouse.OverrideCursor = Cursors.Wait;
            IsEnabled = false;
            ConvertCommand.NotifyCanExecutedChanged();

            Converter.ConverterClient client = new Converter.ConverterClient(channel);
            ConvertReply reply = await client.ConvertDollarAsync(new ConvertRequest { Value = Input });
            
            Output = reply.Message;
        }
        catch(Exception ex)
        {
            if (ex is Grpc.Core.RpcException)
            {
                Output = "ERROR: Unable to connect to server.";
            }
            else
            {
                Output = "ERROR: " + ex.ToString();
            }
        }
        finally
        {
            Mouse.OverrideCursor = null;
            IsEnabled = true;
            ConvertCommand.NotifyCanExecutedChanged();
        }
    }
    private bool CanConvert(object? param)
    {
        return IsEnabled;
    }
}
