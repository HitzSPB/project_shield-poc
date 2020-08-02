using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.File;

namespace TeamTwo.CloudShield.Shield.Service.Model
{
  public class PolicyDto
  {

    public string PolicyName { get; set; }
    public string PolicyKey { get; set; }
    public PolicyClaim PolicyType { get; set; }
  }
}
