using Newtonsoft.Json;
using server;
using System.Reflection;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

CollectionEndpoints.collections.Add(
    new Collection()
    {
        CollectionSymbolization = 10,
        Title = "hi bye",
        NextImage = 15
    });
CollectionEndpoints.collections.Add(new Collection
{
    CollectionSymbolization = 102,
    Title = "hi 123",
    NextImage = 1500
});
CollectionEndpoints.collections.Add(new Collection
{
    CollectionSymbolization = 120,
    Title = "hi wow",
    NextImage = 150
}
    );

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//����� ����� �����
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
            .WithOrigins("https://localhost:4200", "http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            ;
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapPost("/api/Image/{collection}/{imageName}", async (string collection, string imageName,HttpContext context) =>
{
    var file = context.Request.Form.Files.GetFile("image");

    if (file != null && file.Length > 0)
    {
        // Access additional data
        /*var additionalDataJson = context.Request.Form["data"];
        var additionalData = System.Text.Json.JsonSerializer.Deserialize<Item>(additionalDataJson);
        var model = JsonConvert.SerializeObject(additionalDataJson);*/
        //save here

        var filePath = Path.Combine($@"./images/{collection}", imageName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        await context.Response.WriteAsync("File uploaded successfully.");
        // Process the file and additional data here

        return Results.Ok(new { message = "Image uploaded successfully" });
    }

    return Results.BadRequest("No file or empty file received");
});


app.MapItemEndpoints();

app.MapCollectionEndpoints();

app.Run();
