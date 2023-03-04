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
    public AppUserRole(string name) : base(name)
    {

    }
}

public static class RoleType
{
    public const string Admin = "Admin";
    public const string Instructor = "Instructor";
    public const string Student = "Student";
}





