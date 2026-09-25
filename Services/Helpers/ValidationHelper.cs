using System.ComponentModel.DataAnnotations;

namespace Services.Helpers;

public class ValidationHelper
{
    internal static void ModelValidation(object obj)
    {
        //Model Validations
        ValidationContext validationContext = new ValidationContext(obj);
        List<ValidationResult> validationResults = new List<ValidationResult>();
        
        //validate the model object and get errors
        bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

        if (!isValid)
        {
            throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }
}