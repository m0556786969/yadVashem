using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace server;

public static class ItemEndpoints
{
    public static void MapItemEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Item", () =>
        {
            return new[] { new Item() };
        })
        .WithName("GetAllItems")
        .Produces<Item[]>(StatusCodes.Status200OK);

        routes.MapGet("/api/Item/{id}", (int id) =>
        {
            //return new Item { ID = id };
        })
        .WithName("GetItemById")
        .Produces<Item>(StatusCodes.Status200OK);

        routes.MapPut("/api/Item/{id}", (int id, Item input) =>
        {
            return Results.NoContent();
        })
        .WithName("UpdateItem")
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Item/", (Item model) =>
        {
            string jsonData = JsonConvert.SerializeObject(
            model.BackPath == null ?
            new
            {
                model.Title,
                model.CollectionSymbolization,
                model.imageIndex,
                model.Path
            }
        : model);

            string filePath = $"./JSON/{model.CollectionSymbolization}/{model.imageIndex}.json"; // שם קובץ לפי מספר הכרטיסיה


            // Write the JSON data to the file
            File.WriteAllText(filePath, jsonData);



            return Results.Created("WOW", model);
        })
        .WithName("CreateItem")
        .Produces<Item>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Item/{id}", (int id) =>
        {
            //return Results.Ok(new Item { ID = id });
        })
        .WithName("DeleteItem")
        .Produces<Item>(StatusCodes.Status200OK);
    }
}
