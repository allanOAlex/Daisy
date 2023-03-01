using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.User
{
    public class DeleteUserRequest
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public int DeletedBy { get; set; }
        public bool IsActive { get; set; }
        public bool Successful { get; set; }
        public string? Message { get; set; }
    }
}
