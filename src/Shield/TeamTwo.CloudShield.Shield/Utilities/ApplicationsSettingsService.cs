using System;

namespace TeamTwo.CloudShield.Shield.Utilities
{
  public class ApplicationsSettingsService : IApplicationsSettingsService
  {
    string IApplicationsSettingsService.GetProcessEnvironmentVariable(string environmentName)
    {
      return Environment.GetEnvironmentVariable(environmentName, EnvironmentVariableTarget.Process);
    }
  }
}
