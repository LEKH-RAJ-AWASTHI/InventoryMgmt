using FluentValidation;
using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;
using InventoryMgmt.Validation_Rules;
using InventoryMgmt.Validations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace InventoryMgmt.DependencyInjections
{
    public static class ValidationRulesDependency
    {
        public static void RegisterFluentValidationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserModel>, UserValidationRules>();
            services.AddScoped<IValidator<LoginModel>, LoginUserValidationRules>();
            services.AddScoped<IValidator<AddItemFormModel>, AddItemValidationRules >();
            services.AddScoped<IValidator<ItemFormModel>, UpdateItemValidationRules >();

        }
    }
}
