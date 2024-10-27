using CollegeApp.Model;
using System.Linq.Expressions;

namespace CollegeApp.Repository
{
    public interface ICollegeRepository<T> 
    {
        Task<List<T>> GetAll();

        Task<T> GetbyId(Expression<Func<T, bool>> filter, bool useNoTracking = false);

        Task<T> GetbyName(Expression<Func<T, bool>> filter);

        Task<T> CreateData(T record);

        Task<bool> DeleteData(T record);

        Task<T> UpdateData(T record);

        Task<List<T>> GetbyFilterId(Expression<Func<T, bool>> filter, bool useNoTracking = false);
    }
}
