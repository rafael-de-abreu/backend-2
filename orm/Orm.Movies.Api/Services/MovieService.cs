using Orm.Movies.DataModels;

namespace Orm.Movies.Api.Services
{
    public class MovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie? GetById(Guid id)
        {
            return _movieRepository.GetById(id);
        }

        public IEnumerable<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }

        public Guid Add(Movie movie)
        {
            return _movieRepository.Add(movie);
        }

        public bool Update(Movie movie)
        {
            return _movieRepository.Update(movie);
        }

        public bool Delete(Guid id) 
        {
            return _movieRepository.Delete(id);
        }
    }
}
