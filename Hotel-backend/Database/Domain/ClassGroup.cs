using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClassGroup
{
    public int GroupId { get; set; }
    public int ClassId { get; set; }
    public int Serial { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }

    public virtual ClassSession Class { get; set; }

}

public class ClassGroupEntityConfig : IEntityTypeConfiguration<ClassGroup>
{
    public void Configure(EntityTypeBuilder<ClassGroup> builder)
    {
        builder.ToTable("ClassGroup");
        builder.HasKey(x => x.GroupId);
        builder.Property(x => x.GroupId).IsRequired().ValueGeneratedOnAdd();
        builder.HasOne(x => x.Class).WithMany(x => x.Groups).HasForeignKey(x => x.ClassId);
        builder.HasIndex(x => x.ClassId).HasDatabaseName("IX_ClassGroup_ClassID");

        builder.Property(x => x.Serial).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Balance).HasDefaultValue(0);

    }

}