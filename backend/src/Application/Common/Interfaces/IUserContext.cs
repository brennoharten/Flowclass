namespace Application.Common.Interfaces
{
    public interface IUserContext
    {
        string Email { get; }
        Guid TenantId { get; }
        string Role { get; } // opcional, mas útil
    }
}
