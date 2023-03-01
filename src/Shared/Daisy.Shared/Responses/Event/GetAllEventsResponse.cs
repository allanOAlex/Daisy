using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Responses.Event
{
    public class GetAllEventsResponse
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Display(Name = "Start Date")] 
        public DateTime StartDate { get; set; }
        public DateTime Time { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public string? Venue { get; set; }
    }
}
