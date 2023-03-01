using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Exceptions.Global
{
    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string message) : base(message, null, HttpStatusCode.Forbidden) { }
    }
    
}
