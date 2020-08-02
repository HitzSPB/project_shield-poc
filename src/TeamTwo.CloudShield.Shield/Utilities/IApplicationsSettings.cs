using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTwo.CloudShield.Shield.Utilities
{
  public interface IApplicationsSettings
  {
    string GetProcessEnvironmentVariable(string environmentName);
  }
}
