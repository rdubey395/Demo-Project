using System.ComponentModel.DataAnnotations;

namespace CollegeApp.DTO
{
    public class UserDTO
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string? Password { get; set; }
        
        public int? UserType { get; set; }
        public bool? IsActive { get; set; }
    }
}
