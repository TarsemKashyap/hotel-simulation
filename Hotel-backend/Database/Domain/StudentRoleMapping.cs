using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Domain;

public class StudentRoleMapping
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string StudentId { get; set; }
    public virtual StudentRoles StudentRoles { get; set; }
    public virtual Student Student { get; set; }
}
public class StudentRoleMappingEntityConfig : IEntityTypeConfiguration<StudentRoleMapping>
{
    public void Configure(EntityTypeBuilder<StudentRoleMapping> builder)
    {
        builder.ToTable("StudentRoleMapping");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.StudentRoles).WithMany(x => x.StudentRoleMappings).HasForeignKey(x => x.RoleId);
        builder.HasOne(x => x.Student).WithMany(x => x.StudentRoleMapping).HasForeignKey(x => x.StudentId);
        builder.HasIndex(x => x.RoleId).HasDatabaseName("IX_StudentRoleMapping_RoleId");
    }

}