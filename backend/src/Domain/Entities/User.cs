namespace Domain.Entities;

public enum UserRole { Student = 1, Teacher = 2, Admin = 3 }

public class User : BaseEntity, ITenantScoped
{
    public Guid TenantId { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Name { get; private set; }
    public UserRole Role { get; private set; }
    public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();
    public ICollection<Payment> Payments { get; private set; } = new List<Payment>();
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    private User() { }

    public User(Guid tenantId, string email, string passwordHash, string name, UserRole role)
    {
        TenantId = tenantId;
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        Role = role;
    }
}

