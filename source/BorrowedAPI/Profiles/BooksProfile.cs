using AutoMapper;
using BorrowedAPI.Dtos;
using BorrowedAPI.Models;
using LibraryTransit.Contract.Dtos.Books;

namespace BorrowedAPI.Profiles
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
