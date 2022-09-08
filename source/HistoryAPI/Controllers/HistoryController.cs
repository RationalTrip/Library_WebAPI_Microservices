using AutoMapper;
using HistoryAPI.Dtos;
using HistoryAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HistoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryRepository _historyRepo;
        private readonly IMapper _mapper;

        public HistoryController(IHistoryRepository historyRepo, IMapper mapper)
        {
            _historyRepo = historyRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HistoryRecordReadDto>> GetAllHistory()
        {
            var history = _historyRepo.GetAllHistory();

            return Ok(_mapper.Map<IEnumerable<HistoryRecordReadDto>>(history));
        }

        [HttpGet("{id}", Name=nameof(GetHistoryRecordById))]
        public ActionResult<HistoryRecordReadDto> GetHistoryRecordById(int id)
        {
            var history = _historyRepo.GetHistoryRecordById(id);

            if (history == null)
                return NotFound();

            return Ok(_mapper.Map<HistoryRecordReadDto>(history));
        }

        [HttpGet("visitor/{visitorId}")]
        public ActionResult<IEnumerable<HistoryRecordReadDto>> GetVisitorHistory(int visitorId)
        {
            if(!_historyRepo.IsVisitorExists(visitorId))
                return NotFound(new { title = $"Visitor with id {visitorId} not found!", status = StatusCodes.Status404NotFound });
            
            var history = _historyRepo.GetVisitorHistory(visitorId);

            return Ok(_mapper.Map<IEnumerable<HistoryRecordReadDto>>(history));
        }

        [HttpGet("book/{bookId}")]
        public ActionResult<IEnumerable<HistoryRecordReadDto>> GetBookHistory(int bookId)
        {
            if (!_historyRepo.IsBookExists(bookId))
                return NotFound(new { title = $"Book with id {bookId} not found!", status = StatusCodes.Status404NotFound });

            var history = _historyRepo.GetBookHistory(bookId);

            return Ok(_mapper.Map<IEnumerable<HistoryRecordReadDto>>(history));
        }
    }
}
