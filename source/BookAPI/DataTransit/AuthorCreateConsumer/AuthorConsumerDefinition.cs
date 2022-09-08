using MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookAPI.DataTransit.AuthorCreateConsumer
{
    public class AuthorConsumerDefinition : ConsumerDefinition<AuthorConsumer>
    {
        public AuthorConsumerDefinition(IConfiguration config)
        {
            EndpointName = config["LibraryTransit:authors:receiveEndpoint"];
        } 
    }
}
