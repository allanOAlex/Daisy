using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.Event
{
    public class UpdateEventRequest
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }
        public string? ImageName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]  
        public string? Location { get; set; }

        [Required]  
        public string? Venue { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
