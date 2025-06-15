namespace MovieApi.Dtos;

public class MovieCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int Year { get; set; }
    public double Rating { get; set; }
}