using CollegeApp.DTO;
using CollegeApp.Model;
using CollegeApp.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace CollegeApp.Service
{
    public class CacheProvider : ICacheProvider
    {
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
        private readonly ICollegeRepository<Student> _collegeRepository;
        public CacheProvider(IMemoryCache memoryCache, ICollegeRepository<Student> collegeRepository)
        {
            _cache = memoryCache;
            _collegeRepository = collegeRepository;
        }

        public async Task<IEnumerable<Student>> GetCachedResponse()
        {
            try
            {
                return await GetCachedResponse(CacheKeys.Employees, GetUsersSemaphore);
            }
            catch
            {
                throw;
            }
        }

        private async Task<IEnumerable<Student>> GetCachedResponse(string cacheKeys, SemaphoreSlim semaphore)
        {
            bool Available = _cache.TryGetValue(cacheKeys, out List<Student> students);
            if (Available) {
                return students;
            }
            try
            {
                
               
                    await semaphore.WaitAsync();
                    Available = _cache.TryGetValue(cacheKeys, out students);
                    if (Available)
                    {
                        return students;

                    }
                    students = await _collegeRepository.GetAll();
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        SlidingExpiration = TimeSpan.FromMinutes(2),
                        Size = 2056,

                    };
                    _cache.Set(cacheKeys, students, cacheEntryOptions);

            }
            catch
            {
                throw;
            }
            finally
            {
                semaphore.Release();
            }
            return students;

        }
    }
}
