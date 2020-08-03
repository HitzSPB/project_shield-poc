using System;

namespace TeamTwo.CloudProvider.Management.Utilities
{
  public class ApplicationSettingsService : IApplicationSettingsService
  {
    string IApplicationSettingsService.GetProcessEnvironmentVariable(string environmentName)
    {
      return Environment.GetEnvironmentVariable(environmentName, EnvironmentVariableTarget.Process);
    }
  }
}
