using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class ClassSession
{
    public int ClassId { get; set; }
    public string Title { get; set; }
    public string Memo { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int HotelsCount { get; set; }
    public int RoomInEachHotel { get; set; }
    public int CurrentQuater { get; set; }
    public ClassStatus Status { get; set; }
    public DateTime CreatedOn { get; set; }


}

public enum ClassStatus
{
    S = 0,
    I = 1,
    T = 2
}

public class ClassSessionEntityConfig : IEntityTypeConfiguration<ClassSession>
{
    public void Configure(EntityTypeBuilder<ClassSession> builder)
    {
        builder.ToTable("Class");
        builder.HasKey(x => x.ClassId);
        builder.Property(x => x.Title).HasMaxLength(300).IsRequired();
        builder.Property(x => x.Memo).HasMaxLength(300).IsRequired();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.HotelsCount).IsRequired();
        builder.Property(x => x.RoomInEachHotel).IsRequired();
        builder.Property(x => x.CurrentQuater).IsRequired();
        builder.Property(x => x.Status).IsRequired().HasConversion<string>();
        builder.Property(x=>x.CreatedOn).IsRequired();

    }
}