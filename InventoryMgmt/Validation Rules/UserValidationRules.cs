
using FluentValidation;
using InventoryMgmt.Model;

namespace InventoryMgmt.Validation_Rules
{
    public class UserValidationRules : AbstractValidator<RegisterUserModel>
    {
        public UserValidationRules() 
        {
            //RuleFor(x=>x.userId).Cascade(CascadeMode.Stop)
            //    .NotEmpty().WithMessage(EmptyMessage("User Id"))
            //    .NotNull().WithMessage(NullMessage("User Id"))
            //    .NotEqual(0).WithMessage("User Id Must Not be equal to 0");

            RuleFor(x => x.username).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("UserName"))
                .NotNull().WithMessage(NullMessage("UserName"));

            RuleFor(x => x.password).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("password"))
                .NotNull().WithMessage(NullMessage("password"))
                .MinimumLength(4).WithMessage("Password length should be atleast 4");

            RuleFor(x => x.fullName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("FullName"))
                .NotNull().WithMessage(NullMessage("FullName"));

            RuleFor(x => x.role).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Role"))
                .NotNull().WithMessage(NullMessage("Role"))
                .Must(role => role == "Admin" || role == "User")
                .WithMessage("The role must be either 'Admin' or 'User'");

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
