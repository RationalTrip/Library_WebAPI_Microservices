using AutoMapper;
using BookAPI.Dtos;
using BookAPI.Models;
using BookAPI.Repository;
using LibraryTransit.Contract.Dtos.Books;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepo, IBus bus, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookReadDto>> GetAllBooks()
        {
            var books = _bookRepo.GetAllBooks();

            return Ok(_mapper.Map<IEnumerable<BookReadDto>>(books));
        }

        [HttpGet("{id}", Name=nameof(GetBookById))]
        public ActionResult<BookReadDto> GetBookById(int id)
        {
            var book = _bookRepo.GetBookById(id);

            if (book == null)
                return NotFound();

            return Ok(_mapper.Map<BookReadDto>(book));
        }

        [HttpPost]
        public async Task<ActionResult<BookReadDto>> CreateBook(BookCreateDto bookModel)
        {
            var authorId = bookModel.AuthorId;

            if (!_bookRepo.IsAuthorExists(authorId))
                return NotFound(new { title=$"Author with id {authorId} not found!", status=StatusCodes.Status404NotFound });

            var book = _mapper.Map<Book>(bookModel);

            _bookRepo.CreateBook(book);
            _bookRepo.SaveChanges();

            await _bus.Publish<IBookPublishDto>(_mapper.Map<BookPublishDto>(book));

            var bookRead = _mapper.Map<BookReadDto>(book);

            return CreatedAtRoute(nameof(GetBookById), new { id = bookRead.Id }, bookRead);
        }
    }
}
