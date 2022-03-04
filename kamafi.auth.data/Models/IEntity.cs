namespace kamafi.auth.data.models
{
    public interface IEntity
    {
        Guid PublicKey { get; set; }
        string RoleName { get; set; }
    }
}
