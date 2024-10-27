using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Model
{
    [Table("RolePrivilege")]
    public class RolePrivilege
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public string? RolePrivilegeName { get; set; }

        public string? Description { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        //[DateCheck]
        public DateTime? CreatedDate { get; set; }

        public Roles Role { get; set; }
    }
}
