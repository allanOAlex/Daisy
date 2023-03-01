using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Responses.Carousels
{
    public class DeleteCarouselResponse
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Label { get; set; }
        public string? Paragraph { get; set; }
        public bool Successful { get; set; }
        public string? Message { get; set; }
       
    }
}
