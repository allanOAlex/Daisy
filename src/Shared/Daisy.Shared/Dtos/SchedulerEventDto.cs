using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Dtos
{
    public class SchedulerEventDto
    {
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
