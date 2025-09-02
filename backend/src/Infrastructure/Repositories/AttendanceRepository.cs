using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _db;

    public AttendanceRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Attendance attendance, CancellationToken ct)
    {
        await _db.Attendances.AddAsync(attendance, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<List<Attendance>> GetByLessonAsync(Guid tenantId, Guid lessonId, CancellationToken ct)
    {
        return await _db.Attendances
            .Include(a => a.Student) // se você quiser carregar nome/email do aluno
            .Where(a => a.TenantId == tenantId && a.LessonId == lessonId)
            .ToListAsync(ct);
    }
}
