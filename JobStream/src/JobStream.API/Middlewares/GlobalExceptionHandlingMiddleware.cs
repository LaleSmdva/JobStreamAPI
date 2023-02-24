using JobStream.Business.Exceptions;
using System.Net;

namespace JobStream.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                 context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
