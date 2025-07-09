using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
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

// GET /games
app.MapGet("games", () => games);

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id));

app.Run();
