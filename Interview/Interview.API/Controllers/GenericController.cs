namespace Interview.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    public class GenericController : Controller
    {
        [NonAction]
        public IActionResult GenericContent<T>(
            (BaseFailure? failure, T? data) convertYetResult,
            Func<T?, IActionResult>? result = null
        )
        {
            if (convertYetResult.failure is not null)
            {
                return FailureContent(convertYetResult.failure);
            }
            result ??= data => Ok(data);
            return result(convertYetResult.data);
        }

        [NonAction]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult FailureContent(BaseFailure failure)
        {
            if (string.IsNullOrWhiteSpace(failure.TraceId))
            {
                failure.TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            }

            return FailureResultMap(failure);
        }

        private ObjectResult FailureResultMap(BaseFailure failure)
        {
            return failure.Code switch
            {
                HttpStatusCode.BadRequest => BadRequest(failure),
                HttpStatusCode.Unauthorized => Unauthorized(failure),
                HttpStatusCode.Forbidden => StatusCode((int)HttpStatusCode.Forbidden, failure.Message),
                HttpStatusCode.NotFound => NotFound(failure),
                HttpStatusCode.MethodNotAllowed => StatusCode((int)HttpStatusCode.MethodNotAllowed, failure),
                HttpStatusCode.Conflict => Conflict(failure),
                HttpStatusCode.Locked => StatusCode((int)HttpStatusCode.Locked, failure),
                HttpStatusCode.InternalServerError => StatusCode((int)HttpStatusCode.InternalServerError, failure),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, failure),
            };
        }
    }
}
