using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Domain
{
    public class StudentClassMapping
    {
        public Guid Id { get; set; }
        public int ClassId { get; set; }
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
        public virtual ClassSession Class { get; set; }

    }

    public class StudentClassMappingEntityConfig : IEntityTypeConfiguration<StudentClassMapping>
    {
        public void Configure(EntityTypeBuilder<StudentClassMapping> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Class).WithMany(x => x.StudentClassMappings).HasForeignKey(x => x.ClassId);
            builder.HasOne(x => x.Student).WithMany(x => x.StudentClassMappings).HasForeignKey(x => x.StudentId);

        }

    }
}
