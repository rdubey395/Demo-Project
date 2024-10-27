using CollegeApp.Validators;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Student Name")]
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage = "Please neter valid email address")]
        public string? Email { get; set; }
        public string? Address { get; set; }
        //[DateCheck]
        public DateTime? DOB { get; set; }
    }
}
