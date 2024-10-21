using System.ComponentModel.DataAnnotations;

namespace RealTime.Infrastructure.Configurations;

public class ProxySettings
{
  [Required(ErrorMessage = "Url is required")]
  public string Url { get; set; }

  [Required(ErrorMessage = "Timeout is required")]
  public int Timeout { get; set; } = 3;

  [Required(ErrorMessage = "RetryAttempts is required")]
  public int RetryAttempts { get; set; } = 5;
}