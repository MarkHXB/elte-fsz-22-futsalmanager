using System.ComponentModel.DataAnnotations;

namespace FutsalManager.Extensions;

[AttributeUsage(AttributeTargets.Property)]
public class CustomFileRequired : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        ValidationResult validationResult = null;

        // Check if Model is WorkphoneViewModel, if so, activate validation
        if (validationContext.ObjectInstance.GetType() == typeof(IFormFile)
            && string.IsNullOrWhiteSpace((string)value) == true)
        {
            this.ErrorMessage = "Phone is required";
            validationResult = new ValidationResult(this.ErrorMessage);
        }
        else
        {
            validationResult = ValidationResult.Success;
        }

        return validationResult;
    }
}