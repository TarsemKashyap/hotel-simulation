using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Reflection;

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
    public DbSet<AppUserRefreshToken> RefreshTokens {get;set;}
    public DbSet<MigrationScript> MigrationScripts { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
       
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        base.OnModelCreating(builder);
    }
}