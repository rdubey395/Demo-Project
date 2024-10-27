using CollegeApp.DatabaseContext;
using CollegeApp.Model;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Repository
{
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository
    {
        private readonly DatabContext _dbContext;

        public StudentRepository(DatabContext context) : base(context) 
        {
            _dbContext = context;
        }
       
    }
}
