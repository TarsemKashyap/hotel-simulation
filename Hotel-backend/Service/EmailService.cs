
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Threading.Tasks;


public interface IEmailService
{
    System.Threading.Tasks.Task<bool> Send(MailMessage mail);
}

public class EmailService : IEmailService
{
    private readonly Smtp _smtp;

    public EmailService(IOptions<Smtp> smtp)
    {
        _smtp = smtp.Value;
    }
    public async Task<bool> Send(MailMessage mail)
    {
        var apiKey = _smtp.WebAppUrl;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("info@hotelsimulation.com", "Hotel Simulation");
        var firstToAddress = mail.To[0];
        var to = new EmailAddress(firstToAddress.Address, firstToAddress.User);
        var msg = MailHelper.CreateSingleEmail(from, to, mail.Subject, string.Empty, mail.Body);
        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }
}