using FluentValidation;
using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;

namespace InventoryMgmt.Validations
{
    public class AddItemValidationRules : AbstractValidator<AddItemFormModel>
    {
        public AddItemValidationRules()
        {
            //RuleFor(x=> x.ItemId).Cascade(CascadeMode.Stop)
            //    .NotEmpty().WithMessage(EmptyMessage("Item Id"))
            //    .NotNull().WithMessage(NullMessage("Item Id"))
            //    .NotEqual(0).WithMessage("Item Id Must Not be equal to 0");
            //RuleSet("WithoutStore", () =>
            //{ 

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
                .GreaterThan(0).WithMessage("Purchase Rate should be greater than 0");


            RuleFor(x => x.salesRate).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Sales Rate"))
                .NotNull().WithMessage(NullMessage("Sales Rate"))
                .GreaterThan(0).WithMessage("Sales Rate should be greater than 0");

            RuleFor(x => x.quantity).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Quantity"))
                .NotNull().WithMessage(NullMessage("Quantity"))
                .GreaterThan(0).WithMessage("Quantity should be greater than 0");

            RuleFor(x => x.expiryDate).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Expiry Date"))
                .NotNull().WithMessage(NullMessage("Expiry Date"))
                .GreaterThan(DateTime.Today).WithMessage("Expiry date should be ");
            //});


            RuleFor(x => x.storeName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Store Name"))
                .NotNull().WithMessage(NullMessage("Store Name"));
            //RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive is required");
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
