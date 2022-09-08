using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BorrowedAPI.Dtos;
using BorrowedAPI.Repository;

namespace BorrowedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly IVisitorRepository _visitorRepo;
        private readonly IMapper _mapper;

        public VisitorsController(IVisitorRepository visitorRepo, IMapper mapper)
        {
            _visitorRepo = visitorRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VisitorReadDto>> GetAllVisitors()
        {
            var visitors = _visitorRepo.GetAllVisitors();

            return Ok(_mapper.Map<IEnumerable<VisitorReadDto>>(visitors));
        }

        [HttpGet("{id}", Name="GetVisitorById")]
        public ActionResult<VisitorReadDto> GetVisitorById(int id)
        {
            var visitor = _visitorRepo.GetVisitorById(id);

            if (visitor == null)
                return NotFound();

            return Ok(_mapper.Map<VisitorReadDto>(visitor));
        }
    }
}
