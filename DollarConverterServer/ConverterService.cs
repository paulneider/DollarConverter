using GrpcConverter;

namespace DollarConverterServer;

public class ConverterService : Converter.ConverterBase
{
    private readonly ILogger logger;
    public ConverterService(ILogger<ConverterService> logger)
    {
        this.logger = logger;
    }

    public override Task<ConvertReply> ConvertDollar(ConvertRequest request, Grpc.Core.ServerCallContext context)
    {
        logger.LogInformation($"Input: {request.Value}");

        return Task.Run(() => 
        {
            string msg = DollarConverter.Convert(request.Value);
            logger.LogInformation($"Output: {msg}");
            return new ConvertReply() { Message = msg }; 
        });
    }
}