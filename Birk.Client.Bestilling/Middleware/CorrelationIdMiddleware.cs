namespace Birk.Client.Bestilling.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string correlationId = Guid.NewGuid().ToString();
            context.Request.Headers.Add("correlation-id", correlationId);
            await _next(context);
        }
    }
}
