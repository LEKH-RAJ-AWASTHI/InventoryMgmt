using FluentValidation;
using InventoryMgmt.Model.ApiUseModel;
using InventoryMgmt.Model.DTOs;

namespace InventoryMgmt.Validation_Rules
{
    public class SalesDTOValidation : AbstractValidator<AddSalesModel>
    {
        public SalesDTOValidation()
        {
            RuleFor(x => x.ItemId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Item Id"))
                .NotNull().WithMessage(NullMessage("Item Id"))
                .NotEqual(0).WithMessage("Item Id Must Not be equal to 0");
            RuleFor(x => x.StoreId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Store Id"))
                .NotNull().WithMessage(NullMessage("Store Id"))
                .NotEqual(0).WithMessage("Store Id Must Not be equal to 0");
            RuleFor(x => x.Quantity).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Quantity Id"))
                .NotNull().WithMessage(NullMessage("Quantity Id"))
                .NotEqual(0).WithMessage("Quantity Id Must Not be equal to 0");
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
