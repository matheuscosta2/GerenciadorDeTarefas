using System.Net;

namespace GerenciadorDeTarefas.API.Middlewares;

public class MiddlewareTratamentoErros
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MiddlewareTratamentoErros> _logger;

    public MiddlewareTratamentoErros(RequestDelegate next, ILogger<MiddlewareTratamentoErros> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu uma exceção não tratada.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            ArgumentException => (int)HttpStatusCode.BadRequest,
            InvalidOperationException => (int)HttpStatusCode.Conflict,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Mensagem = context.Response.StatusCode switch
            {
                (int)HttpStatusCode.BadRequest => "Requisição inválida.",
                (int)HttpStatusCode.Conflict => "Conflito na operação.",
                _ => "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde."
            },
            Detalhes = exception.Message
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}