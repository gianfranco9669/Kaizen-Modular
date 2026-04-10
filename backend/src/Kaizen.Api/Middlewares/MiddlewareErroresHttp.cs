using System.Net;
using System.Text.Json;
using Kaizen.Aplicacion.Compartido;

namespace Kaizen.Api.Middlewares;

public class MiddlewareErroresHttp
{
    private readonly RequestDelegate _next;

    public MiddlewareErroresHttp(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidacionNegocioException ex)
        {
            context.Response.StatusCode = ex.CodigoHttp;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
        }
        catch (Exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Error interno no controlado." }));
        }
    }
}
