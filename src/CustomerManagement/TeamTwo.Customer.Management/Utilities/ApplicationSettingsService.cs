using System;

namespace TeamTwo.Customer.Management.Utilities
{
  public class ApplicationSettingsService : IApplicationSettingsService
  {
    private string GetProccessEnvironmentVariable(string environmentVariableName)
    {
      return Environment.GetEnvironmentVariable(environmentVariableName, EnvironmentVariableTarget.Process);
    }

    string IApplicationSettingsService.AzureStorageAccountConnection => GetProccessEnvironmentVariable("TEAMTWO-AZURE_STORAGE_ACCOUNT_CONNECTION");
  }
}
