using AutoMapper;
using BorrowedAPI.Dtos;
using BorrowedAPI.Models;
using BorrowedAPI.Repository;
using BorrowedAPI.Services.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace BorrowedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedController : ControllerBase
    {
        private readonly IBorrowedRepository _borrowedRepo;
        private readonly IMapper _mapper;
        private readonly IGrpcHistoryCreatorClient _historyCreatorClient;

        public BorrowedController(IBorrowedRepository borrowedRepo, IMapper mapper, IGrpcHistoryCreatorClient historyCreatorClient)
        {
            _borrowedRepo = borrowedRepo;
            _mapper = mapper;
            _historyCreatorClient = historyCreatorClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BorrowedRecordReadDto>> GetAllBorrowedRecords()
        {
            var borrowed = _borrowedRepo.GetAllBorrowedRecords();

            return Ok(_mapper.Map<IEnumerable<BorrowedRecordReadDto>>(borrowed));
        }

        [HttpGet("{id}", Name=nameof(GetBorrowedRecordById))]
        public ActionResult<BorrowedRecordReadDto> GetBorrowedRecordById(int id)
        {
            var borrowed = _borrowedRepo.GetBorrowedRecordById(id);

            if (borrowed == null)
                return NotFound();

            return Ok(_mapper.Map<BorrowedRecordReadDto>(borrowed));
        }

        [HttpGet("visitor/{visitorId}")]
        public ActionResult<IEnumerable<BorrowedRecordReadDto>> GetVisitorBorrowedRecords(int visitorId)
        {
            if(!_borrowedRepo.IsVisitorExists(visitorId))
                return NotFound(new { title = $"Visitor with id {visitorId} not found!", status = StatusCodes.Status404NotFound });
            
            var borrowed = _borrowedRepo.GetVisitorBorrowedRecords(visitorId);

            return Ok(_mapper.Map<IEnumerable<BorrowedRecordReadDto>>(borrowed));
        }

        [HttpGet("book/{bookId}")]
        public ActionResult<IEnumerable<BorrowedRecordReadDto>> GetBookBorrowedRecords(int bookId)
        {
            if (!_borrowedRepo.IsBookExists(bookId))
                return NotFound(new { title = $"Book with id {bookId} not found!", status = StatusCodes.Status404NotFound });

            var borrowed = _borrowedRepo.GetBookBorrowedRecords(bookId);

            return Ok(_mapper.Map<IEnumerable<BorrowedRecordReadDto>>(borrowed));
        }

        [HttpPost]
        public ActionResult<BorrowedRecordReadDto> CreateBorrowedRecord(BorrowedRecordCreateDto borrowedRecordModel)
        {
            int visitorId = borrowedRecordModel.VisitorId;
            if (!_borrowedRepo.IsVisitorExists(visitorId))
                return NotFound(new { title = $"Visitor with id {visitorId} not found!", status = StatusCodes.Status404NotFound });

            int bookId = borrowedRecordModel.BookId;
            if (!_borrowedRepo.IsBookExists(bookId))
                return NotFound(new { title = $"Book with id {bookId} not found!", status = StatusCodes.Status404NotFound });

            var borrowedRecord = _mapper.Map<BorrowedRecord>(borrowedRecordModel);

            _borrowedRepo.CreateBorrowedRecord(borrowedRecord);
            _borrowedRepo.SaveChanges();

            var borrowedRecordRead = _mapper.Map<BorrowedRecordReadDto>(borrowedRecord);

            return CreatedAtRoute(nameof(GetBorrowedRecordById), new { id = borrowedRecordRead.Id }, borrowedRecordRead);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveBorrowedRecord(int id, DateTime bookReturnedDate)
        {
            var borrowedRecord = _borrowedRepo.GetBorrowedRecordById(id);

            if (borrowedRecord == null)
                return NotFound(new { title = $"Borrowed Record with id {id} not found!", status = StatusCodes.Status404NotFound });

            var historyCreated = await _historyCreatorClient.CreateHistoryRecordAsync(borrowedRecord, bookReturnedDate);

            if (!historyCreated)
                return Problem(detail:"History record can not be created.",
                    statusCode: StatusCodes.Status500InternalServerError);

            _borrowedRepo.RemoveBorrowedRecord(borrowedRecord);
            _borrowedRepo.SaveChanges();

            return Ok();
        }
    }
}
