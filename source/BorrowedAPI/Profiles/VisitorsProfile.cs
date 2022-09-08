using AutoMapper;
using BorrowedAPI.Dtos;
using BorrowedAPI.Models;
using LibraryTransit.Contract.Dtos.Visitors;

namespace BorrowedAPI.Profiles
{
    public class VisitorsProfile : Profile
    {
        public VisitorsProfile()
        {
            CreateMap<Visitor, VisitorReadDto>();
            CreateMap<IVisitorPublishDto, Visitor>();
        }
    }
}
