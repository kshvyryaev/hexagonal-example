using AutoMapper;
using HexagonalExample.Api.Models.Commands.Books;
using HexagonalExample.Api.Models.Responses;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Data.Mongo.Models;

namespace HexagonalExample.Infrastructure.Mapping.AutoMapper.Profiles
{
    internal class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, BookMongoModel>();
            CreateMap<BookMongoModel, Book>();

            CreateMap<Book, BookResponse>();
            CreateMap<CreateBookCommand, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateBookCommand, Book>();
        }
    }
}
