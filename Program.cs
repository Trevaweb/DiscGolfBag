using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DiscDb>(opt => opt.UseInMemoryDatabase("DiscList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "DiscAPI";
    config.Title = "DiscAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "DiscAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

var plantItems = app.MapGroup("/discbag");

plantItems.MapGet("/", GetAllDiscs);
plantItems.MapGet("/{id}", GetDiscById);
plantItems.MapGet("/discname/{name}", GetDiscByName);
plantItems.MapGet("/search", GetDiscsByColorAndName);
plantItems.MapPost("/", CreateDisc);
plantItems.MapPut("/{id}", UpdateDisc);
plantItems.MapDelete("/{id}", DeleteDisc);

app.Run();

static async Task<IResult> GetAllDiscs(DiscDb db)
{
    return TypedResults.Ok(await db.Discs.ToArrayAsync());
}

static async Task<IResult> GetDiscById(int id , DiscDb db)
{
    return await db.Discs.FindAsync(id)
        is Disc disc
            ? TypedResults.Ok(disc)
            : TypedResults.NotFound();
}

static async Task<IResult> GetDiscByName(string name , DiscDb db)
{
    var discs = await db.Discs.Where(disc => disc.Name == name).ToArrayAsync();
    return discs.Any()
        ? TypedResults.Ok(discs)
        : TypedResults.NotFound();
 
}

static async Task<IResult> CreateDisc(Disc disc, DiscDb db)
{
    db.Discs.Add(disc);
    await db.SaveChangesAsync();
    
    return TypedResults.Created($"/Disc/{disc.Id}",disc);
}

static async Task<IResult> UpdateDisc(int id, Disc inputDisc, DiscDb db)
{
    var Disc = await db.Discs.FindAsync(id);
    
    if (Disc is null) return TypedResults.NotFound();
    Disc.Name = inputDisc.Name;
    Disc.Color = inputDisc.Color;
    Disc.FlightNumbers = inputDisc.FlightNumbers;
    await db.SaveChangesAsync();
    

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteDisc(int id, DiscDb db)
{
    if (await db.Discs.FindAsync(id) is Disc Disc)
    {
        db.Discs.Remove(Disc);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}