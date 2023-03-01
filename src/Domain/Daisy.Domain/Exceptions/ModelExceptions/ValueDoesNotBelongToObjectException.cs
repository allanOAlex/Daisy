using Daisy.Domain.Exceptions.BaseExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Domain.Exceptions.ModelExceptions
{
    public sealed class ValueDoesNotBelongToObjectException : BadRequestException
    {
        public ValueDoesNotBelongToObjectException(Guid objectId, Guid valueId) : base($"The value with the identifier {valueId} does not belong to the object with the identifier {objectId}")
        {
        }
    }
}
