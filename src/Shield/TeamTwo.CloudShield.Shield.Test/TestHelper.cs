using System;
using TeamTwo.CloudShield.Shield.Service.Models;
using TeamTwo.CloudShield.Shield.Services.Models;

namespace TeamTwo.CloudShield.Shield.Test
{
  public static class TestHelper
  {
    public static Guid TenantId = Guid.NewGuid();
    public static Uri HybridConnectionUri = new Uri("https://relay.hybridconnection.com");

    public static HybridConnectionDto GetHybridConnectionDto()
    {
      return new HybridConnectionDto(HybridConnectionUri, new PolicyDto[]
        {
          new PolicyDto() { PolicyKey = "ListenKey", PolicyName = "ListenName", PolicyType = PolicyClaim.Listen},
          new PolicyDto() { PolicyKey = "SendKey", PolicyName = "SendName", PolicyType = PolicyClaim.Send}
        });
    }

    public static CreateRelayStorageDto GetCreateRelayStorageDto()
    {
      return new CreateRelayStorageDto() { TenantId = TenantId.ToString() };
    }
  }
}
