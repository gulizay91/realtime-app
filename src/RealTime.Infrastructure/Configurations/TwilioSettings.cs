using System.ComponentModel.DataAnnotations;

namespace RealTime.Infrastructure.Configurations;

public class TwilioSettings
{
  [Required(ErrorMessage = "Account SID is required")]
  public string AccountSID { get; set; } = string.Empty;

  [Required(ErrorMessage = "Auth Token is required")]
  public string AuthToken { get; set; } = string.Empty;

  [Required(ErrorMessage = "Phone Number is required")]
  public string PhoneNumber { get; set; } = string.Empty;
  
  public SmsProviderSettings SmsProviderSettings { get; set; } = new();
}