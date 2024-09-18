using ExcelServiceApp.User.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExcelServiceApp.User.Endpoints;

public static class AuthEndpoints
{
    public static void AuthEndpoint(this IEndpointRouteBuilder app)
    {
        var common = app.MapGroup("/api/auth");
        common.MapPost("/register", RegisterUser).AllowAnonymous();
        common.MapPost("/login", LoginUser).AllowAnonymous();
        common.MapPost("/refresh-token", RefreshToken).RequireAuthorization().DisableAntiforgery();
        common.MapPost("/secure-data", SecureData).RequireAuthorization();

        static async Task<IResult> RegisterUser([FromBody]UserAuthApiRequest registerRequest,[FromServices]IAuthService authService)
        {
            return Results.Ok(await authService.RegisterUserAsync(registerRequest));
        }

        static async Task<IResult> LoginUser(UserAuthApiRequest userAuthApiRequest, IAuthService authService)
        {
            return Results.Ok(await authService.LoginUserAsync(userAuthApiRequest));
        }

        static async Task<IResult> RefreshToken([FromForm] string refreshToken, IAuthService authService)
        {
            return Results.Ok(await authService.RefreshToken(refreshToken));
        }

        static async Task<IResult> SecureData()
        {
            return Results.Ok("secure data");
        }
    }
}