﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.Email
{
    public class PasswordResetEmailRequest
    {
        public List<MailboxAddress>? To { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
    }
}
