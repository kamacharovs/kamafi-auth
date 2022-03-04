namespace kamafi.auth.data.models
{
    public class Role
    {
        public string RoleName { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public static class Roles
    {
        public const string Admin = "admin";
        public const string User = "user";
        public const string Client = "client";
    }
}
