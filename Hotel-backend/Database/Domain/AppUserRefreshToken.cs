using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AppUserRefreshToken
{
    public Guid Id { get; set; }

    public string UserId { get; set; }
    public string RefreshToken { get; set; }
    public bool IsActive { get; set; }

    public DateTime ExpiryTime { get; set; }
    public DateTime CreatedDate { get; set; }

    public virtual AppUser User { get; set; }
}

public class AppUserRefreshTokenEntityConfig : IEntityTypeConfiguration<AppUserRefreshToken>
{
    public void Configure(EntityTypeBuilder<AppUserRefreshToken> builder)
    {
        builder.ToTable("AppUserRefreshToken");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).IsRequired();
        builder.HasOne(x => x.User).WithMany(x => x.RefreshTokens);
        builder.HasIndex(x => x.UserId).HasDatabaseName("IX_AppUserRefreshToken_UserId");
        builder.Property(x => x.RefreshToken).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.ExpiryTime).IsRequired();

    }

}
