using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeamTwo.CloudShield.Shield.Service
{
  public class ProxyRelayCallService : IProxyRelayCallService
  {
    async Task<HttpResponseMessage> IProxyRelayCallService.ProxyRelayCallAsync(string tenantId, string body, HttpMethod httpMethod)
    {
      throw new NotImplementedException();
    }
  }
}
