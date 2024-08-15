using Microsoft.AspNetCore.Mvc;
using System.Net;

public class HttpActionResult<T> : IActionResult
{
    private readonly string _message;
    private readonly HttpStatusCode _statusCode;
    private readonly T _result;

    public HttpActionResult(HttpStatusCode statusCode, string message, T result)
    {
        _statusCode = statusCode;
        _message = message;
        _result = result;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        if (_statusCode == HttpStatusCode.OK)
        {
            var OkResult = new OkObjectResult(new { message = _message, result = _result })
            {
                StatusCode = (int)_statusCode
            };

            await OkResult.ExecuteResultAsync(context);

            return;
        }

        var ErrorResult = new ObjectResult(new { message = _message })
        {
            StatusCode = (int)_statusCode
        };

        await ErrorResult.ExecuteResultAsync(context);
    }
}
