using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HistoryAPI.Models;
using HistoryAPI.Repository;

namespace HistoryAPI.Services.Grpc
{
    public class GrpcHistoryCreatorService : GrpcHistoryCreator.GrpcHistoryCreatorBase
    {
        private readonly IHistoryRepository _historyRepo;
        private readonly IMapper _mapper;

        public GrpcHistoryCreatorService(IHistoryRepository historyRepo, IMapper mapper)
        {
            _historyRepo = historyRepo;
            _mapper = mapper;
        }
        public override Task<BoolValue> CreateHistory(HistoryCreateModel historyModel, ServerCallContext context)
        {
            var historyRecord = _mapper.Map<HistoryRecord>(historyModel);

            _historyRepo.CreateHistoryRecord(historyRecord);
            var isCreated = _historyRepo.SaveChanges();

            var grcpResult = new BoolValue();
            grcpResult.Value = isCreated;

            return Task.FromResult(grcpResult);
        }
    }
}
