using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class MigrationScript
{


    public MigrationScript(string scriptId, string description)
    {
        ScriptId = scriptId;
        Description = description;
        ExecutedOn = DateTime.Now;
    }

    public string ScriptId { get; protected set; }
    public string Description { get; protected set; }
    public DateTime ExecutedOn { get; protected set; }
}

public class MigrationScriptEntityConfig : IEntityTypeConfiguration<MigrationScript>
{
    public void Configure(EntityTypeBuilder<MigrationScript> builder)
    {
        builder.ToTable("__MigrationScript");
        builder.HasKey(x => x.ScriptId);
        builder.Property(x => x.Description).HasMaxLength(300);
        builder.Property(x => x.ExecutedOn).HasDefaultValue(new DateTime(2023,05,20));

    }

}