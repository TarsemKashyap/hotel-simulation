using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Database;
public class HotelDbContext : IdentityDbContext<IdentityUser>
{

   
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AppUserEntityConfig());
        builder.ApplyConfiguration(new ClassSessionEntityConfig());
        
        base.OnModelCreating(builder);
    }
}