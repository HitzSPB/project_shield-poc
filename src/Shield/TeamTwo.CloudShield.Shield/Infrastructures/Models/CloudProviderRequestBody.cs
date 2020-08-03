using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTwo.CloudShield.Shield.Infrastructures.Models
{
  public class CloudProviderRequestBody
  {
    public CloudProviderRequestBody(string tenantId)
    {
      TenantId = tenantId;
    }
    public string TenantId { get; set; }
  }
}
