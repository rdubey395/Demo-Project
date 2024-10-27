using System.ComponentModel.DataAnnotations;

namespace CollegeApp.DTO
{
    public class RolePrivilegeDTO
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public string? RolePrivilegeName { get; set; }

        public int? RoleId { get; set; }
        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
