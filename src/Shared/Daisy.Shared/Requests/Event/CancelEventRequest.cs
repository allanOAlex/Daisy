using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.Event
{
    public class CancelEventRequest
    {
        public int Id { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CancelledOn { get; set; }
        public int CancelledBy { get; set; }
    }
}
