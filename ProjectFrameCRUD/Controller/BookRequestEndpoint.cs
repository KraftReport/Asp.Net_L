using Microsoft.AspNetCore.Mvc;
using ProjectFrameCRUD.DTO;
using ProjectFrameCRUD.Service;

namespace ProjectFrameCRUD.Controller
{
    public static class BookRequestEndpoint
    { 
        public static void BookRequestEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/createBook", async ([FromBody]APIRequestDTO apiRequestDTO,[FromServices] IBookService bookService) =>
            await bookService.RegisterBook(apiRequestDTO) is true ?
            Results.Ok("Ok") : Results.Ok("no Ok"));

            app.MapGet("/getAll", async ([FromServices]IBookService bookService) =>
            Results.Ok(await bookService.GetAllBook()));

            app.MapPost("/findById", async ([FromBody]APIRequestDTO APIRequestDTO,[FromServices] IBookService bookService) =>
            Results.Ok(await bookService.FindBookById(APIRequestDTO)));

            app.MapPut("/updateBook", async ([FromBody]APIRequestDTO apiRequestDTO,[FromServices] IBookService bookService) =>
            Results.Ok(await bookService.UpdateBook(apiRequestDTO)));

            app.MapDelete("/deleteBook", async ([FromBody]APIRequestDTO apiRequestDTO,[FromServices] IBookService bookService) =>
            Results.Ok(await bookService.DeleteBook(apiRequestDTO)));
        }
    }
}
