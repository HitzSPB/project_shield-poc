﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TeamTwo.CloudShield.Shield.Infrastructure;
using TeamTwo.CloudShield.Shield.Service.Model;

namespace TeamTwo.CloudShield.Shield.Service
{
  public class ProxyRelayCallService : IProxyRelayCallService
  {
    private readonly IStorageApiClient _storageApiClient;
    private readonly IRelayApiClient _relayApiClient;
    public ProxyRelayCallService(IStorageApiClient storageApiClient, IRelayApiClient relayApiClient)
    {
      _storageApiClient = storageApiClient;
      _relayApiClient = relayApiClient;
    }
    async Task<HttpResponseMessage> IProxyRelayCallService.ProxyRelayCallAsync(string tenantId, string body, HttpMethod httpMethod, IHeaderDictionary httpHeaders)
    {
      if (string.IsNullOrWhiteSpace(body)) throw new ArgumentNullException(nameof(body));
      if (string.IsNullOrWhiteSpace(tenantId)) throw new ArgumentNullException(nameof(tenantId));
      if (httpMethod is null) throw new ArgumentNullException(nameof(httpMethod));

      HybridConnectionDto hybridConnectionDto = await _storageApiClient.GetRelayFromIdAsync(tenantId);

      if (hybridConnectionDto is null)
        return new HttpResponseMessage(HttpStatusCode.NotFound);

      var relayCallDto = new RelayCallDto(hybridConnectionDto, body, httpMethod, httpHeaders);
      return await _relayApiClient.RelayCallAsync(relayCallDto);
    }
  }
}
