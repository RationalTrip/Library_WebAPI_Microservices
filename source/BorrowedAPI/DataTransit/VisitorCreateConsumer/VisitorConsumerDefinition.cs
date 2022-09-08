using MassTransit;

namespace BorrowedAPI.DataTransit.VisitorCreateConsumer
{
    public class VisitorConsumerDefinition : ConsumerDefinition<VisitorConsumer>
    {
        public VisitorConsumerDefinition(IConfiguration config)
        {
            EndpointName = config["LibraryTransit:visitors:receiveEndpoint"];
        }
    }
}
