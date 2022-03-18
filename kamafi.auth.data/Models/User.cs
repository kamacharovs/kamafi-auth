using System.Text.Json.Serialization;

namespace kamafi.auth.data.models
{
    public class User : IEntity
    {
        public int UserId { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordSalt { get; set; }

        [JsonIgnore]
        public string RoleName { get; set; } = Roles.User;
        public virtual Role Role { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;

        [JsonIgnore]
        public virtual List<UserApiKey> ApiKeys { get; set; }
    }

    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool AddApiKey { get; set; } = true;
    }
}