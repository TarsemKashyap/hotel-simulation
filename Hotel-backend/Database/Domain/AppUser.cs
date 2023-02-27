using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;


public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class AppUserRole : IdentityRole
{
}

public class Instructor : AppUser
{
    public string Institue { get; set; }
}

public class Student : AppUser
{
    public string Institue { get; set; }
}

public enum AppRoleType
{
    Admin = 1,
    Instructor = 2,
    Student = 3
}

public class InstructorEntityConfig : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructor");

    }

}

public class StudentEntityConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Student");

    }

}

