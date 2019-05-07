using AutoMapper;
using HexagonalExample.Infrastructure.Mapping.AutoMapper.Profiles;

namespace HexagonalExample.Infrastructure.Mapping.AutoMapper
{
    internal static class MapperFactory
    {
        internal static IMapper GetInstance()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<AuthorsProfile>();
                configuration.AddProfile<BooksProfile>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
