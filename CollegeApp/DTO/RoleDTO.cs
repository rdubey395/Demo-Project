using System.ComponentModel.DataAnnotations;

namespace CollegeApp.DTO
{
    public class RoleDTO
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public string? RoleName { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
