namespace kamafi.auth.data.models
{
    public class UserApiKey
    {
        public int UserId { get; set; }
        public string ApiKey { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}
