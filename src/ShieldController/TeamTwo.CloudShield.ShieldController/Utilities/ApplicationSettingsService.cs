using System;

namespace TeamTwo.CloudShield.ShieldController.Utilities
{
  public class ApplicationSettingsService : IApplicationSettingsService
  {
    private string GetProcessEnvironmentVariable(string environmentName)
    {
      return Environment.GetEnvironmentVariable(environmentName, EnvironmentVariableTarget.Process);
    }

    public string ShieldUrl => GetProcessEnvironmentVariable("ShieldManagementUrl");
    public string CustomerManagementUrl => GetProcessEnvironmentVariable("CustomerManagementUrl");
  }
}
