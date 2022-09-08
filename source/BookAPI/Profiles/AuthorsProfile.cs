using AutoMapper;
using BookAPI.Dtos;
using BookAPI.Models;
using LibraryTransit.Contract.Dtos.Authors;

namespace BookAPI.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, AuthorReadDto>();
            CreateMap<IAuthorPublishDto, Author>();
        }
    }
}
