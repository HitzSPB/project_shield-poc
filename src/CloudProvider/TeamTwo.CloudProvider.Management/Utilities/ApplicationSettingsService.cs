using System;
using System.Diagnostics.CodeAnalysis;

namespace TeamTwo.CloudProvider.Management.Utilities
{
  [ExcludeFromCodeCoverage]
  public class ApplicationSettingsService : IApplicationSettingsService
  {
    private string GetProcessEnvironmentVariable(string environmentName)
    {
      return Environment.GetEnvironmentVariable(environmentName, EnvironmentVariableTarget.Process);
    }

    string IApplicationSettingsService.AadTenantId => GetProcessEnvironmentVariable("TEAMTWO-AAD_TENANTID");

    string IApplicationSettingsService.AadClientId => GetProcessEnvironmentVariable("TEAMTWO-AAD_CLIENTID");

    string IApplicationSettingsService.AadClientSecret => GetProcessEnvironmentVariable("TEAMTWO-AAD_CLIENTSECRET");

    string IApplicationSettingsService.AzureManagementApi => GetProcessEnvironmentVariable("AZURE-MANAGEMENTAPI");

    string IApplicationSettingsService.AzureSubscriptionId => GetProcessEnvironmentVariable("TEAMTWO-SUBSCRIPTIONID");

    string IApplicationSettingsService.ResourceGroupname => GetProcessEnvironmentVariable("TEAMTWO-RSGNAME");

    string IApplicationSettingsService.RelayNameSpace => GetProcessEnvironmentVariable("TEAMTWO-RELAY_NAMESPACE");

  }
}
