using AuthorAPI.Dtos;
using AuthorAPI.Models;
using AutoMapper;
using LibraryTransit.Contract.Dtos.Authors;

namespace AuthorAPI.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, AuthorReadDto>();

            CreateMap<AuthorCreateDto, Author>()
                .ForMember(author => author.Id, opt => opt.Ignore());

            CreateMap<Author, AuthorPublishDto>();
        }
    }
}
