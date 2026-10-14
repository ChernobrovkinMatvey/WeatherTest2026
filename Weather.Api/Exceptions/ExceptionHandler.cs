using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Weather.Domain;

namespace Weather.Api.Exceptions;

/// <summary>
/// Глобальный обработчик исключений для API-контроллеров.
/// Перехватывает все необработанные исключения конвейера, маппит их на
/// корректный HTTP-код и возвращает ответ в стандартном формате ProblemDetails (RFC 7807).
/// </summary>
public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly bool _isDevelopment;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        // Определяем окружение один раз при создании обработчика
        _isDevelopment = environment.IsDevelopment();
    }

    /// <summary>
    /// Пытается обработать возникшее исключение.
    /// </summary>
    /// <param name="httpContext">Контекст текущего HTTP-запроса/ответа.</param>
    /// <param name="exception">Необработанное исключение, возникшее в конвейере.</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции.</param>
    /// <returns>
    /// <c>true</c>, если запрос обработан и дальше по конвейеру двигаться не нужно.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Маппинг типа исключения на HTTP-статус:
        //   - ошибки валидации FluentValidation -> 400 Bad Request
        //   - отсутствие сущности -> 404 Not Found
        //   - всё остальное (непредвиденное) -> 500 Internal Server Error
        var statusCode = exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            EntityNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        // Логируем только настоящие 500-е — остальные (4xx) не являются сбоями сервера
        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Unhandled exception occurred");

        httpContext.Response.StatusCode = statusCode;

        // Формируем стандартный ответ об ошибке (ProblemDetails)
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = ReasonPhrases.GetReasonPhrase(statusCode),
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };

        // Детали стека отдаём наружу только в Development-окружении,
        // чтобы не раскрывать внутренности приложения в Production
        if (_isDevelopment)
            problemDetails.Extensions["trace"] = exception.StackTrace;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        // Возврат true означает: исключение обработано, запрос завершён
        return true;
    }
}
