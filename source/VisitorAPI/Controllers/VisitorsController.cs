using AutoMapper;
using LibraryTransit.Contract.Dtos.Visitors;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using VisitorAPI.Dtos;
using VisitorAPI.Models;
using VisitorAPI.Repository;

namespace VisitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly IVisitorRepository _visitorRepo;
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public VisitorsController(IVisitorRepository visitorRepo, IBus bus, IMapper mapper)
        {
            _visitorRepo = visitorRepo;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VisitorReadDto>> GetAllVisitors()
        {
            var visitors = _visitorRepo.GetAllVisitors();

            return Ok(_mapper.Map<IEnumerable<VisitorReadDto>>(visitors));
        }

        [HttpGet("{id}", Name=nameof(GetVisitorById))]
        public ActionResult<VisitorReadDto> GetVisitorById(int id)
        {
            var visitor = _visitorRepo.GetVisitorById(id);

            if (visitor == null)
                return NotFound();

            return Ok(_mapper.Map<VisitorReadDto>(visitor));
        }

        [HttpPost]
        public async Task<ActionResult<VisitorReadDto>> CreateVisitor(VisitorCreateDto visitorModel)
        {
            var visitor = _mapper.Map<Visitor>(visitorModel);

            _visitorRepo.CreateVisitor(visitor);
            _visitorRepo.SaveChanges();

            await _bus.Publish<IVisitorPublishDto>(_mapper.Map<VisitorPublishDto>(visitor));

            var visitorRead = _mapper.Map<VisitorReadDto>(visitor);

            return CreatedAtRoute(nameof(GetVisitorById), new { id = visitorRead.Id }, visitorRead);
        }
    }
}
