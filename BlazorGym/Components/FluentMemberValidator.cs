using FluentValidation;
using System.Net.Mail;

namespace BlazorGym.Components;

public partial class FluentMemberValidator : AbstractValidator<Member>
{
    public FluentMemberValidator()
    {
        RuleFor(member => member.Name)
            .NotEmpty().WithMessage("Name is mandatory")
            .MaximumLength(100).WithMessage("Name cannot be longer that 100 characters");

        RuleFor(member => member.Email)
            .NotEmpty().WithMessage("Email is mandatory");

        When(member => member.Email is { Length: > 0 }, () =>
        {
            RuleFor(member => member.Email)
                .Must(IsValidEmail).WithMessage("This is not a valid e-mail address");
        });
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return mailAddress.Host.Contains('.');
        }
        catch
        {
            return false;
        }
    }
}