using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTwo.CloudProvider.Management.Services.Models
{
  public class PolicyDto
  {

    public string PolicyName { get; set; }
    public string PolicyKey { get; set; }
    public PolicyClaim PolicyType { get; set; }
  }
}
