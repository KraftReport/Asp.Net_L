namespace PlateDirectPaymentApi.DirectPaymentModule.Exception
{
    public class GlobalExpectionHandler : IMiddleware
    {

        async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(MemberServiceRequestInvalidException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (PaymentServiceRequestInvalidException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch(TransactionServiceRequestInvalidException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, System.Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new { Message = ex.Message };
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }
}
