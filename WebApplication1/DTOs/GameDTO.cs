namespace WebApplication1.DTOs;

public record class GameDTO(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);
