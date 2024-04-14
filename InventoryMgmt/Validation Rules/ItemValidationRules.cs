using FluentValidation;
using InventoryMgmt.Model;

namespace InventoryMgmt.Validations
{
    public class ItemValidationRules : AbstractValidator<ItemModel>
    {
        public ItemValidationRules()
        {
            RuleFor(x=> x.ItemId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Item Id"))
                .NotNull().WithMessage(NullMessage("Item Id"))
                .NotEqual(0).WithMessage("Item Id Must Not be equal to 0");

            RuleFor(x => x.ItemCode).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Item Code"))
                .NotNull().WithMessage(NullMessage("Item Code"));

            RuleFor(x => x.ItemName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Item Name"))
                .NotNull().WithMessage(NullMessage("Item Name"));

            RuleFor(x => x.BrandName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Brand Name"))
                .NotNull().WithMessage(NullMessage("Brand Name"));

            RuleFor(x => x.UnitOfMeasurement).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Unit of Measurement"))
                .NotNull().WithMessage(NullMessage("Unit of Measurement"));

            RuleFor(x => x.PurchaseRate).Cascade(CascadeMode.Stop).   
                NotEmpty().WithMessage(EmptyMessage("Purchase Rate"))
                .NotNull().WithMessage(NullMessage("Purchase Rate"))
                .LessThanOrEqualTo(0).WithMessage("Purchase Rate should be greater than 0");

            RuleFor(x => x.SalesRate).Cascade(CascadeMode.Stop).NotEmpty()
                .WithMessage(EmptyMessage("Sales Rate"))
                .NotNull().WithMessage(NullMessage("Sales Rate"))
                .LessThanOrEqualTo(0).WithMessage("Sales Rate should be greater than 0");
            

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
