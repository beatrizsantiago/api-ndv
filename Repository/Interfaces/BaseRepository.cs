using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        protected readonly IMapper mapper;

        protected BaseRepository(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.Set = context.Set<T>();
            this.mapper = mapper;
        }

        public virtual Task<T> FindById(long id)
        {
            return Set.FindAsync(id).AsTask();
        }

        public virtual Task<TR> FindByIdAs<TR>(long id)
        {
            return Set.Where(e => e.Id == id).ProjectTo<TR>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public Task<IPagedList<T>> Get(int pageIndex, int pageLimit)
        {
            return Set.ToPagedListAsync(pageIndex, pageLimit);
        }

        public virtual Task<IPagedList<TR>> GetAs<TR>(int pageIndex, int pageLimit)
        {
            return Set.ProjectTo<TR>(mapper.ConfigurationProvider).ToPagedListAsync(pageIndex, pageLimit);
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

        public Task Delete(T entity)
        {
            Set.Remove(entity);
            return context.SaveChangesAsync();
        }
    }
}