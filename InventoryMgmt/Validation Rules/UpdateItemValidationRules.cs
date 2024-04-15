using FluentValidation;
using InventoryMgmt.Model.ApiUseModel;

namespace InventoryMgmt.Validation_Rules
{
    public class UpdateItemValidationRules : AbstractValidator<ItemFormModel>
    {
        public UpdateItemValidationRules()
        {
            RuleFor(x => x.itemCode).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Item Code"))
                .NotNull().WithMessage(NullMessage("Item Code"));

            RuleFor(x => x.itemName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Item Name"))
                .NotNull().WithMessage(NullMessage("Item Name"));

            RuleFor(x => x.brandName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Brand Name"))
                .NotNull().WithMessage(NullMessage("Brand Name"));

            RuleFor(x => x.unitOfMeasurement).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Unit of Measurement"))
                .NotNull().WithMessage(NullMessage("Unit of Measurement"));

            RuleFor(x => x.purchaseRate).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage(EmptyMessage("Purchase Rate"))
                .NotNull().WithMessage(NullMessage("Purchase Rate"))
                .LessThanOrEqualTo(0).WithMessage("Purchase Rate should be greater than 0");

            RuleFor(x => x.salesRate).Cascade(CascadeMode.Stop).NotEmpty()
                .WithMessage(EmptyMessage("Sales Rate"))
                .NotNull().WithMessage(NullMessage("Sales Rate"))
                .LessThanOrEqualTo(0).WithMessage("Sales Rate should be greater than 0");

            RuleFor(x => x.quantity).Cascade(CascadeMode.Stop)
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
