using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Domain.Models
{
    public class Carousel
    {
        public Carousel()
        {

        }

        [Key]
        public int Id { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }
        public string? Label { get; set; }
        public string? Paragraph { get; set; }

        [DefaultValue(0)]
        public bool IsDeleted { get; set; }
    }
}
