using System.Net;

namespace PineAPP.Models;

public struct ApiResponse<T>
{
    public HttpStatusCode StatusCode { get; private set; }
    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }
    public T Result { get; private set; }

    public ApiResponse(HttpStatusCode statusCode, bool isSuccess, T result = default, string errorMessage = default)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Result = result;
    }
}