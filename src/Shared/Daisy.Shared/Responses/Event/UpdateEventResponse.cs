using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Responses.Event
{
    public class UpdateEventResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Time { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public string? Venue { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }
        public bool Successful { get; set; }
        public string? Message { get; set; }
        
    }
}
