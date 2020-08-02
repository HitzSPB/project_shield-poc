using System;
using System.Net.Http;
using System.Threading.Tasks;

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
