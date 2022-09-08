using AutoMapper;
using BorrowedAPI.Dtos;
using BorrowedAPI.Models;

namespace BorrowedAPI.Profiles
{
    public class BorrowedRecordProfile : Profile
    {
        public BorrowedRecordProfile()
        {
            CreateMap<BorrowedRecord, BorrowedRecordReadDto>();

            CreateMap<BorrowedRecordCreateDto, BorrowedRecord>()
                .ForMember(hist => hist.Id, opt => opt.Ignore())
                .ForMember(hist => hist.Book, opt => opt.Ignore())
                .ForMember(hist => hist.Visitor, opt => opt.Ignore());
        }
    }
}
