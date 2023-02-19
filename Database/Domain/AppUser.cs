using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;


public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType UserType { get; set; }
}

public class AppUserRole : IdentityRole { }

public enum UserType
{
    Admin = 1,
    Instructor = 2,
    Student = 3
}

// public class AppUserEntityConfig : IEntityTypeConfiguration<AppUser>
// {
//     public void Configure(EntityTypeBuilder<AppUser> builder)
//     {
//         builder.ToTable("AppUsers");
//         builder.HasKey(x => x.UserId);
//         builder.HasOne(x => x.IdentityUser).WithOne(x => x.Id);
//         builder.Property(x => x.FirstName).HasMaxLength(200).IsRequired();
//         builder.Property(x => x.LastName).HasMaxLength(200).IsRequired();
//         builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
//         builder.Property(x => x.Mobile).HasMaxLength(100);

//     }

// }

