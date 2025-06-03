using Microsoft.AspNetCore.Mvc;
using Orm.Movies.Api.Services;
using Orm.Movies.DataModels;

namespace Orm.Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _movieService.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var movie = _movieService.GetById(id);
            return movie is not null ? Ok(_movieService.GetById(id)) : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            return Created($"/api/movie/{_movieService.Add(movie)}", null);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] Movie movie)
        {
            movie.Id = id;
            return _movieService.Update(movie) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            return _movieService.Delete(id) ? NoContent() : NotFound();
        }
    }
}
