using System;
using System.Threading.Tasks;
using TeamTwo.CloudProvider.Management.Services.Models;

namespace TeamTwo.CloudProvider.Management.Infrastructure
{
  public interface IRelayManagementApiClient
  {
    Task<Uri> CreateHybridConnectionAsync(string tenantId);
    Task<PolicyDto> CreatePolicykeyAsync(string tenantId, PolicyClaim policyClaim);
  }
}
