using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Domain
{
    public class StudentRoles
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<StudentRoleMapping> StudentRoleMappings { get; set; }
    }

    public class StudentRolesEntityConfig : IEntityTypeConfiguration<StudentRoles>
    {
        public void Configure(EntityTypeBuilder<StudentRoles> builder)
        {
            builder.ToTable("StudentRoleMapping");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoleName).HasMaxLength(300).IsRequired();
            builder.HasMany(x => x.StudentRoleMappings);
        }
    }
}
