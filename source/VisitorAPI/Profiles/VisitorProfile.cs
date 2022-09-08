using AutoMapper;
using LibraryTransit.Contract.Dtos.Visitors;
using VisitorAPI.Dtos;
using VisitorAPI.Models;

namespace VisitorAPI.Profiles
{
    public class VisitorProfile : Profile
    {
        public VisitorProfile()
        {
            CreateMap<Visitor, VisitorReadDto>();

            CreateMap<VisitorCreateDto, Visitor>()
                .ForMember(visitor => visitor.Id, opt => opt.Ignore())
                .ForMember(visitor => visitor.Age, opt => opt.Ignore());

            CreateMap<Visitor, VisitorPublishDto>();
        }
    }
}
