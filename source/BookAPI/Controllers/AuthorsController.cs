using AutoMapper;
using BookAPI.Dtos;
using BookAPI.Models;
using BookAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public AuthorsController(IBookRepository bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorReadDto>> GetAllAuthors()
        {
            var authors = _bookRepo.GetAllAuthors();

            return Ok(_mapper.Map<IEnumerable<AuthorReadDto>>(authors));
        }

        [HttpGet("{id}")]
        public ActionResult<AuthorReadDto> GetAuthorById(int id)
        {
            var author = _bookRepo.GetAuthorById(id);

            if (author == null)
                return NotFound();

            return Ok(_mapper.Map<AuthorReadDto>(author));
        }
    }
}
