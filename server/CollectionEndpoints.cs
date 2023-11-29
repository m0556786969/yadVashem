namespace server;
public static class CollectionEndpoints
{
    public static List<Collection> collections = new();
    public static void MapCollectionEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Collection", () =>
        {
            return collections;
        })
        .WithName("GetAllCollections")
        .Produces<Collection[]>(StatusCodes.Status200OK);

        routes.MapGet("/api/Collection/{id}", (int id) =>
        {
            //var a = new { Title: ;
            return new
            {
                Title = Program.GetNum(id),
                newImgNumber = Program.LastNumImge(id) + 1
            };
            //var c = collections.FirstOrDefault(c => c.CollectionSymbolization == id);
            //if (c == default) return null;
            //return c;
        })
        .WithName("GetCollectionById")
        .Produces<Object>(StatusCodes.Status200OK);

        routes.MapPut("/api/Collection/{id}", (int id, Collection input) =>
        {
            return Results.NoContent();
        })
        .WithName("UpdateCollection")
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Collection/", (Collection model) =>
        {
            //return Results.Created($"/api/Collections/{model.ID}", model);
        })
        .WithName("CreateCollection")
        .Produces<Collection>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Collection/{id}", (int id) =>
        {
            //return Results.Ok(new Collection { ID = id });
        })
        .WithName("DeleteCollection")
        .Produces<Collection>(StatusCodes.Status200OK);
    }
}
