using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Attribute
{
    public int ID { get; set; }
    public string AttributeName { get; set; }
    public int Status { get; set; }
}
public class AttributeEntityConfig : IEntityTypeConfiguration<Attribute>

{
    public void Configure(EntityTypeBuilder<Attribute> builder)
    {
        builder.ToTable("Attribute");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.AttributeName);
        builder.Property(x => x.Status).HasDefaultValue(1);

    }
}
