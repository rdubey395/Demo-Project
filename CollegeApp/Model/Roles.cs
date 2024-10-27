using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Model
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public string? RoleName { get; set; }
       
        public string? Description { get; set; }
       
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        //[DateCheck]
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<RolePrivilege> RolePrivilege { get; set; }

        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }


    }
}
