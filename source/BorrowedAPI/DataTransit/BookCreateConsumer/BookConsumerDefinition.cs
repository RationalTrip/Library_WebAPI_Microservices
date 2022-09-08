using MassTransit;

namespace BorrowedAPI.DataTransit.BookCreateConsumer
{
    public class BookConsumerDefinition : ConsumerDefinition<BookConsumer>
    {
        public BookConsumerDefinition(IConfiguration config)
        {
            EndpointName = config["LibraryTransit:books:receiveEndpoint"];
        }
    }
}
