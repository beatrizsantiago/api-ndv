using System.Threading.Tasks;
using Domain.Entities;
using X.PagedList;

namespace Repository.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        // get findById save getAs findByIdAs
        Task <IPagedList<T>> Get(int pageIndex, int pageLimit);
        Task <T> FindById(long id);
        Task Save(T entity);
    }
}