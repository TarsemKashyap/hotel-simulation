using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CustomerRawRating
{
    public int ID { get; set; }
    public int MonthID { get; set; }
    public int QuarterNo { get; set; }
    public int GroupID { get; set; }
    public string Attribute { get; set; }
    public string Segment { get; set; }
    public int RawRating { get; set; }
    public virtual Month Month { get; set; }

}
public class CustomerRawRatingEntityConfig : IEntityTypeConfiguration<CustomerRawRating>
{
    public void Configure(EntityTypeBuilder<CustomerRawRating> builder)
    {
        builder.ToTable("CustomerRawRating");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.MonthID).IsRequired();
        builder.HasOne(x => x.Month).WithMany(x => x.CustomerRawRating);
        builder.Property(x => x.QuarterNo).IsRequired();
        builder.Property(x => x.GroupID).IsRequired();
        builder.Property(x => x.Attribute);
        builder.Property(x => x.Segment);
        builder.Property(x => x.RawRating);
    }
}

