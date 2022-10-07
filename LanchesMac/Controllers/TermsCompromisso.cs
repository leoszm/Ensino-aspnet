using LanchesMac.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Controllers
{
    public class TermsCompromisso
    {
        public class CheckBoxRequired : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                //get the entered value
                var student = (LoginViewModel)validationContext.ObjectInstance;
                //Check whether the IsAccepted is selected or not.
                if (student.IsAccepted == false)
                {
                    //if not checked the checkbox, return the error message.
                    return new ValidationResult(ErrorMessage == null ? "Please checked the checkbox" : ErrorMessage);
                }
                return ValidationResult.Success;
            }
        }
    }
}
