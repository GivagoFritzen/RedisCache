using RedisCache.Entities;
using RedisCache.Request;

namespace RedisCache.Mapper
{
    public static class MovieExtension
    {
        public static IEnumerable<MovieEntity> ToEntity(this List<MoviesDto> dtos)
        {
            return dtos.Select(dto =>
            {
                return new MovieEntity(dto.Name, dto.Rattings);
            });
        }
    }
}
