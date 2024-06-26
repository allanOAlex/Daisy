﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Exceptions.Global
{
    public class InternalServerException : CustomException
    {
        public InternalServerException(string message, List<string>? errors = default) : base(message, errors, HttpStatusCode.InternalServerError) 
        {

        }
    }
}
