using Daisy.Shared.Requests.Email;
using Daisy.Shared.Responses.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface IEmailService
    {
        Task<PasswordResetEmailResponse> SendPasswordResetEmail(string emailAddress);
    }
}
