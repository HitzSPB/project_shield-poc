using System;
using TeamTwo.CloudProvider.Management.Services.Models;

namespace TeamTwo.CloudProvider.Management.Test
{
  public static class TestHelper
  {
    public static Uri GetDefaultLocalHostUri = new Uri("http://localhost");
    public static Uri GetCorrectUrlFormatWithWrongAddress = new Uri("https://www.google.com/");
    public static string TenantId = "TeamTwoCustomerTenantId";
    public static string PrimaryKey = "343gtdhsh-+3421";


    public static PolicyDto GetListenPolicyDto()
    {
      return CreatePolicyDto(PolicyClaim.Listen);
    }
    public static PolicyDto GetSendPolicyDto()
    {
      return CreatePolicyDto(PolicyClaim.Send);
    }

    private static PolicyDto CreatePolicyDto(PolicyClaim policyClaim)
    {
      return new PolicyDto()
      {
        PolicyName = $"PolicyName{policyClaim}",
        PolicyKey = $"PolicyKey{policyClaim}",
        PolicyType = policyClaim
      };
    }
  }
}
