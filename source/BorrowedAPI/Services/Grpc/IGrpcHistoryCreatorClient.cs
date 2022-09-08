using BorrowedAPI.Models;

namespace BorrowedAPI.Services.Grpc
{
    public interface IGrpcHistoryCreatorClient
    {
        Task<bool> CreateHistoryRecordAsync(BorrowedRecord borrowedRecord, DateTime returnedDate);
    }
}
