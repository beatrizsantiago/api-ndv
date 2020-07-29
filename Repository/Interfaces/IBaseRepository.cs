using System.Threading.Tasks;
using Domain.Entities;
using X.PagedList;

namespace Repository.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        Task<IPagedList<T>> Get(int pageIndex, int pageLimit);
        Task<IPagedList<TR>> GetAs<TR>(int pageIndex, int pageLimit);
        Task<T> FindById(long id);
        Task<TR> FindByIdAs<TR>(long id);
        Task Save(T entity);
    }
}