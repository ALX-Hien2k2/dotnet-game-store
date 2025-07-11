using GameStore.Api.Dtos;
using GameStore.Api.Middlewares.Filters;

namespace GameStore.Api.Endpoints;

// Extension class: Contains all endpoints of Games
public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (
            1,
            "Street Fighter II",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 15)
        ),
        new (
            2,
            "The Legend of Zelda: Ocarina of Time",
            "Action-Adventure",
            29.99M,
            new DateOnly(1998, 11, 21)
        ),
        new (
            3,
            "Super Mario Bros.",
            "Platformer",
            14.99M,
            new DateOnly(1985, 9, 13)
        ),
        new (
            4,
            "Halo: Combat Evolved",
            "Shooter",
            39.99M,
            new DateOnly(2001, 11, 15)
        ),
        new (
            5,
            "Minecraft",
            "Sandbox",
            26.95M,
            new DateOnly(2011, 11, 18)
        ),
        new (
            6,
            "The Witcher 3: Wild Hunt",
            "RPG",
            49.99M,
            new DateOnly(2015, 5, 19)
        )
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)  // Extending class "WebApplication" -> Add method MapGamesEndpoints() to class "WebApplication"
    {
        var group = app.MapGroup("games");

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        }).AddEndpointFilter<ValidationFilter<CreateGameDto>>();

        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id); // If found -> return index; else -> return -1
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
            // return Results.Ok(games[index])
        }).AddEndpointFilter<ValidationFilter<UpdateGameDto>>();

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return group;
    }
}
