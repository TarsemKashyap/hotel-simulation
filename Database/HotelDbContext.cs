using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Database;
public class HotelDbContext : IdentityDbContext<IdentityUser>
{

   
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
    }

   // public DbSet<Course> Courses { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}