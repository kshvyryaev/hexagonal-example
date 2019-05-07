using AutoMapper;
using HexagonalExample.Domain.Contracts.Adapters;

namespace HexagonalExample.Infrastructure.Mapping.AutoMapper
{
    public class MapperAdapter : IMapperAdapter
    {
        private readonly IMapper _mapper;

        public MapperAdapter()
        {
            _mapper = MapperFactory.GetInstance();
        }

        public TDestination Map<TDestination>(object source)
            where TDestination : class
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
            where TSource : class
            where TDestination : class
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
