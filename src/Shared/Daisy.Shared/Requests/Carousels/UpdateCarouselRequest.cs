using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.Carousels
{
    public class UpdateCarouselRequest
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }
        public string? Label { get; set; }
        public string? Paragraph { get; set; }
    }
}
