using AutoMapper;
using HistoryAPI.Dtos;
using HistoryAPI.Models;
using LibraryTransit.Contract.Dtos.Visitors;

namespace HistoryAPI.Profiles
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
