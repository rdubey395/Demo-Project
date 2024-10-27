using CollegeApp.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Model
{
     [Table("Students")]
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Student Name")]
        public string? StudentName { get; set; }
        [EmailAddress(ErrorMessage = "Please neter valid email address")]
        public string? Email { get; set; }
        public string? Address { get; set; }
        //[DateCheck]
        public DateTime? DOB { get; set; }

        public int? DepartmentId { get; set; }

       public virtual Department Department { get; set; }

        //public string Password {  get; set; }
        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }
    }
}
