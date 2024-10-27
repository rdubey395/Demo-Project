using System.ComponentModel.DataAnnotations;

namespace CollegeApp.DTO
{
    public class LoginModelDTO
    {
        [Required]
        public string PolicyName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    

       
    
}
