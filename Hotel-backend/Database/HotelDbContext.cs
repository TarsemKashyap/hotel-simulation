using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Reflection;
using Database.Domain;
using System.Reflection.Emit;

namespace Database;
public class HotelDbContext : IdentityDbContext<AppUser, AppUserRole, string>
{


    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<ClassSession> ClassSessions { get; set; }
    public DbSet<ClassGroup> ClassGroups { get; set; }
    public DbSet<Month> Months { get; set; }
    public DbSet<AppUserRefreshToken> RefreshTokens { get; set; }
    public DbSet<MigrationScript> MigrationScripts { get; set; }
    public DbSet<StudentSignupTemp> StudentSignupTemp { get; set; }
    public DbSet<StudentClassMapping> StudentClassMapping { get; set; }
    public DbSet<StudentRoles> StudentRoles { get; set; }
    public DbSet<StudentGroupMapping> StudentGroupMapping { get; set; }
    public DbSet<StudentRoleMapping> StudentRoleMapping { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
        builder.Entity<StudentRoles>().HasData(
          new StudentRoles { Id = 1, RoleName = "Revenue Manager" },
          new StudentRoles { Id = 2, RoleName = "Retail and Operations Manager" },
          new StudentRoles { Id = 3, RoleName = "F&B Manager" },
          new StudentRoles { Id = 4, RoleName = "General Manager" },
          new StudentRoles { Id = 5, RoleName = "Room Manager" },
          new StudentRoles { Id = 6, RoleName = "Marketing Manager" }
      );
        base.OnModelCreating(builder);
    }
}