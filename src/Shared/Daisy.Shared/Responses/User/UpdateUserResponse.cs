﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Responses.User
{
    public class UpdateUserResponse
    {
        public bool Successful { get; set; }
        public string? Message { get; set; }
    }
}