using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MarketingTechniques
{
    public int ID { get; set; }
    public string Techniques { get; set; }
}
public class MarketingTechniquesEntityConfig : IEntityTypeConfiguration<MarketingTechniques>
{
    public void Configure(EntityTypeBuilder<MarketingTechniques> builder)
    {
        builder.ToTable("MarketingTechniques");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Techniques).IsRequired();
    }
}

