using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Exceptions.BaseExceptions;
using Daisy.Shared.Exceptions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Infrastructure.Implementations.Services
{
    public class ErrorService : IErrorService
    {
        public ErrorService()
        {

        }

        public void BadRequest()
        {
            throw new Shared.Exceptions.Global.BadRequestException("Invalid data");
        }

        public void NotFound()
        {
            throw new KeyNotFoundException();
        }

        public void NullReference()
        {
            throw new NullReferenceException();
        }
    }
}
