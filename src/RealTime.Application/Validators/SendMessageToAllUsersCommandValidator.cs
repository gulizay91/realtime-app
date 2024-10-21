using FluentValidation;
using RealTime.Application.Contracts.Commands;

namespace RealTime.Application.Validators;

public class SendMessageToAllUsersCommandValidator: AbstractValidator<SendMessageToAllUsersCommand>
{
  public SendMessageToAllUsersCommandValidator()
  {
    RuleFor(x => x.Message)
      .NotEmpty().WithMessage("Message cannot be empty.")
      .NotNull().WithMessage("Message is required.");
  }
}