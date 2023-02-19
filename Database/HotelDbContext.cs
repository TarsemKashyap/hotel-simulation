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
    protected override void OnModelCreating(ModelBuilder builder)
    {
      //  builder.ApplyConfiguration(new AppUserEntityConfig());
       // builder.ApplyConfiguration(new ClassSessionEntityConfig());
      //  builder.ApplyConfiguration(new ClassGroupEntityConfig());
      builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        base.OnModelCreating(builder);
    }
}