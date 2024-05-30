using FluentValidation;
using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;

namespace InventoryMgmt.Validation_Rules
{
    public class StoreValidationRules : AbstractValidator<StoreRegisterModel>
    {
        public StoreValidationRules()
        {
            RuleFor(x => x.storeId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Store Id"))
                .NotNull().WithMessage(NullMessage("Store Id"))
                .NotEqual(0).WithMessage("Store Id Must Not be equal to 0");
            RuleFor(x => x.storeName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(EmptyMessage("Store Name"))
                .NotNull().WithMessage(NullMessage("Store Name"));
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
