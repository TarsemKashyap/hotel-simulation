using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Domain
{
    public class StudentSignupTemp
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Institute { get; set; }
        public string ClassCode { get; set; }
        public string Email { get; set; }
        public string Reference { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentFailureReason { get; set; }
        public string RawTransactionResponse { get; set; }
    }

    public enum PaymentStatus
    {
        NotStarted = 0,
        Success = 1,
        Failed = 2
    }

    public class StudentSignupTempEntityConfig : IEntityTypeConfiguration<StudentSignupTemp>
    {
        public void Configure(EntityTypeBuilder<StudentSignupTemp> builder)
        {
            builder.ToTable("StudentSignupTemp");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(300).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Institute).IsRequired();
            builder.Property(x => x.ClassCode).IsRequired();
            builder.Property(x => x.Reference).IsRequired();
            builder.Property(x => x.TransactionId);
            builder.Property(x => x.Amount);
            builder.Property(x => x.CreatedDate);
            builder.Property(x => x.PaymentDate);
            builder.Property(x => x.PaymentStatus).HasDefaultValue(PaymentStatus.NotStarted);
            builder.Property(x => x.PaymentFailureReason);


        }

    }
}
