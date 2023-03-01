using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Daisy.Shared.Responses.Carousels
{
    public class GetAllCarouselsResponse
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }
        public string? ImageSource { get; set; }
        public string? Label { get; set; }
        public string? Paragraph { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }
    }
}
