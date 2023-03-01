using Daisy.Application.Abstractions.IServices;
using Daisy.Shared.Configs;
using Daisy.Shared.Requests.Email;
using Daisy.Shared.Responses.Email;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using MimeKit;
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

        public async Task<PasswordResetEmailResponse> SendPasswordResetEmail(string emailAddress)
        {
            try
            {
                using var smtpClient = new SmtpClient(emailConfig.SmtpServer, emailConfig.Port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailConfig.UserName, emailConfig.Password),
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
                           $"Otherwise, use the link below and follow the instructions. We’ll have you up and running in no time. \n" +
                           $" \n" +
                           $"{string.Empty} \n" +
                           $" \n"
                };

                await smtpClient.SendMailAsync(message);

                switch (smtpClient.SendMailAsync(message).Status)
                {
                    case TaskStatus.Created:
                        break;
                    case TaskStatus.WaitingForActivation:
                        break;
                    case TaskStatus.WaitingToRun:
                        break;
                    case TaskStatus.Running:
                        break;
                    case TaskStatus.WaitingForChildrenToComplete:
                        break;
                    case TaskStatus.RanToCompletion:
                        break;
                    case TaskStatus.Canceled:
                        break;
                    case TaskStatus.Faulted:
                        break;
                    default:
                        break;
                }

                return new PasswordResetEmailResponse() { Successful = true, Message = $"The password reset link has been sent to the email you provided. \nPlease check you email and follow the instructions to reset your password." };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
