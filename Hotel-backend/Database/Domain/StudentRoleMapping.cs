using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Domain
{
    public class StudentRoleMapping
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }

    public class StudentRoleMappingEntityConfig : IEntityTypeConfiguration<StudentRoleMapping>
    {
        public void Configure(EntityTypeBuilder<StudentRoleMapping> builder)
        {
            builder.ToTable("StudentRoleMapping");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoleName).HasMaxLength(300).IsRequired();

        }

    }
}
