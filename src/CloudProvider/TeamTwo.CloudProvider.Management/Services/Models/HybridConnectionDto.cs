using System;

namespace TeamTwo.CloudProvider.Management.Services.Models
{
  public class HybridConnectionDto
  {
    public HybridConnectionDto(Uri hybridConnectionUrl, PolicyDto[] policyDtos)
    {
      HybridConnectionUrl = hybridConnectionUrl;
      PolicyDtos = policyDtos;
    }

    public Uri HybridConnectionUrl { get; set; }
    public PolicyDto[] PolicyDtos { get; }
  }
}
