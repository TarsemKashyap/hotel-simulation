using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Reflection;
using Database.Domain;

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
    public DbSet<StudentSignupTemp> StudentSignupTemp { get; set; }
    public DbSet<StudentClassMapping> StudentClassMapping { get; set; }
    public DbSet<Attribute> Attribute { get; set; }
    public DbSet<Segment> Segment { get; set; }
    public DbSet<MarketingTechniques> MarketingTechniques { get; set; }
    public DbSet<DistributionChannels> DistributionChannels { get; set; }
    public DbSet<MarketingDecision> MarketingDecision { get; set; }
    public DbSet<PriceDecision> PriceDecision { get; set; }
    public DbSet<AttributeDecision> AttributeDecision { get; set; }
    public DbSet<RoomAllocation> RoomAllocation { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        base.OnModelCreating(builder);
    }
}