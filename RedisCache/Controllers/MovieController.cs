using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RedisCache.Entities;
using RedisCache.Infra.Caching;
using RedisCache.Mapper;
using RedisCache.Request;

namespace RedisCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ICachingService _cache;
        private readonly MovieDbContext _context;

        public MovieController(ICachingService cache, MovieDbContext context)
        {
            _cache = cache;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies(string movieName = "", int ratting = 0)
        {
            var key = movieName + ratting.ToString();
            var cacheMovies = await _cache.GetAsync(key);

            List<MovieEntity>? movies;
            if (!string.IsNullOrEmpty(cacheMovies))
            {
                movies = JsonConvert.DeserializeObject<List<MovieEntity>>(cacheMovies);
                return Ok(movies);
            }

            movies = await _context.Movies
                .Where(m => m.Rattings == ratting || m.Name.Contains(movieName))
                .ToListAsync();

            if (movies == null)
                return NotFound();

            await _cache.SetAsync(key, JsonConvert.SerializeObject(movies));

            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovies(List<MoviesDto> movies)
        {
            await _context.Movies.AddRangeAsync(movies.ToEntity());
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
