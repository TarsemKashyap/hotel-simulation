using Database.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class Student : AppUser
{
    public string Institue { get; set; }

    public virtual List<StudentClassMapping> StudentClassMappings { get; set; }
    public virtual List<StudentRoleMapping> StudentRoleMapping { get; set; }
}

public class StudentEntityConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Student");

    }

}