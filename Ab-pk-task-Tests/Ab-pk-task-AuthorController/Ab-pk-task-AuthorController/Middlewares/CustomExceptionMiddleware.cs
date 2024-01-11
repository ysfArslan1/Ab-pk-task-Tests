using Ab_pk_task3.Services;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace Ab_pk_task3.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
         private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP: " + context.Request.Method + " - " + context.Request.Path;
               _loggerService.Write(message);

                await _next.Invoke(context);
                watch.Stop();

                message = "[Response] HTTP: " + context.Request.Method + " - " + context.Request.Path +
                    " - " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
            

        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error] HTTP: " + context.Request.Method + " - " + context.Response.StatusCode +
                    " Error Mesage: " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);

            
            var result =JsonConvert.SerializeObject(new { error  = ex.Message },Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
    static public class CustomExceptionMiddlewareExtention
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
