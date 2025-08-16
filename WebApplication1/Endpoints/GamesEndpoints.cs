using System;
using WebApplication1.DTOs;

namespace WebApplication1.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDTO> games = [
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

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                       .WithParameterValidation();
        //Get games
        group.MapGet("/", () => games);

        //GET / games/1
        group.MapGet("/{id}", (int id) =>
        {
            GameDTO? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
            .WithName(GetGameEndpointName);

        //POST/ games
        group.MapPost("/", (CreateGameDTO newGame) =>
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
        

        //PUT / games
        group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDTO(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        }
        );

        //DELETE / games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        }
        );
        return group;
    }
}
