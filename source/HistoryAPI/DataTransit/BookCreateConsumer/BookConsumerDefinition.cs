using MassTransit;

namespace HistoryAPI.DataTransit.BookCreateConsumer
{
    public class BookConsumerDefinition : ConsumerDefinition<BookConsumer>
    {
        public BookConsumerDefinition(IConfiguration config)
        {
            EndpointName = config["LibraryTransit:books:receiveEndpoint"];
        }
    }
}
