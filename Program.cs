using Microsoft.EntityFrameworkCore;
using MinimalAPI.Commands;
using MinimalAPI.Model;

var builder = WebApplication.CreateBuilder(args);
// definition of DataContext
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("DbDog"));
// definition of Dependency Injection
builder.Services.AddScoped<IDogCommands, DogCommands>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Definition Get Method
app.MapGet("/dog", async (IDogCommands commands) =>
    await commands.GetAllDogs());

// Definition Get{Id} Method
app.MapGet("/dog/{id}", async (Guid id, IDogCommands commands) =>
{
    var dog = await commands.GetDogById(id);

    if (dog == null) return Results.NotFound();

    return Results.Ok(dog);
});

// Definition Post Method
app.MapPost("/dog", async (Dog dog, IDogCommands commands) =>
{
    await commands.AddDog(dog);
    await commands.Save();

    return Results.Ok();
});

// Definition Put Method
app.MapPut("/dog/{id}", async (Guid id, Dog dog, IDogCommands commands) =>
{
    var updateOk = await commands.UpdateDog(dog, id);

    if (!updateOk) return Results.NotFound();

    return Results.NoContent();
});

// Definition Delete Method
app.MapDelete("/dog/{id}", async (Guid id, IDogCommands commands) =>
{
    var deleteOk = await commands.DeleteDog(id);
    if (deleteOk)
    {
        await commands.Save();
        return Results.Ok();
    }

    return Results.NotFound();
});

app.Run();
