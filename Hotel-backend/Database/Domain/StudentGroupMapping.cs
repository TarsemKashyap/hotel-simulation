using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Domain
{
    public class StudentGroupMapping
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int GroupId { get; set; }


    }

    public class StudentGroupMappingEntityConfig : IEntityTypeConfiguration<StudentGroupMapping>
    {
        public void Configure(EntityTypeBuilder<StudentGroupMapping> builder)
        {
            builder.HasKey(x => x.Id);
        }

    }
}
