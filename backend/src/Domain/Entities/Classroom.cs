namespace Domain.Entities;

public class Classroom : BaseEntity, ITenantScoped
{
    public Guid TenantId { get; private set; }
    public string Name { get; private set; }
    public Guid TeacherId { get; private set; }

    private Classroom() { }
    public Classroom(Guid tenantId, string name, Guid teacherId)
        => (TenantId, Name, TeacherId) = (tenantId, name, teacherId);
}

