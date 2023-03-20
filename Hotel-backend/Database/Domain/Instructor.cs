
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Instructor : AppUser
{
    public string Institute { get; set; }
}

public class InstructorEntityConfig : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructor");
        builder.Property(x=>x.Institute);

    }

}