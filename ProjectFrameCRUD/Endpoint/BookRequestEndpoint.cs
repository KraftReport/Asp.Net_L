using Microsoft.AspNetCore.Mvc;
using ProjectFrameCRUD.Model.RequestModel;
using ProjectFrameCRUD.Model.ResponseModel;
using ProjectFrameCRUD.Service;
using System.Security.Cryptography.X509Certificates;

namespace ProjectFrameCRUD.Endpoint
{
    public static class BookRequestEndpoint
    { 
        public static void BookRequestEndpoints(this IEndpointRouteBuilder app)
        {
            var commonRoute = app.MapGroup("/api/book/").WithOpenApi();

            commonRoute.MapPost("/create-book",CreateBook).WithName("CreateBook").RequireAuthorization();
            commonRoute.MapGet("/get-all-books", GetAllBook).WithName("GetAllBooks").RequireAuthorization();
            commonRoute.MapGet("/get-book-by-id/{id}", GetBookById).WithName("GetBookById").RequireAuthorization();
            commonRoute.MapPut("/update-book", UpdateBook).WithName("UpdateBook").RequireAuthorization();
            commonRoute.MapDelete("/delete-book/{id}", DeleteBook).WithName("DeleteBook").RequireAuthorization();




            static async Task<IResult> 
                CreateBook([FromBody] APIRequestModel apiRequestModel, [FromServices] IBookService bookService) =>
                await bookService.RegisterBook(apiRequestModel) is true ?
                Results.Ok(Result<bool>.Success("book is created successfully")) :
                Results.Ok(Result<bool>.Fail("book is failed to register"));

            static async Task<IResult>
                GetAllBook(IBookService bookService) =>
                await bookService.GetAllBook() is APIResponseModel model ?
                Results.Ok(Result<APIResponseModel>.Success("book list", model)) :
                Results.Ok(Result<APIResponseModel>.Fail("fail to reterive the book list"));

            static async Task<IResult>
                GetBookById(int id, IBookService bookService) =>
                await bookService.FindBookById(new APIRequestModel { Id = id }) is APIResponseModel model ?
                Results.Ok(Result<APIResponseModel>.Success("book is returned", model)) :
                Results.Ok(Result<APIResponseModel>.Fail("something wrong"));

            static async Task<IResult> UpdateBook(APIRequestModel apiRequestModel, IBookService bookService) =>
                await bookService.UpdateBook(apiRequestModel) is true ?
                Results.Ok(Result<bool>.Success("book is updated", true)) :
                Results.Ok(Result<bool>.Fail("fail to update", false));

            static async Task<IResult>
                DeleteBook(int id, IBookService bookService) =>
                await bookService.DeleteBook(new APIRequestModel { Id = id }) is true ?
                Results.Ok(Result<bool>.Success("book is deleted successfully")) :
                Results.Ok(Result<bool>.Fail("book is failed to delete"));


 
        }

         
    }
}
