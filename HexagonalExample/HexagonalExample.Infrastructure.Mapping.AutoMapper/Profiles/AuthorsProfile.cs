using AutoMapper;
using HexagonalExample.Api.Models.Commands.Authors;
using HexagonalExample.Api.Models.Responses;
using HexagonalExample.Domain.Entities;
using HexagonalExample.Infrastructure.Data.Mongo.Models;

namespace HexagonalExample.Infrastructure.Mapping.AutoMapper.Profiles
{
    internal class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, AuthorMongoModel>();
            CreateMap<AuthorMongoModel, Author>();

            CreateMap<Author, AuthorResponse>();
            CreateMap<CreateAuthorCommand, Author>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateAuthorCommand, Author>();
        }
    }
}
