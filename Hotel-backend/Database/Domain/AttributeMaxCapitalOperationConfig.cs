using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AttributeMaxCapitalOperationConfig
{
    public int ID { get; set; }
    public int ConfigID { get; set; }
    public string Attribute { get; set; }
    public decimal MaxNewCapital { get; set; }
    public decimal NewCapitalPortion { get; set; }
    public decimal MaxOperation { get; set; }
    public decimal OperationPortion { get; set; }
    public decimal LaborPortion { get; set; }
    public decimal PreCapitalPercent { get; set; }
    public decimal PreLaborPercent { get; set; }
    public decimal InitialCapital { get; set; }
    public decimal DepreciationYearly { get; set; }

}
public class AttributeMaxCapitalOperationConfigEntityConfig : IEntityTypeConfiguration<AttributeMaxCapitalOperationConfig>

{
    public void Configure(EntityTypeBuilder<AttributeMaxCapitalOperationConfig> builder)
    {
        builder.ToTable("AttributeMaxCapitalOperationConfig");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.ConfigID);
        builder.Property(x => x.ConfigID).IsRequired();
        builder.Property(x => x.Attribute);
        builder.Property(x => x.MaxNewCapital);
        builder.Property(x => x.NewCapitalPortion);
        builder.Property(x => x.MaxOperation);
        builder.Property(x => x.OperationPortion);
        builder.Property(x => x.LaborPortion);
        builder.Property(x => x.PreCapitalPercent).HasPrecision(5, 5);
        builder.Property(x => x.PreLaborPercent).HasPrecision(8, 8);
        builder.Property(x => x.InitialCapital);
        builder.Property(x => x.DepreciationYearly);



    }
}
