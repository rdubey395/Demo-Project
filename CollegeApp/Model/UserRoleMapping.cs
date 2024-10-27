using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Model
{
    [Table("UserRoleMapping")]
    public class UserRoleMapping
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public Usertable Usertable { get; set; }

        public Roles Roles { get; set; }
    }
}
