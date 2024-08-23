using CustomCookieAuth.Entities;
using CustomCookieAuth.Models;
using CustomCookieAuth.Services;

namespace CustomCookieAuth.MinimalApis
{
    public static class ProductMinimalApi
    {
        public static void ProductMinimalApiRoutes(this IEndpointRouteBuilder builder)
        {

            var productMap = builder.MapGroup("/product");

            productMap.MapPost("/createProduct", CreateProduct).RequireAuthorization("admin","authenticated").WithName("CP");
            productMap.MapGet("/{Id}", FindProduct).RequireAuthorization("authenticated").WithName("FP");
            productMap.MapGet("/",GetProducts).RequireAuthorization("authenticated").WithName("GP");
            productMap.MapPut("/",UpdateProduct).RequireAuthorization("admin", "authenticated").WithName("UP");
            productMap.MapDelete("/",DeleteProduct).RequireAuthorization("admin", "authenticated").WithName("DP");

            static async Task<IResult> 
                CreateProduct(ProductDTO product, ProductService productService,LinkGenerator link) =>
                await productService.CreateProduct(product)  is int id?
                TypedResults.Created(link.GetPathByName("FP",values:id), product) : TypedResults.BadRequest();

            static async Task<IResult> 
                FindProduct(int Id,ProductService productService) =>
                await productService.FindProduct(Id) is Product product ? 
                TypedResults.Ok(new ProductDTO(product)) : TypedResults.NotFound();
            
            static async Task<IResult> 
                GetProducts(ProductService productService) =>
                await productService.GetProducts() is List<Product> products ?
                TypedResults.Ok(products.Select(p => new ProductDTO(p)).ToList()) : TypedResults.NotFound();

            static async Task<IResult> 
                UpdateProduct(ProductDTO product, ProductService productService) =>
                await productService.UpdateProduct(product) is Product updated ?
                TypedResults.Ok(new ProductDTO(updated)) : TypedResults.NotFound();

            static async Task<IResult> 
                DeleteProduct(int Id, ProductService productService) =>
                await productService.DeleteProduct(Id) is true ?
                TypedResults.Ok(Id + " is deleted ") : TypedResults.NotFound();

/*          productMap.MapPost("/createProduct", async (ProductDTO product, ProductService productService) =>
            await productService.CreateProduct(product) is true ?
            Results.Created($"/{product.Id}", product) : Results.BadRequest());
 
            productMap.MapGet("/{id}", async (int id, ProductService productService) =>
            await productService.FindProduct(id) is Product product ?
            Results.Ok(product) : Results.NotFound());

            productMap.MapGet("/", async (ProductService productService) =>
            await productService.GetProducts());

            productMap.MapPut("/", async (ProductService productService, ProductDTO productDTO) =>
            await productService.UpdateProduct(productDTO) is Product product ?
            Results.Ok(product) : Results.BadRequest());

            productMap.MapDelete("/",async (ProductService productService,int Id)=>
            await productService.DeleteProduct(Id) is true ?
            Results.Ok(Id) : Results.BadRequest()); */
        }
    }
}
