using FluentValidation;
using InventoryMgmt.Model;

namespace InventoryMgmt.Validation_Rules
{
    public class LoginUserValidationRules : AbstractValidator<LoginModel>
    {
        public LoginUserValidationRules()
        {
            RuleFor(x => x.username).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("UserName"))
                .NotNull().WithMessage(NullMessage("UserName"));

            RuleFor(x => x.password).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("password"))
                .NotNull().WithMessage(NullMessage("password"))
                .MinimumLength(4).WithMessage("Password length should be atleast 4");
        }
        private string NullMessage(string property)
        {
            return $"{property} Cannot be null";
        }
        private string EmptyMessage(string property)
        {
            return $"{property} Cannot be Empty";
        }
    }
}
