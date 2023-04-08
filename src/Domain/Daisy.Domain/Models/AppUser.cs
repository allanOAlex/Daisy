using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Daisy.Domain.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? OtherNames { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        [DefaultValue(0)]
        public bool IsApproved { get; set; } = false;

        public int ApprovedBy { get; set; }

        public DateTime ApprovedOn { get; set; }

        [DefaultValue(0)]
        public bool IsActive { get; set; } = false;

        [DefaultValue(0)]
        public bool IsAdmin { get; set; } = false;

        [DefaultValue(0)]
        public bool IsLoggedIn { get; set; } = false;
        public DateTime LoginTime { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LogoutTime { get; set; }

        [DefaultValue(0)]
        public bool IsAuthenticated { get; set; } = false;

        [DefaultValue(0)]
        public bool RememberMe { get; set; } = false;

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime DeletedOn { get; set; }

        public int DeletedBy { get; set; }

        public string? PasswordResetToken { get; set; }



        [NotMapped]
        public bool Successful { get; set; }
        [NotMapped]
        public string? Message { get; set; }
        [NotMapped]
        public IEnumerable<string>? Errors { get; set; }

    }
}
