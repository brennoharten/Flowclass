
namespace Domain.Entities;

public class Lesson : BaseEntity, ITenantScoped
{
    public Guid TenantId { get; private set; }
    public Guid ClassroomId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    private Lesson() { }
    public Lesson(Guid tenantId, Guid classroomId, DateTime startTime, DateTime endTime)
        => (TenantId, ClassroomId, StartTime, EndTime) = (tenantId, classroomId, startTime, endTime);
}

