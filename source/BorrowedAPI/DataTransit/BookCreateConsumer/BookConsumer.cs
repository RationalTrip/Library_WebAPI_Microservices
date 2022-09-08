using AutoMapper;
using BorrowedAPI.Models;
using BorrowedAPI.Repository;
using LibraryTransit.Contract.Dtos.Books;
using MassTransit;

namespace BorrowedAPI.DataTransit.BookCreateConsumer
{
    public class BookConsumer : IConsumer<IBookPublishDto>
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public BookConsumer(IBookRepository bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }
        public Task Consume(ConsumeContext<IBookPublishDto> context)
        {
            var book = _mapper.Map<Book>(context.Message);

            if (book is null)
                throw new ArgumentNullException("Book from received message must not be null");

            if (!_bookRepo.IsBookExists(book.Id))
            {
                _bookRepo.CreateBook(book);
                _bookRepo.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
