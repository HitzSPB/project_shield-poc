using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamTwo.CloudShield.Shield.Infrastructures;
using TeamTwo.CloudShield.Shield.Service.Models;
using TeamTwo.CloudShield.Shield.Services.Models;

namespace TeamTwo.CloudShield.Shield.Services
{
  public class RelayManagementService : IRelayManagementService
  {
    private readonly IStorageApiClient _storageApiClient;
    private readonly ICloudProviderHandlerApiClient _cloudProviderHandlerApiClient;
    public RelayManagementService(IStorageApiClient storageApiClient, ICloudProviderHandlerApiClient cloudProviderHandlerApiClient)
    {
      _storageApiClient = storageApiClient;
      _cloudProviderHandlerApiClient = cloudProviderHandlerApiClient;
    }

    async Task<HybridConnectionDto> IRelayManagementService.GetRelayAsync(string tenantId)
    {
      return await _storageApiClient.GetRelayFromIdAsync(tenantId);
    }

    async Task<HybridConnectionDto> IRelayManagementService.StoreRelayAsync(CreateRelayStorageDto createRelayStorageDto)
    {
      HybridConnectionDto hybridConnectionDto = await _cloudProviderHandlerApiClient.CreateRelayHybridConnection(createRelayStorageDto.TenantId);
      if (hybridConnectionDto is null) throw new InvalidOperationException(nameof(hybridConnectionDto));

      return await _storageApiClient.StoreRelayAsync(createRelayStorageDto.TenantId, hybridConnectionDto);
    }
  }
}
