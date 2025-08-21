namespace Domain.Entities;

public enum AttendanceStatus { Present = 1, Absent = 2, Justified = 3 }

public class Attendance : BaseEntity, ITenantScoped
{
    public Guid TenantId { get; private set; }
    public Guid LessonId { get; private set; }
    public Guid StudentId { get; private set; }
    public AttendanceStatus Status { get; private set; }

    private Attendance() { }
    public Attendance(Guid tenantId, Guid lessonId, Guid studentId, bool status)
        => (TenantId, LessonId, StudentId, Status) = (tenantId, lessonId, studentId, status);
}