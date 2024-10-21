using System.ComponentModel.DataAnnotations;

namespace RealTime.Infrastructure.Configurations;

public class SmsProviderSettings
{
  [Required(ErrorMessage = "Enable is required")]
  public bool Enable { get; set; } = false;

  [Required(ErrorMessage = "ActiveCountryCodes is required")]
  public string[] ActiveCountryCodes { get; set; } = [];
}