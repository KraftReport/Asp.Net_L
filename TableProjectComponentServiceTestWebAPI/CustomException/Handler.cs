namespace TableProjectComponentServiceTestWebAPI.CustomException
{
    public class Handler : IMiddleware
    {
        private readonly Logger logger;
        public Handler(Logger logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate request)
        {
            try
            {
                await request(context);
            }catch(DataNotFoundException ex)
            {
                await HandleExceptionAsync
                  (context, ex, 404, "no matching data is found with provided search information");
            }catch(Exception ex)
            {
                await HandleExceptionAsync
                  (context, ex, 500, "interl error 500 tat twr p :-P");
            }
     
        }
        public async Task HandleExceptionAsync(HttpContext context, Exception ex, int status, string message)
        {
            context.Response.StatusCode = status;
            logger.Info(ex.Message);
            var responseModel = ResponseModel.Builder()
                .HttpStatus(status)
                .Message(message)
                .Build();
            await context.Response.WriteAsJsonAsync(responseModel);
        }
    }
}
