using FluentValidation;
using RealTime.Application.Contracts.Commands;
using RealTime.Application.Utils;

namespace RealTime.Application.Validators;

public class SendSmsCommandValidator : AbstractValidator<SendSmsCommand>
{
  public SendSmsCommandValidator()
  {
    // PhoneNumber validation: Required and should be in a valid format
    RuleFor(x => x.PhoneNumber)
      .NotEmpty().WithMessage("Phone number is required.")
      .Must(PhoneNumberUtils.IsPhoneNumberFormatValid).WithMessage("Phone number format is invalid.");

    // Message validation: Required and should not be empty
    RuleFor(x => x.Message)
      .NotEmpty().WithMessage("Message is required.")
      .MaximumLength(160).WithMessage("Message cannot be longer than 160 characters.");
  }
}