using WebApplication1.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDTO> games = [
new (
        1,
        "Elden Ring",
        "Action RPG",
        59.99m,
        new DateOnly(2022, 2, 25)
    ),
    new (
        2,
        "Minecraft",
        "Sandbox",
        26.95m,
        new DateOnly(2011, 11, 18)
    ),
    new (
        3,
        "The Witcher 3: Wild Hunt",
        "RPG",
        39.99m,
        new DateOnly(2015, 5, 19)
    )
];

//Get games
app.MapGet("games", () => games);

//GET / games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
    .WithName(GetGameEndpointName);

//POST/ games
app.MapPost("games", (CreateGameDTO newGame) =>
{
    GameDTO game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

app.MapGet("/", () => "Hello World!");

app.Run();
