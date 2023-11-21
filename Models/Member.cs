using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Garage_3_MVCEF.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [NotEqual(nameof(FirstName), ErrorMessage = "Last name should not be the same as First name")]
        public string LastName { get; set; }

        [RegularExpression(@"^\d{8}-\d{4}$", ErrorMessage = "Personal number should be in format yyyymmdd-nnnn")]
        public string PersonalNumber { get; set; }
        public DateTime? ProEndTime { get; set; }
            public class NotEqualAttribute : ValidationAttribute
            {
                private readonly string _otherProperty;

                // Correct the constructor to include a default error message
                public NotEqualAttribute(string otherProperty)
                {
                    _otherProperty = otherProperty;
                }

                protected override ValidationResult IsValid(object value, ValidationContext validationContext)
                {
                    var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherProperty);

                    var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

                if (string.Equals(value?.ToString(), otherPropertyValue?.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult(ErrorMessage ?? "Values should not be equal");
                }

                return ValidationResult.Success;
                }
            }
        }


    }
   
