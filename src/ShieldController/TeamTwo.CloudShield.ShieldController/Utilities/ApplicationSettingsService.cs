using System;

namespace TeamTwo.CloudShield.ShieldController.Utilities
{
  public class ApplicationSettingsService : IApplicationSettingsService
  {
    string IApplicationSettingsService.GetProcessEnvironmentVariable(string environmentName)
    {
      return Environment.GetEnvironmentVariable(environmentName, EnvironmentVariableTarget.Process);
    }
  }
}
