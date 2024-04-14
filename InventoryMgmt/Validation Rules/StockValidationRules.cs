using FluentValidation;
using InventoryMgmt.Model;

namespace InventoryMgmt.Validation_Rules
{
    public class StockValidationRules : AbstractValidator<StockModel>
    {
        StockValidationRules()
        {
            RuleFor(x=> x.stockId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Stock Id"))
                .NotNull().WithMessage(NullMessage("Stock Id"))
                .NotEqual(0).WithMessage("User Id Must Not be equal to 0");

            RuleFor(x=> x.quantity).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Quantity"))
                .NotNull().WithMessage(NullMessage("Quantity"))
                .LessThanOrEqualTo(0).WithMessage("Quantity should be greater than 0");

            RuleFor(x => x.expiryDate).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Expiry Date"))
                .NotNull().WithMessage(NullMessage("Expiry Date"))
                .GreaterThan(DateTime.Today).WithMessage("Expiry date should be ");


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
