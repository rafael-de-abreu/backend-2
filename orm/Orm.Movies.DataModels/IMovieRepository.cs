namespace Orm.Movies.DataModels
{
    public interface IMovieRepository
    {
        Guid Add(Movie movie);

        bool Update(Movie movie);

        bool Delete(Guid id);

        Movie? GetById(Guid id);

        IEnumerable<Movie> GetAll();
    }
}
