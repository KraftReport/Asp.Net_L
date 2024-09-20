using Microsoft.AspNetCore.Mvc;

namespace StreamLab.MP3;

public static class Mp3Endpoint
{
    public static void Mp3Endpoints(this IEndpointRouteBuilder app)
    {
        var common = app.MapGroup("/api/mp3");
        common.MapPost("/cut-mp3-file-into-segments", CutMp3FileIntoSegments);

        static async Task<IResult> CutMp3FileIntoSegments([FromBody] ApiRequestDto apiRequestDto,
            [FromServices] Mp3Service mp3Service)
        {
            if (apiRequestDto.InputFilePath != null) await mp3Service.StreamSong(apiRequestDto.InputFilePath);
            return Results.Ok(true);
        }
    }
}