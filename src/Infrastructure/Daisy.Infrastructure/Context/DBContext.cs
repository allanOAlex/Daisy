using Daisy.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Infrastructure.Context
{
    public class DBContext : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly IConfiguration? configuration;

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            RenameIdentityTables(builder);
        }

        protected void RenameIdentityTables(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //create user
            AppUser appUser1= new();
            AppUser appUser2= new();

            AppUser[] appUser = new AppUser[]
            {
                appUser1 = new()
                {
                    Id = 1,
                    UserName = "allanOAlex",
                    NormalizedUserName = "ALLANOALEX",
                    Password = "PA$5@AUTH",
                    FirstName = "Allan",
                    LastName = "Alex",
                    OtherNames = string.Empty,
                    Email = "allan.alex0803@gmail.com",
                    NormalizedEmail = "ALLAN.ALEX0803@GMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumber = string.Empty,
                    PhoneNumberConfirmed = false,
                    CreatedOn = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString(),

                },

                appUser2 = new()
                {
                    Id = 2,
                    UserName = "allanOdhiambo",
                    NormalizedUserName = "ALLANODHIAMBO",
                    Password = "PA$5@AUTH",
                    FirstName = "Allan",
                    LastName = "Odhiambo",
                    OtherNames = string.Empty,
                    Email = "allan.odhiambo@gmail.com",
                    NormalizedEmail = "ALLAN.ODHIAMBO@GMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumber = string.Empty,
                    PhoneNumberConfirmed = false,
                    CreatedOn = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString(),

                },
            };
       
            //set user password
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            appUser1.PasswordHash = ph.HashPassword(appUser1, appUser1.Password);
            appUser2.PasswordHash = ph.HashPassword(appUser2, appUser2.Password);

            //seed admin role
            builder.Entity<AppRole>(entity =>
            {
                entity.ToTable(name: "Roles").HasData(
                    new AppRole 
                    {
                        Id = 1,
                        Name = "SuperAdmin",
                        NormalizedName = "SUPERADMIN"
                    },
                    new AppRole
                    {
                        Id = 2,
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new AppRole
                    {
                        Id = 3,
                        Name = "User",
                        NormalizedName = "USER"
                        
                    });
            });

            //seed user
            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "Users").HasData(appUser);
            }); 

            //set user role to SuperAdmin
            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles").HasData(
                    new IdentityUserRole<int>
                    {
                        RoleId = 1,
                        UserId = 1
                    },
                    new IdentityUserRole<int>
                    {
                        RoleId = 2,
                        UserId = 2
                    });
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
           


        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


    }
}
