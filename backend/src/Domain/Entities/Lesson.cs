
namespace Domain.Entities;

public class Lesson : BaseEntity, ITenantScoped
{
    public Guid TenantId { get; private set; }
    public Guid ClassroomId { get; private set; }
    public Classroom Classroom { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();

    private Lesson() { }

    public Lesson(Guid tenantId, Guid classroomId, DateTime startTime, DateTime endTime)
    {
        TenantId = tenantId;
        ClassroomId = classroomId;
        StartTime = startTime;
        EndTime = endTime;
    }
}

