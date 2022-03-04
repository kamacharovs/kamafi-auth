namespace kamafi.auth.data
{
    public class AuthDbOptions
    {
        public const string Section = "AuthDb";

        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }

        public string ConnectionString => $"Server={Host};Username={Username};Password={Password};Database={DatabaseName}";
    }
}
