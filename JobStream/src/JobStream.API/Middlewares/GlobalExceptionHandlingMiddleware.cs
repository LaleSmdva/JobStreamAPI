using JobStream.Business.Exceptions;
using System.Net;
using N=JobStream.Business.Exceptions; 

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
            catch (FileFormatException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (N.ArgumentNullException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (CreateRoleFailedException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (CreateUserFailedException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (DuplicateUserNameException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (DuplicateEmailException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (FileSizeException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (AlreadyExistsException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (RepeatedChoiceException ex)
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
