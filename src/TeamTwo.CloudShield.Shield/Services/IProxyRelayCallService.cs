using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TeamTwo.CloudShield.Shield.Services
{
  public interface IProxyRelayCallService
  {
    Task<HttpResponseMessage> ProxyRelayCallAsync(string tenantId, string body, HttpMethod httpMethod, IHeaderDictionary httpHeaders);
  }
}
