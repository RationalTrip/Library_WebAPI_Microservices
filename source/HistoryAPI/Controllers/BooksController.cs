using AutoMapper;
using HistoryAPI.Dtos;
using HistoryAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HistoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookReadDto>> GetAllBooks()
        {
            var books = _bookRepo.GetAllBooks();

            return Ok(_mapper.Map<IEnumerable<BookReadDto>>(books));
        }

        [HttpGet("{id}", Name="GetBookById")]
        public ActionResult<BookReadDto> GetBookById(int id)
        {
            var book = _bookRepo.GetBookById(id);

            if (book == null)
                return NotFound();

            return Ok(_mapper.Map<BookReadDto>(book));
        }
    }
}
