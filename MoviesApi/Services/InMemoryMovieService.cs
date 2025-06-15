using MovieApi.Models;

namespace MovieApi.Services;

public class InMemoryMovieService : IMovieService
{
    private readonly List<Movie> _movies;

    public InMemoryMovieService()
    {
        _movies = Enumerable.Range(1997, 29).Select((year, index) => new Movie
        {
            Id = index + 1,
            Title = $"Movie {index + 1}",
            Genre = index % 2 == 0 ? "Action" : "Drama",
            Year = year,
            Rating = Math.Round(5 + (index % 5) + 0.3 * (index % 3), 1)
        }).ToList();
    }

    public IEnumerable<Movie> GetAll() => _movies;

    public Movie? GetById(int id) => _movies.FirstOrDefault(m => m.Id == id);

    public Movie Create(Movie movie)
    {
        movie.Id = _movies.Max(m => m.Id) + 1;
        _movies.Add(movie);
        return movie;
    }

    public void Delete(int id)
    {
        var movie = GetById(id);
        if (movie != null) _movies.Remove(movie);
    }
}