using Daisy.Application.Abstractions.IServices;
using Daisy.Shared.Configs;
using Daisy.Shared.Requests.Email;
using Daisy.Shared.Responses.Email;
using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using MimeKit;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Infrastructure.Implementations.Services
{
    internal sealed class EmailService : IEmailService
    {
        private readonly EmailConfiguration emailConfig;

        public EmailService(EmailConfiguration EmailConfig)
        {
            emailConfig = EmailConfig;
        }

        public async Task<PasswordResetEmailResponse> SendPasswordResetEmail(string emailAddress, string token, string resetUrl)
        {
            try
            {
                
                using var smtpClient = new SmtpClient(emailConfig.SmtpServer, emailConfig.Port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailConfig.UserEmail, emailConfig.Password),
                    EnableSsl = true
                };

                using var message = new MailMessage(emailConfig.From, emailAddress)
                {
                    Subject = "Password Reset",
                    Body = $"Hi {string.Empty} \n" +
                           $"There was a request to change your password! \n" +
                           $" \n" +
                           $"If you did not make this request then please ignore this email. \n" +
                           $" \n" +
                           $"Otherwise, paste the token below in the [Reset Key] section of the password reset form, or \n click on the link below. \n We’ll have you up and running in no time. \n" +
                           $" \n" +
                           $"token:  {token} \n" +
                           $" \n" +
                           $"link:  {resetUrl} \n" +
                           $" \n" +
                           $" \n" +
                           $" Please do not reply to this email. \n" +
                           $" Supporting you is our priority! \n" +
                           $" \n" +
                           $" \n" +
                           $" BR!" +
                           $" \n" 
                           
                };

                await smtpClient.SendMailAsync(message);

                return new PasswordResetEmailResponse() { Successful = true, Message = $"A password reset token has been sent to the email you provided. \nPlease check your email and follow instructions to reset your password." };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
