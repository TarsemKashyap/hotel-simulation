using Common.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Service;
using Mysqlx;
using Common.Model;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System.Security.Principal;

public interface IPaymentService
{
    Task<PaymentResponse> PaymentCheck(PaymentTransactionDto paymentTransactionDto);
}


public class PaymentService : IPaymentService
{
    private readonly PaymentConfig _PaymentConfig;
    private readonly IStudentSignupTempService _studentSignupTempService;
    private readonly IEmailService _emailService;

    public PaymentService(IOptions<PaymentConfig> paymentConfig, IStudentSignupTempService studentSignupTempService, IEmailService emailService)
    {
        _PaymentConfig = paymentConfig.Value;
        _studentSignupTempService = studentSignupTempService;
        _emailService = emailService;
    }

    public async Task<PaymentResponse> PaymentCheck(PaymentTransactionDto paymentTransactionDto)
    {
        string toEmail = paymentTransactionDto.Payer_email;
        var signupUser = await _studentSignupTempService.GetByRefrence(paymentTransactionDto.Custom);
        signupUser.PaymentDate = DateTime.Now;
        signupUser.TransactionId = paymentTransactionDto.Tx;
        signupUser.Amount = Convert.ToDecimal(paymentTransactionDto.Amount);
        signupUser.PaymentStatus = paymentTransactionDto.Payment_status;
        //  signupUser.quantity = Convert.ToInt16(quantity);
        //   signupUser.quantityleft = Convert.ToInt16(quantity);
        signupUser.RawTransactionResponse = paymentTransactionDto.RawTransactionResponse;
        signupUser.Email = toEmail;

        var response = await _studentSignupTempService.Update(signupUser);
        string signUpUrl = _PaymentConfig.webUrl + "/signup?id=" + paymentTransactionDto.Custom;
        MailMessage message = new MailMessage();
        message.To.Add(new MailAddress(toEmail, paymentTransactionDto.First_name));
        message.Subject = "Hotel Simulation Payment Transaction ID";
        message.IsBodyHtml = true;

        message.Body = "<p>Dear user,</p><p>Thank you for your payment. The transaction has been completed successfully. Please use the transaction ID below to register a new account at <a>" + signUpUrl + "</a>.</p> <p>" + paymentTransactionDto.Tx + "</p><p>Sincerely,<br/> Hotel Business Management Training Simulation</p>";
        try
        {
            await _emailService.Send(message);
        }
        catch (Exception ex)
        {
            return new PaymentResponse
            {
                Message = ex.Message,
                status = false
            };
        }

        return new PaymentResponse
        {
            Message = "Done",
            status = true
        };
    }
}

