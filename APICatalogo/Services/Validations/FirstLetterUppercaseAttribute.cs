using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Services.Validations
{
    public class FirstLetterUppercaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string firstLetter = value.ToString()[0].ToString();
            if(firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome deve ser maiúscula.");
            }

            return ValidationResult.Success;
        }
    }
}
