using Microsoft.AspNetCore.Mvc;
using ProjectFrameCRUD.Model.RequestModel;
using ProjectFrameCRUD.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ProjectFrameCRUD.Endpoint
{
    public static class AuthEndpoint
    {
        public static void AuthEndpoints(this IEndpointRouteBuilder app)
        {
            var commonRoute = app.MapGroup("/api/auth");
             
            commonRoute.MapPost("/register-user", RegisterUser).AllowAnonymous();
            commonRoute.MapPost("/login-user", LoginUser).AllowAnonymous(); 
            commonRoute.MapPost("/refresh-token", RefreshToken).RequireAuthorization();

            static async Task<IResult> RegisterUser([FromBody] APIRequestModel requestModel, [FromServices] IAuthService authService)
            {
                var result = await authService.Register(requestModel.User);
                return Results.Ok(result);
            }

            static async Task<IResult> LoginUser([FromBody] APIRequestModel requestModel, [FromServices] IAuthService authService)
            {
                var result = await authService.Login(requestModel.User.Email, requestModel.User.Password);
                return result.IsSuccess ? Results.Ok(result) : Results.Unauthorized();
            }

            [Authorize]  
            static async Task<IResult> RefreshToken([FromBody] APIRequestModel requestModel, [FromServices] IAuthService authService)
            {
                var result = await authService.RefreshToken(requestModel.token);
                return result.IsSuccess ? Results.Ok(result) : Results.Unauthorized();
            }
        }
    }
}
