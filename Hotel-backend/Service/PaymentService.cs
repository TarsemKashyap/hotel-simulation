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

public interface IPaymentService
    {
        Task<bool> PaymentCheck(PaymentTransactionDto paymentTransactionDto);
    }


public class PaymentService : IPaymentService
{
    private readonly PaymentConfig _PaymentConfig;
    private readonly IStudentSignupTempService _studentSignupTempService;
    private readonly IEmailService _emailService;

    public PaymentService(IOptions<PaymentConfig> paymentConfig , IStudentSignupTempService studentSignupTempService, IEmailService emailService)
    {
        _PaymentConfig = paymentConfig.Value;
        _studentSignupTempService = studentSignupTempService;
        _emailService = emailService;
    }

    public async Task<bool> PaymentCheck(PaymentTransactionDto paymentTransactionDto)
    {
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        String authToken, query, strResponse;
        String sKey, sValue, fname = "", lname, mc_gross, itemName, Pmtcurrency, quantity = "", payerEmail = "", item_name1;

        authToken = _PaymentConfig.authToken;

        query = "cmd=_notify-synch&tx=" + paymentTransactionDto.Tx + "&at=" + authToken;

        // Create the request back
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_PaymentConfig.sandBoxUrl);

        // Set values for the request back
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        req.ContentLength = query.Length;

        // Write the request back IPN strings
        StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
        stOut.Write(query);
        stOut.Close();

        // Do the request to PayPal and get the response
        StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
        strResponse = stIn.ReadToEnd();
        stIn.Close();

        // If response was SUCCESS, parse response string and
        //output details
        if (strResponse.Substring(0, 7) == "SUCCESS")
        {
            //return Ok();
            ///If success fully then inser the transaction ID to database
            // LabelTitle.Text = "Order Confirmation";
            //LabelTitle.Text = strResponse;
            // LiteralMessage.Text = "Thank you for your payment. The transaction has been completed successfully. Please use the transaction ID below to register a new account. The transaction ID will be emailed to you as well for your records.";

            //split response into string array using whitespace
            //as delimiter
            String[] StringArray = strResponse.Split();

            // NOTE:
            /*
            * loop is set to start at 1 rather than 0 because first
            string in array will be single word SUCCESS or FAIL
            and there is no splitting of this...so we will skip the
            first string and go to the next.
            */

            // use split to split array we already have
            // using "=" as delimiter
            int i;
            for (i = 1; i < StringArray.Length - 1; i++)
            {
                String[] StringArray1 = StringArray[i].Split('=');

                sKey = StringArray1[0];
                sValue = StringArray1[1];

                // set string vars to hold variable names using a switch
                switch (sKey)
                {
                    case "first_name":
                        fname = sValue;
                        break;

                    case "last_name":
                        lname = sValue;
                        break;

                    case "payer_email":
                        payerEmail = sValue;
                        break;

                    case "mc_gross":
                        mc_gross = sValue;
                        break;

                    case "item_name":
                        itemName = sValue;
                        break;

                    //for shopping cart payments the
                    //item_name vars are numbered to reflect
                    //a multi-item purchase.
                    case "item_name1":
                        item_name1 = sValue;
                        break;

                    case "mc_currency":
                        Pmtcurrency = sValue;
                        break;

                    case "quantity":
                        quantity = sValue;
                        break;
                }
            }
            var signupUser = await _studentSignupTempService.GetByRefrence(paymentTransactionDto.Custom);
            signupUser.PaymentDate = DateTime.Now;
            signupUser.TransactionId = paymentTransactionDto.Tx;
            signupUser.quantity = Convert.ToInt16(quantity);
            signupUser.quantityleft = Convert.ToInt16(quantity);
            var response = await _studentSignupTempService.Update(signupUser);

            ////send email 
            // string fromEmail = "info@hotelsimulation.com";
            string toEmail = payerEmail.Replace("%40", "@");

            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(toEmail, fname));
            message.Subject = "Hotel Simulation Payment Transaction ID";
            message.IsBodyHtml = true;
            message.Body = "<p>Dear user,</p><p>Thank you for your payment. The transaction has been completed successfully. Please use the transaction ID below to register a new account at <a>http://hotelsimulation.com/Account/Register.aspx</a>.</p> <p>" + paymentTransactionDto.Tx + "</p><p>Sincerely,<br/> Hotel Business Management Training Simulation</p>";
            try
            {
                await _emailService.Send(message);
            }
            catch (Exception ex)
            {
                //  LiteralOrderDetails.Text = ex.ToString();
            }
        }

        else if (strResponse.Substring(0, 4) == "FAIL")
        {
            throw new System.ComponentModel.DataAnnotations.ValidationException(strResponse + ". " + "Your Payment Failed!");
            // if response is FAIL, print it to screen
            //Response.Write(strResponse);
            // LabelTitle.Text = strResponse + ". " + "Your Payment Failed!";
            //LiteralMessage.Text = "<p>Your Payment Failed!</p>";
            //paymentIDTableAdapter payA = new paymentIDTableAdapter();

            //if (payA.ScalarCheckExist("4B445058YN007393B")==null)
            //{ LabelTitle.Text = "Not Exist"; }
        }

        else
        {
            // some unexpected response??? -  log it
            // for investigation later.

        }
        return true;
    }
}

