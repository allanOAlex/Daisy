using Daisy.Domain.Exceptions.BaseExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Domain.Exceptions.ModelExceptions
{
    public sealed class ObjectNotFoundException : NotFoundException
    {
        public ObjectNotFoundException(Guid objectId) : base($"The object with the identifier {objectId} was not found.")
        {
        }
    }
}
