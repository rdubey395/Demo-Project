using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Model
{
    [Table("Usertable")]
    public class Usertable
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string? Password { get; set; }
        public string? PasswordSalt { get; set; }
        public int? UserType { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        //[DateCheck]
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }

        public virtual UserTypeData UserTypeData { get; set; }




    }
}
