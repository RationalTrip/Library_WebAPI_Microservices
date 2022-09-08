using BorrowedAPI.Models;
using Grpc.Core;
using HistoryAPI.Services.Grpc;

namespace BorrowedAPI.Services.Grpc
{
    public class GrpcHistoryCreatorClient : IGrpcHistoryCreatorClient
    {
        private readonly GrpcHistoryCreator.GrpcHistoryCreatorClient _client;
        private readonly ILogger<GrpcHistoryCreatorClient> _logger;

        public GrpcHistoryCreatorClient(GrpcHistoryCreator.GrpcHistoryCreatorClient client, ILogger<GrpcHistoryCreatorClient> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<bool> CreateHistoryRecordAsync(BorrowedRecord borrowedRecord, DateTime returnedDate)
        {
            var historyCreateModel = GenerateHistoryCreateModel(borrowedRecord, returnedDate);

            try
            {
                var isCreated = await _client.CreateHistoryAsync(historyCreateModel);
                return isCreated.Value;

            }catch(RpcException e)
            {
                _logger.LogError(e.Message);
            }

            return false;
        }
        static HistoryCreateModel GenerateHistoryCreateModel(BorrowedRecord borrowedRecord, DateTime returnedDate)
        {
            return new HistoryCreateModel
            {
                BookId = borrowedRecord.BookId,
                VisitorId = borrowedRecord.VisitorId,
                BorrowedDateTicks = borrowedRecord.BorrowedDate.Ticks,
                ReturnedDateTicks = returnedDate.Ticks,
                IsReturnedLate = returnedDate.Date > borrowedRecord.ReturnDeadline.Date
            };
        }
    }
}
