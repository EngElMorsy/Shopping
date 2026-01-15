using ECEMAPI.MiddleWares;

namespace ECEMAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app) =>
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    public static void UseRequestContextLogging(this IApplicationBuilder app) =>
    app.UseMiddleware<RequestContextLoggingMiddleware>();

}