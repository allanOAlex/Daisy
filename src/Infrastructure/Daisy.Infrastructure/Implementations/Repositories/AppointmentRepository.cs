using Daisy.Application.Abstractions.IRepositories;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Daisy.Infrastructure.Implementations.Repositories
{
    internal sealed class AppointmentRepository : IBaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly DBContext context;

        public AppointmentRepository(DBContext Context)
        {
            context = Context;
        }

        public Appointment Create(Appointment entity)
        {
            context.Appointments.AddAsync(entity);
            return entity;
        }

        public Appointment Delete(Appointment entity)
        {
            Appointment itemToDelete = context.Appointments.Find(entity.Id);
            context.Appointments.Remove(entity);
            return entity;
        }

        public IQueryable<Appointment> FindAll()
        {
            return context.Appointments.Where(a=> a.IsCancelled == false && a.IsComplete == false).OrderByDescending(a=> a.Id).AsNoTracking();
        }

        public IQueryable<Appointment> FindByCondition(Expression<Func<Appointment, bool>> expression)
        {
            return context.Appointments.Where(expression).AsNoTracking();
        }

        public Appointment Update(Appointment entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
