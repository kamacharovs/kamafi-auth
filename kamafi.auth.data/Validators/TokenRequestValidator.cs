using FluentValidation;
using kamafi.auth.data.models;

namespace kamafi.auth.data.validators
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(x => x.ApiKey)
                .NotEmpty()
                .When(x => x.Email is null 
                        && x.Password is null);

            RuleFor(x => x.Email)
                .NotEmpty()
                .When(x => x.ApiKey is null);

            RuleFor(x => x.Password)
                .NotEmpty()
                .When(x => x.ApiKey is null);
        }
    }
}
