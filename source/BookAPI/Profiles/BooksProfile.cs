using AutoMapper;
using BookAPI.Dtos;
using BookAPI.Models;
using LibraryTransit.Contract.Dtos.Books;

namespace BookAPI.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<BookCreateDto, Book>()
                .ForMember(book => book.Id, opt => opt.Ignore())
                .ForMember(book => book.Author, opt => opt.Ignore());

            CreateMap<Book, BookReadDto>();

            CreateMap<Book, BookPublishDto>();
        }
    }
}
