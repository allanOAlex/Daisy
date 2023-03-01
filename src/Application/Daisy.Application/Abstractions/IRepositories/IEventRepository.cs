using Daisy.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IRepositories
{
    public interface IEventRepository : IBaseRepository<Event>
    {
    }
}
