﻿using System;
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
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OtherNames { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        [DefaultValue(0)]
        public bool IsLoggedIn { get; set; } = false;
        public DateTime LoginTime { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LogoutTime { get; set; }

        [NotMapped]
        public string? Password { get; set; }
        public string? PasswordResetToken { get; set; }

        [DefaultValue(0)]
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedOn { get; set; }
        public int DeletedBy { get; set; }
        

        [Timestamp]
        public byte[]? RowVersion { get; set; }



    }
}
