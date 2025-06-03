using Orm.Movies.DataModels;

namespace Orm.Movies.Ef
{
    public class MovieEfRepository : IMovieRepository
    {
        private readonly AppDbContext _appDbContext;

        public MovieEfRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Guid Add(Movie movie)
        {
            movie.Id = Guid.NewGuid();
            _appDbContext.Add(movie);
            _appDbContext.SaveChanges();
            return movie.Id.Value   ;
        }

        public bool Delete(Guid id)
        {
            var movie = _appDbContext.Movies.FirstOrDefault(m => m.Id == id);
            if(movie == null)
                {
                return false;
            }
            _appDbContext.Movies.Remove(movie);
            _appDbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _appDbContext.Movies.OrderBy(x => x.Name);
        }

        public Movie? GetById(Guid id)
        {
            return _appDbContext.Movies.FirstOrDefault(m => m.Id == id);
        }

        public bool Update(Movie movie)
        {
            var existingMovie = _appDbContext.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (existingMovie == null)
            {
                return false;
            }
            existingMovie.Name = movie.Name;
            existingMovie.Year = movie.Year;
            _appDbContext.Movies.Update(existingMovie);
            _appDbContext.SaveChanges();
            return true;
        }
    }
}
