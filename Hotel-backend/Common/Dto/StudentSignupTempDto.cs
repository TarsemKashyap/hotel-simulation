using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class StudentSignupTempDto
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
        public int quantity { get; set; }

        public int quantityleft { get; set; }
        public SignupStatus SignupStatus { get; set; }
        public bool IsSignupComplete { get; set; }
    }



    public enum PaymentStatus
    {
        NotStarted = 0,
        Success = 1,
        Failed = 2
    }

    public enum SignupStatus
    {
        NotStarted = 0,
        Success = 1,
    }

   
}
