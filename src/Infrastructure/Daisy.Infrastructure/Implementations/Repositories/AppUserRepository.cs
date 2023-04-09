using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IRepositories;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Context;
using Daisy.Shared.Requests.User;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Daisy.Infrastructure.Implementations.Repositories
{
    internal sealed class AppUserRepository : IBaseRepository<AppUser>, IAppUserRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly DBContext context;
        private readonly DapperContext daper;
        private readonly UserManager<AppUser> userManager;

        public AppUserRepository(IUnitOfWork UnitOfWork, DBContext Context, DapperContext Daper, UserManager<AppUser> UserManager)
        {
            unitOfWork = UnitOfWork;   
            context = Context;
            daper = Daper;
            userManager = UserManager;
        }

        public async Task<AppUser> CreateWithUserManager(AppUser entity)
        {
            try
            {
                await userManager.CreateAsync(entity, entity.Password);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(AppUserRepository)}(CreateWithUserManager)Error Creating User {ex.Message}");
            }
            
        }

        public AppUser Create(AppUser entity)
        {
            context.AppUsers.AddAsync(entity);
            return entity;
        }

        public AppUser Delete(AppUser entity)
        {
            AppUser userToDelete = context.AppUsers.Find(entity.Id);
            context.AppUsers.Remove(entity);
            return entity;
        }

        public AppUser DoDapperDelete(AppUser entity)
        {
            try
            {
                var query = "DELETE FROM Users WHERE Id = @Id";
                using (var connection = daper.CreateConnection())
                {
                    connection.ExecuteAsync(query, entity.Id);
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
            
            
        }

        async Task<AppUser> IAppUserRepository.DapperDelete(AppUser entity)
        {
            try
            {
                var query = "DELETE FROM Users WHERE Id = @Id";
                using (var connection = daper.CreateConnection())
                {
                    await connection.ExecuteAsync(query, entity.Id);
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<AppUser> FindAll()
        {
            return context.AppUsers.OrderByDescending(e => e.Id).AsNoTracking();
        }

        public IQueryable<AppUser> FindByCondition(Expression<Func<AppUser, bool>> expression)
        {
            return context.AppUsers.Where(expression).AsNoTracking();
        }

        public AppUser Update(AppUser entity)
        {
            try
            {
                if (context.ChangeTracker.Entries<AppUser>().Any(e => e.Entity.Id == entity.Id))
                {
                    context.DetachAllEntities();
                    var userToUpdate = context.AppUsers.Find(entity.Id);
                    context.Entry(userToUpdate).State = EntityState.Detached;
                    userToUpdate = entity;
                    AppUser user = userToUpdate;
                    context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    context.AppUsers.Update(entity);
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<AppUser> DoUpdate(AppUser entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AppUser DoDapperUpdate(AppUser entity)
        {
            try
            {
                var query = $"UPDATE Users SET" +
                    $"FirstName = @Name," +
                    $"LastName = @Name," +
                    $"Email = @Address," +
                    $"PhoneNumber = @Country" +
                    $"WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", entity.Id);
                parameters.Add("FirstName", entity.FirstName);
                parameters.Add("LastName", entity.LastName);
                parameters.Add("Email", entity.Email);
                parameters.Add("PhoneNumber", entity.PhoneNumber);

                using (var connection = daper.CreateConnection())
                {
                    connection.ExecuteAsync(query, parameters);
                }

                return entity;

            }
            catch (Exception ex)
            {
                throw ;
            }

        }

        async Task<AppUser> IAppUserRepository.DapperUpdate(AppUser entity)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", entity.Id);
                parameters.Add("FirstName", entity.FirstName);
                parameters.Add("LastName", entity.LastName);
                parameters.Add("Email", entity.Email);
                parameters.Add("PhoneNumber", entity.PhoneNumber);

                var query = $"UPDATE Users SET " +
                    $"FirstName = @FirstName, " +
                    $"LastName = @LastName, " +
                    $"Email = @Email, " +
                    $"PhoneNumber = @PhoneNumber " +
                    $"WHERE Id = @Id";

                using (var connection = daper.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }

                return entity;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        
    }
}
