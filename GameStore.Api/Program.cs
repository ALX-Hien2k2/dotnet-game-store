using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Register Database service
var connString = builder.Configuration.GetConnectionString("GameStore");  // Get connection string
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

// Run migration
app.MigrationDb();

// /games enpoint
app.MapGamesEndpoints();

// Run app
app.Run();
