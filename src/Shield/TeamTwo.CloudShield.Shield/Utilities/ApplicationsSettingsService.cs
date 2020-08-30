using System;
using System.Diagnostics.CodeAnalysis;

namespace TeamTwo.CloudShield.Shield.Utilities
{
  [ExcludeFromCodeCoverage]
  public class ApplicationsSettingsService : IApplicationsSettingsService
  {
    string IApplicationsSettingsService.CloudProviderUrl => GetProcessEnvironmentVariable("TEAMTWO-CLOUD_PROVIDER_URL");

    string IApplicationsSettingsService.AccountEndPoint => GetProcessEnvironmentVariable("TEAMTWO-ACCOUNT_ENDPOINT");

    string IApplicationsSettingsService.Authkey => GetProcessEnvironmentVariable("TEAMTWO-AUTH_KEY");

    private string GetProcessEnvironmentVariable(string environmentName)
    {
      return Environment.GetEnvironmentVariable(environmentName, EnvironmentVariableTarget.Process);
    }


  }
}
