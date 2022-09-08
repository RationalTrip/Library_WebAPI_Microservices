using AuthorAPI.Dtos;
using AuthorAPI.Models;
using AuthorAPI.Repository;
using AutoMapper;
using LibraryTransit.Contract.Dtos.Authors;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepo;
        private readonly IBus _bus;

        public AuthorsController(IAuthorRepository authorRepo, IBus bus, IMapper mapper)
        {
            _authorRepo = authorRepo;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorReadDto>> GetAllAuthors()
        {
            var authors = _authorRepo.GetAllAuthors();

            return Ok(_mapper.Map<IEnumerable<AuthorReadDto>>(authors));
        }

        [HttpGet("{id}", Name = nameof(GetAuthorById))]
        public ActionResult<AuthorReadDto> GetAuthorById(int id)
        {
            var author = _authorRepo.GetAuthorById(id);

            if (author == null)
                return NotFound();

            return Ok(_mapper.Map<AuthorReadDto>(author));
        }

        [HttpPost]
        public async Task<ActionResult<AuthorReadDto>> CreateAuthorAsync(AuthorCreateDto authorCreateDto)
        {
            var author = _mapper.Map<Author>(authorCreateDto);

            _authorRepo.CreateAuthor(author);

            _authorRepo.SaveChanges();

            await _bus.Publish<IAuthorPublishDto>(_mapper.Map<AuthorPublishDto>(author));

            var autorRead = _mapper.Map<AuthorReadDto>(author);

            return CreatedAtRoute(nameof(GetAuthorById), new { id = autorRead.Id }, autorRead);
        }
    }
}
