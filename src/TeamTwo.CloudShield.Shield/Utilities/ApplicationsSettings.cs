using System;

namespace TeamTwo.CloudShield.Shield.Utilities
{
  public class ApplicationsSettings : IApplicationsSettings
  {
    string IApplicationsSettings.GetProcessEnvironmentVariable(string environmentName)
    {
      return Environment.GetEnvironmentVariable(environmentName, EnvironmentVariableTarget.Process);
    }
  }
}
