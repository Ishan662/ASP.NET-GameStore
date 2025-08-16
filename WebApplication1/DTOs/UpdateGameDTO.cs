namespace WebApplication1.DTOs;

public record class UpdateGameDTO(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

