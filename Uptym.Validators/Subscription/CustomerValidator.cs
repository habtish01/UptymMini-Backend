using FluentValidation;
using System.Text.RegularExpressions;
using Uptym.DTO.Subscription.Customer;

namespace Uptym.Validators.Subscription
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        private readonly Regex EmailRegex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        private readonly Regex PhoneRegex = new Regex(@"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}");
        private readonly Regex FirstAndLastNameRegex = new Regex(@"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]*$");

        public CustomerValidator()
        {

            //RuleFor(x => x.FirstName)
            //    .NotEmpty().NotNull().WithMessage("First Name cannot be empty")
            //    .Length(2, 50).WithMessage("First Name length should not exceed 50 character")
            //    .Matches(FirstAndLastNameRegex).WithMessage("First Name only allow characters");

            //RuleFor(x => x.LastName)
            //    .NotEmpty().NotNull().WithMessage("Last Name cannot be empty")
            //    .Length(2, 50).WithMessage("First Name length should not exceed 50 character")
            //    .Matches(FirstAndLastNameRegex).WithMessage("Last Name only allow characters");

            RuleFor(x => x.Email)
                .NotEmpty().NotNull().WithMessage("Email cannot be empty")
                .Matches(EmailRegex).WithMessage("User email must be in a valid format. For Example: xyz@yahoo.com");

            //RuleFor(x => x.PhoneNumber)
            //    .NotEmpty().NotNull().WithMessage("Phone cannot be empty")
            //    .Must(BeValidPhone).WithMessage("Please type a valid phone number");
            //.Matches(PhoneRegex).WithMessage("User phone number must be in a valid format. For Example: 123456789");

        }

        private bool BeValidPhone(CustomerDto customerDto, string newValue)
        {
            return (new GeneralValidators()).IsPhoneValid(newValue);
        }

    }
}
