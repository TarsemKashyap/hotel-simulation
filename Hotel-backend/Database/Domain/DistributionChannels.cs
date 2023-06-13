using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DistributionChannels
{
    public int ID { get; set; }
    public string Channel { get; set; }
}
public class DistributionChannelsEntityConfig : IEntityTypeConfiguration<DistributionChannels>
{
    public void Configure(EntityTypeBuilder<DistributionChannels> builder)
    {
        builder.ToTable("DistributionChannels");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.ID).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Channel).IsRequired();
    }
}
