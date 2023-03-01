using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Dtos
{
    public class AppUserDto
    {
        [DefaultValue(false)]
        public bool IsAuthenticated { get; set; } = false;
    }
}
