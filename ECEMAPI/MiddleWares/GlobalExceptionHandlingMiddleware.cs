using ECMDomain.Abstraction;
using ECMDomain.Exceptions;
using Serilog.Context;
namespace ECEMAPI.MiddleWares;
public class GlobalExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger = logger;
   
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception) 
        {
            var exceptionDetails = GetExceptionDetails(exception);
            //** After USe  Middleware Inject Correlation Id Using LogContext With PushProperty
            using (LogContext.PushProperty("Error", exceptionDetails.Errors!.ErrorMessages, true))
            {
                _logger.LogError(exception, "Exception occured: {Message}", exception.Message);
            }
            context.Response.StatusCode = exceptionDetails.StatusCode;

            await context.Response.WriteAsJsonAsync(exceptionDetails);
        }
    }

    private static Result<NoContentDto> GetExceptionDetails(Exception exception) =>
        exception switch
        {  
            RequestValidationException validationException
            => Result<NoContentDto>.Failed(
                   StatusCodes.Status400BadRequest,validationException.Errors), 

            ConcurrencyException concurrencyException
            => Result<NoContentDto>.Failed(
                   StatusCodes.Status400BadRequest, concurrencyException.Errors),
          
            NullObjectException nullObjectException
            => Result<NoContentDto>.Failed(
                   StatusCodes.Status400BadRequest, nullObjectException.Errors),
           
            BadRequestException badRequestException
            => Result<NoContentDto>.Failed(
                   StatusCodes.Status400BadRequest, badRequestException.Errors),
            
            PayloadFormatException payLoadFormatException
            => Result<NoContentDto>.Failed(
                   StatusCodes.Status400BadRequest, payLoadFormatException.Errors),
            
           _ => Result<NoContentDto>.Failed(
                    StatusCodes.Status500InternalServerError,
                   new Error
                    {
                        ErrorCode = "Internal Server Error",
                        ErrorMessages = ["Please see an advise"]
                    })

            //IBadRequest badRequestException
            //    => Result<NoContentDto>.Failed(
            //        StatusCodes.Status400BadRequest,
            //        badRequestException.Errors),

            //IINternalServerError InternalServerException
            //    => Result<NoContentDto>.Failed(
            //        StatusCodes.Status500InternalServerError,
            //        InternalServerException.Errors),

            //_ => Result<NoContentDto>.Failed(
            //        StatusCodes.Status500InternalServerError,
            //        new Error
            //        {
            //            ErrorCode = "Internal Server Error",
            //            ErrorMessages = ["Please seek an advise"]
            //        })
        };
}
