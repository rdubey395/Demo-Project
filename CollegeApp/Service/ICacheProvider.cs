using CollegeApp.Model;

namespace CollegeApp.Service
{
    public interface ICacheProvider
    {
        Task<IEnumerable<Student>> GetCachedResponse();

        //Task<IEnumerable<Student>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphore);
    }
}
