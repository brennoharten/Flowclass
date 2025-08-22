namespace Domain.Entities;

public class Classroom : BaseEntity, ITenantScoped
{
    public Guid TenantId { get; private set; }
    public string Name { get; private set; }
    public Guid TeacherId { get; private set; }
    public User Teacher { get; private set; } 
    public ICollection<Lesson> Lessons { get; private set; } = new List<Lesson>();

    private Classroom() { } 

    public Classroom(Guid tenantId, string name, Guid teacherId)
    {
        TenantId = tenantId;
        Name = name;
        TeacherId = teacherId;
    }
}

