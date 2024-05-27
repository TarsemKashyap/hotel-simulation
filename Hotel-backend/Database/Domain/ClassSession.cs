using Database.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
public class ClassSession
{
    public int ClassId { get; set; }
    public string Title { get; set; }
    public string Memo { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int HotelsCount { get; set; }
    public int RoomInEachHotel { get; set; }
    public int CurrentQuater { get; set; }
    public ClassStatus Status { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Code { get; set; }

    public string CreatedBy { get; set; }
    public virtual List<ClassGroup> Groups { get; set; }
    public virtual List<Month> Months { get; set; }
    public virtual List<StudentClassMapping> StudentClassMappings { get; set; }
    public virtual List<StudentRoleMapping> StudentRoleMappings { get; set; }

}

public enum ClassStatus
{
    S = 0,
    I = 1,
    T = 2,
    A = 3,
    C = 4
}

public class ClassSessionEntityConfig : IEntityTypeConfiguration<ClassSession>
{
    public void Configure(EntityTypeBuilder<ClassSession> builder)
    {
        builder.ToTable("Class");
        builder.HasKey(x => x.ClassId);
        builder.HasMany(x => x.Groups);
        builder.Property(x => x.Title).HasMaxLength(300).IsRequired();
        builder.Property(x => x.Memo).HasMaxLength(300);
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.HotelsCount).IsRequired();
        builder.Property(x => x.RoomInEachHotel).IsRequired();
        builder.Property(x => x.CurrentQuater).IsRequired();
        builder.Property(x => x.Status).IsRequired().HasConversion<string>();
        builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired();


    }

}