using ExcelServiceApp.User.Model;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExcelServiceApp.User.Endpoints;

public static class AuthEndpoints
{
    public static void AuthEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/auth");

        app.MapPost("/register", RegisterUser).AllowAnonymous();
        app.MapPost("/login", LoginUser).AllowAnonymous();

        static async Task<IResult> RegisterUser([FromBody]UserAuthApiRequest registerRequest,[FromServices]IAuthService authService)
        {
            return Results.Ok(await authService.RegisterUserAsync(registerRequest));
        }

        static async Task<IResult> LoginUser(UserAuthApiRequest userAuthApiRequest, IAuthService authService)
        {
            return Results.Ok(await authService.LoginUserAsync(userAuthApiRequest));
        }
    }
}