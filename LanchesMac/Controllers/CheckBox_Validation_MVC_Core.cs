using LanchesMac.ViewModels;
using System.ComponentModel.DataAnnotations;

public class CheckBoxRequired : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        //get the entered value
        var student = (LoginViewModel)validationContext.ObjectInstance;
        //Check whether the IsAccepted is selected or not.

        return ValidationResult.Success;
    }
}