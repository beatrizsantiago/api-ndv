using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using X.PagedList;

namespace Repository.Interfaces
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly ApplicationContext context;
        protected readonly DbSet<T> Set;

        protected BaseRepository(ApplicationContext context)
        {
            this.context = context;
            this.Set = context.Set<T>();
        }

        public Task<T> FindById(long id)
        {
            return Set.FindAsync(id).AsTask();
        }

        public Task<IPagedList<T>> Get(int pageIndex, int pageLimit)
        {
            return Set.ToPagedListAsync(pageIndex, pageLimit);
        }

        public async Task Save(T entity)
        {
            var dbEntity = await FindById(entity.Id);
            if (dbEntity is null)
            {
                entity.CreatedDate = DateTime.Now;
                await Set.AddAsync(entity);
            }
            else
            {
                dbEntity.UpdatedDate = DateTime.Now;
                var properties = dbEntity.GetType().GetProperties().Where(property => property.PropertyType.IsSimpleType());
                foreach (var property in properties)
                {
                    var dbEntityProperty = property;
                    var dbEntityValue = dbEntityProperty.GetValue(dbEntity);

                    var entityProperty = entity.GetType().GetProperty(dbEntityProperty.Name);
                    var entityValue = entityProperty?.GetValue(entity);

                    if (dbEntityValue?.GetHashCode() != entityValue?.GetHashCode())
                    {
                        dbEntityProperty.SetValue(dbEntity, entityValue);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}