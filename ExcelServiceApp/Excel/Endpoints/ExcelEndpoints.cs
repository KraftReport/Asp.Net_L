﻿using ExcelServiceApp.Excel.Model;
using ExcelServiceApp.Excel.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExcelServiceApp.Excel.Endpoints
{
    public static class ExcelEndpoints
    {
        public static void ExcelEndpointMethod(this IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/excel-service");

            app.MapPost("/create-excel-file", CreateSpreadsheetFile);
            app.MapPost("/read-excel-file", ReadSpreadsheetFile);
            app.MapPost("/generate-pdf", GeneratePdf);
            app.MapPost("/generate-img",GenerateImg);

            static async Task<IResult> CreateSpreadsheetFile([FromBody]SpreadsheetCreateRequestDTO spreadsheetCreateRequestDTO, [FromServices]IExcelService excelService)
            {
                return Results.Ok(await excelService.CreateSpreadsheetFile(spreadsheetCreateRequestDTO));
            }

            static IResult ReadSpreadsheetFile([FromBody]SpreadsheetCreateRequestDTO spreadsheetCreateRequestDTO,IExcelService excelService)
            {
                return Results.Ok(excelService.ReadExcelFile(spreadsheetCreateRequestDTO.FilePath));
            }

            static async Task<IResult> GeneratePdf([FromBody]SpreadsheetCreateRequestDTO spreadsheetCreateRequestDTO,IExcelService excelService)
            {
                return Results.Ok(await excelService.GeneratePdf(spreadsheetCreateRequestDTO.FilePath));
            }

            static async Task<IResult> GenerateImg([FromBody]SpreadsheetCreateRequestDTO spreadsheetCreateRequestDTO,IExcelService excelService)
            {
                return Results.Ok(await excelService.GenerateImage(spreadsheetCreateRequestDTO.FilePath));
            }
        }
    }
}