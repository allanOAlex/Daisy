using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.Carousels
{
    public class DeleteCarouselRequest
    {
        public int Id { get; set; }
        //public string? Image { get; set; }
        //public string? Label { get; set; }
        //public string? Paragraph { get; set; }
        public bool IsDeleted { get; set; }
    }
}
