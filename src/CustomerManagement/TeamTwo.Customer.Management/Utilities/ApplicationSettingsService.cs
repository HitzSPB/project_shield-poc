using System;
using System.Diagnostics.CodeAnalysis;

namespace TeamTwo.Customer.Management.Utilities
{
  [ExcludeFromCodeCoverage]
  public class ApplicationSettingsService : IApplicationSettingsService
  {
    private string GetProccessEnvironmentVariable(string environmentVariableName)
    {
      return Environment.GetEnvironmentVariable(environmentVariableName, EnvironmentVariableTarget.Process);
    }

    string IApplicationSettingsService.AzureStorageAccountConnection => GetProccessEnvironmentVariable("TEAMTWO-AZURE_STORAGE_ACCOUNT_CONNECTION");
  }
}
