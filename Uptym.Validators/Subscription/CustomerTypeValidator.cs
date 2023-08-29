using FluentValidation;
using Uptym.DTO.Subscription.CustomerType;

namespace Uptym.Validators.Subscription
{
    public class CustomerTypeValidator : AbstractValidator<CustomerTypeDto>
    {

        public CustomerTypeValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Customer Type Name cannot be empty");

        }

    }
}
