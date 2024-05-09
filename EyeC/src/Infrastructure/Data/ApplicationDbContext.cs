using System.Reflection;
using EyeC.Application.Common.Interfaces;
using EyeC.Domain.Entities;
using EyeC.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace EyeC.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    #region Entities
    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Office> Offices => Set<Office>();
    public DbSet<OfficeMedia> OfficeMedia => Set<OfficeMedia>();
    public DbSet<OfficeDetailItem> OfficeDetailItems => Set<OfficeDetailItem>();
    #endregion


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public Task<IDbContextTransaction> BeginTransactionAsync() => Database.BeginTransactionAsync();
}
