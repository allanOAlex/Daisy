using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Domain.Models
{
    public class Event
    {
        public Event()
        {

        }

        [Key]
        public int Id { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Time { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public string? Venue { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        
        [DefaultValue(0)]
        public bool IsCancelled { get; set; } = false;
        public DateTime CancelledOn { get; set; }
        public int CancelledBy { get; set; }

    }
}
