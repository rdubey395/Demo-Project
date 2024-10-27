using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Model
{
    [Table("Department")]
    public class Department
    {

        [Key]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please Enter Student Name")]
        public string? DepartmentName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
