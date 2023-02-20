using Microsoft.AspNetCore.Mvc;

namespace Birk.Client.Bestilling.Models.HttpResults
{
    public class HttpResult<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }
        public ProblemDetails ProblemDetails { get; }

        public HttpResult(bool isSuccess, T data, ProblemDetails problemDetails = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            ProblemDetails = problemDetails;
        }
    }
}
