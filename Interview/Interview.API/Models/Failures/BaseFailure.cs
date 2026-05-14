namespace Interview.API.Models.Failures
{
    public class BaseFailure
    {
        public HttpStatusCode Code { get; init; }

        public string Message { get; init; } = string.Empty;

        public string TraceId { get; set; } = string.Empty;

        public Exception? Exception { get; init; }

        public dynamic? Data { get; init; }

        public List<BaseFailure>? Details { get; init; }
    }
}