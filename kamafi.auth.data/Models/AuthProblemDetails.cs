namespace kamafi.auth.data.models
{
    public class AuthProblemDetails
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        public string TraceId { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }

    public class AuthProblemDetailBase
    {
        public int? Code { get; set; }
        public string Message { get; set; }
    }
}
