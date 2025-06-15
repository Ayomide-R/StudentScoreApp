using MovieApi.Models;

namespace MovieApi.Services;

public interface IMovieService
{
    IEnumerable<Movie> GetAll();
    Movie? GetById(int id);
    Movie Create(Movie movie);
    void Delete(int id);
}