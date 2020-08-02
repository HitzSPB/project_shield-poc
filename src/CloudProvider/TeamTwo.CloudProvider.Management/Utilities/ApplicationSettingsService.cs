using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTwo.CloudProvider.Management.Utilities
{
  public class ApplicationsSettings : IApplicationsSettings
  {
    string IApplicationsSettings.GetProcessEnvironmentVariable(string environmentName)
    {
      return Environment.GetEnvironmentVariable(environmentName, EnvironmentVariableTarget.Process);
    }
  }
}
