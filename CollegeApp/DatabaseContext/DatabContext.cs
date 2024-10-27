using CollegeApp.Model;
using Microsoft.EntityFrameworkCore;
namespace CollegeApp.DatabaseContext

{
    public class DatabContext : DbContext
    {
        public DatabContext(DbContextOptions<DatabContext> options) : base(options)
        {
            
        }
       public DbSet<Student> Students { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<Usertable> Usertable { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<RolePrivilege> RolePrivilege { get; set; }

        public DbSet<UserRoleMapping> UserRoleMapping { get; set; }

        public DbSet<UserTypeData> UserTypeData { get; set; }
    }
}
