using System;
using System.Threading.Tasks;
using TeamTwo.CloudProvider.Management.Infrastructure;
using TeamTwo.CloudProvider.Management.Services.Models;

namespace TeamTwo.CloudProvider.Management.Services
{
  public class RelayAzureManagementService : IRelayAzureManagementService
  {
    private readonly IRelayManagementApiClient _relayManagementApiClient;
    public RelayAzureManagementService(IRelayManagementApiClient relayManagementApiClient)
    {
      _relayManagementApiClient = relayManagementApiClient;
    }
    async Task<HybridConnectionDto> IRelayAzureManagementService.CreateHybridConnection(CreateRelayDto createRelayStorageDto)
    {
      Uri hybridConnectionUrl = await _relayManagementApiClient.CreateHybridConnectionAsync(createRelayStorageDto.TenantId);
      PolicyDto policySendKey = await _relayManagementApiClient.CreatePolicykeyAsync(createRelayStorageDto.TenantId, PolicyClaim.Send);
      PolicyDto policyListenKey = await _relayManagementApiClient.CreatePolicykeyAsync(createRelayStorageDto.TenantId, PolicyClaim.Listen);
      return new HybridConnectionDto(hybridConnectionUrl, new PolicyDto[] { policySendKey, policyListenKey });
    }
  }
}
