using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTwo.CloudProvider.Management.Infrastructure.Models
{
  public class GetPolicyKeysResponse
  {
    public string PrimaryConnectionString { get; set; }
    public string PrimaryKey { get; set; }
    public string KeyName { get; set; }
  }
}
