using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Model
{
    [Table("UserTypeData")]
    public class UserTypeData
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public string Name { get; set; }

        public string  Description { get; set; }

        public virtual ICollection<Usertable> Usertable { get; set; }
    }
}
