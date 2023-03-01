using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface IErrorService
    {
        void NullReference();
        void NotFound();
        void BadRequest();
    }
}
