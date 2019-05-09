using AutoMapper;
using HexagonalExample.Api.Models.Commands.Books;
using HexagonalExample.Api.Models.Responses;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Data.EF.Models;
using HexagonalExample.Infrastructure.Data.Mongo.Models;

namespace HexagonalExample.Infrastructure.Mapping.AutoMapper.Profiles
{
    internal class BooksProfile : Profile
    {
        public BooksProfile()
        {
            // Mongo
            CreateMap<Book, BookMongoModel>();
            CreateMap<BookMongoModel, Book>();

            // Entity Framework
            CreateMap<Book, BookEFModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x =>
                    string.IsNullOrWhiteSpace(x.Id)
                        ? default(int)
                        : int.Parse(x.Id)));
            CreateMap<BookEFModel, Book>();

            // Api
            CreateMap<Book, BookResponse>();
            CreateMap<CreateBookCommand, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateBookCommand, Book>();
        }
    }
}
