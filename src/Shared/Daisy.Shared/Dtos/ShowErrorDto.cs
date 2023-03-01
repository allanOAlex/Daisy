using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Dtos
{
    public class ShowErrorDto
    {
        public bool ShowErrors { get; set; } = true;
        public IEnumerable<string>? Errors { get; set; }
        public List<string>? ErrorsList { get;set; }
    }
}
