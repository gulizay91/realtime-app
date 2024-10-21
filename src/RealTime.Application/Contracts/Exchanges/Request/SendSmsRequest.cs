namespace RealTime.Application.Contracts.Exchanges.Request;

public record SendSmsRequest
{
  public Receiver Receiver { get; init; }
  public string TextMessage { get; init; }
  
  public SendSmsRequest(Receiver receiver, string textMessage)
  {
    if (string.IsNullOrWhiteSpace(receiver.PhoneNumber))
    {
      throw new ArgumentException("Phone number cannot be empty", nameof(receiver.PhoneNumber));
    }
    if (string.IsNullOrWhiteSpace(textMessage))
    {
      throw new ArgumentException("Message cannot be empty", nameof(textMessage));
    }
    Receiver = receiver;
    TextMessage = textMessage;
  }
}

public record Receiver
{
  public string PhoneNumber { get; init; }
  
  public Receiver(string phoneNumber)
  {
    if (string.IsNullOrWhiteSpace(phoneNumber))
    {
      throw new ArgumentException("Phone number cannot be empty", nameof(phoneNumber));
    }
    PhoneNumber = phoneNumber;
  }
}