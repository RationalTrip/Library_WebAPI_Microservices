using AutoMapper;
using HistoryAPI.Dtos;
using HistoryAPI.Models;
using LibraryTransit.Contract.Dtos.Books;

namespace HistoryAPI.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, BookReadDto>();

            CreateMap<IBookPublishDto, Book>();
        }
    }
}
