using EyeC.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace EyeC.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Doctor> Doctors { get; }
    DbSet<Office> Offices { get; }
    DbSet<OfficeMedia> OfficeMedia { get; }
    DbSet<OfficeDetailItem> OfficeDetailItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync();
}
