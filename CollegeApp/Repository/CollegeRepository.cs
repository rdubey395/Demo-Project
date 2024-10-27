using CollegeApp.DatabaseContext;
using CollegeApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CollegeApp.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly DatabContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public CollegeRepository(DatabContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            var Allstudents = await _dbSet.ToListAsync();
            return Allstudents;
        }

        public async Task<T> CreateData(T record)
        {
            _dbSet.Add(record);
            await _dbContext.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteData(T record)
        {


            _dbSet.Remove(record);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<T> UpdateData(T record)
        {



            _dbSet.Update(record);
            await _dbContext.SaveChangesAsync();

            return record;


        }
        public async Task<T> GetbyId(Expression<Func<T,bool>> filter, bool useNoTracking = false)
        {
            var Student = new Student();
            if (useNoTracking)

                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();


            else
                return await _dbSet.Where(filter).FirstOrDefaultAsync();
            
        }

        public async Task<T> GetbyName(Expression<Func<T, bool>> filter)
        {
            var Student = await _dbSet.Where(filter).FirstOrDefaultAsync();
            return Student;
        }

        public async Task<List<T>> GetbyFilterId(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            var Student = new Student();
            if (useNoTracking)

                return await _dbSet.AsNoTracking().Where(filter).ToListAsync();


            else
                return await _dbSet.Where(filter).ToListAsync();

        }


    }
}
