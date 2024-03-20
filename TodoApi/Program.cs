using Microsoft.AspNetCore.Authorization.Infrastructure;
using TodoApi;
using System.Web.Http.Cors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddScoped<ToDoDbContext>();
builder.Services.AddDbContext<ToDoDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseCors("corsapp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});


////////////////////////////////
app.MapGet("/todoitems", async (ToDoDbContext db) =>
    await db.Items.ToListAsync());

app.MapGet("/todoitems/complete", async (ToDoDbContext db) =>
    await db.Items.Where(t => t.IsCompliete == true).ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, ToDoDbContext db) =>
    await db.Items.FindAsync(id)
        is Item todo
            ? Results.Ok(todo)
            : Results.NotFound());

app.MapPost("/todoitems/{name}", async (string name,ToDoDbContext db) =>
{
       var newItem = new Item (name);
    db.Items.Add(newItem);
    await db.SaveChangesAsync();
    // return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}/{IsCompliete}", async (int id, bool IsCompliete, ToDoDbContext db) =>
{
        // Logger<m>.LogInformation($"Todo updated successfully.");
    var todo = await db.Items.FindAsync(id);
    if (todo is null) return Results.NotFound();
    todo.IsCompliete = IsCompliete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/todoitems/{id}", async (int id, ToDoDbContext db) =>
{
    if (await db.Items.FindAsync(id) is Item todo)
    {
        db.Items.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();
