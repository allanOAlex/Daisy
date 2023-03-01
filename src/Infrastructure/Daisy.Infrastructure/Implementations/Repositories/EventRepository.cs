using Daisy.Application.Abstractions.IRepositories;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Daisy.Infrastructure.Implementations.Repositories
{
    internal sealed class EventRepository : IBaseRepository<Event>, IEventRepository
    {
        private readonly DBContext context;
        //private readonly IConfiguration configuration;

        public EventRepository(DBContext Context)
        {
            context = Context;
        }

        public Event Create(Event entity)
        {
            context.Events.AddAsync(entity);
            return entity;
        }

        public Event Delete(Event entity)
        {
            Event eventToDelete = context.Events.Find(entity.Id);
            context.Events.Remove(entity);
            return entity;
        }

        public IQueryable<Event> FindAll()
        {
            return context.Events.Where(e=> e.IsCancelled == false).OrderByDescending(e=> e.Id).AsNoTracking();
        }

        public IQueryable<Event> FindByCondition(Expression<Func<Event, bool>> expression)
        {
            return context.Events.Where(expression).AsNoTracking();
        }

        public Event Update(Event entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
