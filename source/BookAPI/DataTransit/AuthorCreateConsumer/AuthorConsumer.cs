using AutoMapper;
using BookAPI.Models;
using BookAPI.Repository;
using LibraryTransit.Contract.Dtos.Authors;
using MassTransit;

namespace BookAPI.DataTransit.AuthorCreateConsumer
{
    public class AuthorConsumer : IConsumer<IAuthorPublishDto>
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public AuthorConsumer(IBookRepository bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }
        public Task Consume(ConsumeContext<IAuthorPublishDto> context)
        {
            var author = _mapper.Map<Author>(context.Message);

            if (author is null)
                throw new ArgumentNullException("Author from received message must not be null");

            if (!_bookRepo.IsAuthorExists(author.Id))
            {
                _bookRepo.CreateAuthor(author);
                _bookRepo.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
