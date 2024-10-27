using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Validators
{
    public class DateCheckAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if(date > DateTime.Now)
            {
                return new ValidationResult("Date must less than todays date");
            }
            return ValidationResult.Success;
        }
    }
}
