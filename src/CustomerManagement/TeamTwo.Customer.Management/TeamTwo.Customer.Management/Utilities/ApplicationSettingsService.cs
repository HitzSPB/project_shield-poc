using System;
using System.Collections.Generic;
using System.Text;

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
