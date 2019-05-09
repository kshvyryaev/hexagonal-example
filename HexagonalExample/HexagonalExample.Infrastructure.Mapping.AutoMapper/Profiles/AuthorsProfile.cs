using AutoMapper;
using HexagonalExample.Api.Models.Commands.Authors;
using HexagonalExample.Api.Models.Responses;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Data.EF.Models;
using HexagonalExample.Infrastructure.Data.Mongo.Models;

namespace HexagonalExample.Infrastructure.Mapping.AutoMapper.Profiles
{
    internal class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            // Mongo
            CreateMap<Author, AuthorMongoModel>();
            CreateMap<AuthorMongoModel, Author>();

            // Entity Framework
            CreateMap<Author, AuthorEFModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x =>
                    string.IsNullOrWhiteSpace(x.Id)
                        ? default(int)
                        : int.Parse(x.Id)))
                .ForMember(dest => dest.BookId, opt => opt.Ignore())
                .ForMember(dest => dest.Book, opt => opt.Ignore());
            CreateMap<AuthorEFModel, Author>();

            // Api
            CreateMap<Author, AuthorResponse>();
            CreateMap<CreateAuthorCommand, Author>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateAuthorCommand, Author>();
        }
    }
}
