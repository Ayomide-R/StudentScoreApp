using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using MovieApi.Dtos;
using MovieApi.Services;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;

    public MoviesController(IMovieService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MovieReadDto>> GetAll()
    {
        var movies = _service.GetAll()
            .Select(m => new MovieReadDto { Id = m.Id, Title = m.Title, Genre = m.Genre, Year = m.Year, Rating = m.Rating });
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public ActionResult<MovieReadDto> GetById(int id)
    {
        var movie = _service.GetById(id);
        if (movie == null) return NotFound();

        return Ok(new MovieReadDto { Id = movie.Id, Title = movie.Title, Genre = movie.Genre, Year = movie.Year, Rating = movie.Rating });
    }

    [HttpPost]
    public ActionResult<MovieReadDto> Create(MovieCreateDto dto)
    {
        var movie = new Movie { Title = dto.Title, Genre = dto.Genre, Year = dto.Year, Rating = dto.Rating };
        var created = _service.Create(movie);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, new MovieReadDto { Id = created.Id, Title = created.Title, Genre = created.Genre, Year = created.Year, Rating = created.Rating });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var movie = _service.GetById(id);
        if (movie == null) return NotFound();
        _service.Delete(id);
        return NoContent();
    }
}