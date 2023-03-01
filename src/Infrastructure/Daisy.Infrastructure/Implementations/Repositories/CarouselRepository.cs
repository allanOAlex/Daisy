using Daisy.Application.Abstractions.IRepositories;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Infrastructure.Implementations.Repositories
{
    internal sealed class CarouselRepository : IBaseRepository<Carousel>, ICarouselRepository
    {
        private readonly DBContext context;
        public CarouselRepository(DBContext Context)
        {
            context = Context;
        }

        public Carousel Create(Carousel entity)
        {
            context.Carousels.AddAsync(entity);
            return entity;
        }

        public Carousel Delete(Carousel entity)
        {
            //Carousel carouselToDelete = context.Carousels.Find(entity.Id);
            //context.Carousels.Remove(entity);

            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Carousel> FindAll()
        {
            return context.Carousels.Where(c=> c.IsDeleted == false).OrderByDescending(c=> c.Id).AsNoTracking();
        }

        public IQueryable<Carousel> FindByCondition(Expression<Func<Carousel, bool>> expression)
        {
            return context.Carousels.Where(expression).AsNoTracking();
        }

        public Carousel Update(Carousel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
