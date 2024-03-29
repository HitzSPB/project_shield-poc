﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using TeamTwo.CloudShield.Shield.Infrastructures.Mappers;
using TeamTwo.CloudShield.Shield.Infrastructures.Models;
using TeamTwo.CloudShield.Shield.Service.Models;
using TeamTwo.CloudShield.Shield.Utilities;

namespace TeamTwo.CloudShield.Shield.Infrastructures
{
  public class StorageApiClient : IStorageApiClient
  {
    private readonly IHybridConnectionDtoMapper _hybridConnectionDtoMapper;

    private readonly CosmosClient _cosmosClient;
    public StorageApiClient(IApplicationsSettingsService applicationsSettings, IHybridConnectionDtoMapper hybridConnectionDtoMapper)
    {
      _hybridConnectionDtoMapper = hybridConnectionDtoMapper;
      _cosmosClient = new CosmosClient(applicationsSettings.AccountEndPoint,
        applicationsSettings.Authkey,
        new CosmosClientOptions { ConnectionMode = ConnectionMode.Gateway });
    }

    async Task<HybridConnectionDto> IStorageApiClient.GetRelayFromIdAsync(string tenantId)
    {
      Database database = await _cosmosClient.CreateDatabaseIfNotExistsAsync("RelayProxyDatabase");
      Container container = await database.CreateContainerIfNotExistsAsync("ProxyRelayContainer", "/Connections");
      var query = new QueryDefinition($"SELECT c.id, c.HybridConnection FROM c WHERE c.id = '{tenantId}'");
      FeedIterator<HybridConnectionStorageDto> queryResultSetIterator = container.GetItemQueryIterator<HybridConnectionStorageDto>(query);
      var hybridConnectionStorageDtos = new List<HybridConnectionStorageDto>();
      while (queryResultSetIterator.HasMoreResults)
      {
        FeedResponse<HybridConnectionStorageDto> currentResultSet = await queryResultSetIterator.ReadNextAsync();
        foreach (HybridConnectionStorageDto hybridConnectionStorageDto in currentResultSet)
        {
          hybridConnectionStorageDtos.Add(hybridConnectionStorageDto);
        }
      }

      if (hybridConnectionStorageDtos.Count == 0)
        return null;
      return _hybridConnectionDtoMapper.HybridConnectionDtoMap(hybridConnectionStorageDtos.FirstOrDefault(x => x.Id == tenantId));
    }

    async Task<HybridConnectionDto> IStorageApiClient.StoreRelayAsync(string tenantId, HybridConnectionDto hybridConnectionDto)
    {
      Database database = await _cosmosClient.CreateDatabaseIfNotExistsAsync("RelayProxyDatabase");
      Container container = await database.CreateContainerIfNotExistsAsync("ProxyRelayContainer", "/Connections");
      ItemResponse<HybridConnectionStorageDto> cosmosDbResponse = await container.CreateItemAsync(_hybridConnectionDtoMapper.HybridConnectionStorageDtoMap(tenantId, hybridConnectionDto));
      return _hybridConnectionDtoMapper.HybridConnectionDtoMap(cosmosDbResponse.Resource);
    }
  }
}
