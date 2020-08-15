using System;

namespace TeamTwo.CloudShield.ShieldController.Utilities
{
  public class ApplicationSettingsService : IApplicationSettingsService
  {
    private string GetProcessEnvironmentVariable(string environmentName)
    {
      return Environment.GetEnvironmentVariable(environmentName, EnvironmentVariableTarget.Process);
    }

    public string ShieldUrl => GetProcessEnvironmentVariable("TEAMTWO-SHIELD_MANAGEMENT_URL");
    public string CustomerManagementUrl => GetProcessEnvironmentVariable("TEAMTWO-CUSTOMER_MANAGEMENT_URL");
  }
}
