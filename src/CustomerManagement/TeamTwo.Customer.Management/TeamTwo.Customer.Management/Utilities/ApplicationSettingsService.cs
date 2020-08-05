using System;

namespace TeamTwo.Customer.Management.Utilities
{
  public class ApplicationSettingsService : IApplicationSettingsService
  {
    public string GetProccessEnvironmentVariable(string environmentVariableName)
    {
      return Environment.GetEnvironmentVariable(environmentVariableName, EnvironmentVariableTarget.Process);
    }
  }
}
