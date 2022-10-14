using aspnetharjoitus;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Web-palvelu, joka ottaa yhteydén tietokantaan
builder.Services.AddDbContext<SuperAdventure>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "Tämä on get API-kutsu");

//Returns all status information from table Stat to the caller
app.MapGet("/superadventure", async (SuperAdventure context) =>
await context.Stats.ToListAsync());
//returns all status information to the caller (GET and PUT calls).
async Task<List<Stat>> GetAllStats(SuperAdventure context) => await context.Stats.ToListAsync();

//updates the status information to the Stats table at the request of the caller
app.MapPut("/superadventure/{id}", async (SuperAdventure context, Stat stat, int id) =>
{
    //Retrieves a record from the database based on the main key (id).
    var dbStat = await context.Stats.FindAsync(id);
    if (dbStat is null) return Results.NotFound("Ei tilatietoja. :/");

    //Defines the data to be updated
    dbStat.CurrentHitPoints = stat.CurrentHitPoints;
    dbStat.MaxHitPoints = stat.MaxHitPoints;
    dbStat.Gold = stat.Gold;
    dbStat.Exp = stat.Exp;
    dbStat.CurrentLocationID = stat.CurrentLocationID;

    //Updates status information
    await context.SaveChangesAsync();

    //if the recording went well, call the method GetAllStats();
    return Results.Ok(await GetAllStats(context));
});





app.Run();

