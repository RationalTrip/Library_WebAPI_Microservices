using AutoMapper;
using HistoryAPI.Dtos;
using HistoryAPI.Models;
using HistoryAPI.Services.Grpc;

namespace HistoryAPI.Profiles
{
    public class HistoryRecordProfile : Profile
    {
        public HistoryRecordProfile()
        {
            CreateMap<HistoryRecord, HistoryRecordReadDto>();

            CreateMap<HistoryCreateModel, HistoryRecord>()
                .ForMember(record => record.BorrowedDate,
                    opt => opt.MapFrom(historyModel => new DateTime(historyModel.BorrowedDateTicks)))
                .ForMember(record => record.ReturnedDate,
                    opt => opt.MapFrom(historyModel => new DateTime(historyModel.ReturnedDateTicks)));
        }
    }
}
