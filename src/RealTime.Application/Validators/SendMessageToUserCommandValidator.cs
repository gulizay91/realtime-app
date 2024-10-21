using FluentValidation;
using RealTime.Application.Contracts.Commands;

namespace RealTime.Application.Validators;

public class SendMessageToUserCommandValidator: AbstractValidator<SendMessageToUserCommand>
{
  public SendMessageToUserCommandValidator()
  {
    RuleFor(x => x.UserId)
      .NotEmpty().WithMessage("UserId cannot be empty.")
      .NotNull().WithMessage("UserId is required.");
        
    RuleFor(x => x.Message)
      .NotEmpty().WithMessage("Message cannot be empty.")
      .NotNull().WithMessage("Message is required.");
  }
}