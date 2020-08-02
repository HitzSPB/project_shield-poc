using System.Net.Http;
using System.Threading.Tasks;

namespace TeamTwo.CloudShield.Shield.Service
{
  public interface IProxyRelayCallService
  {
    Task<HttpResponseMessage> ProxyRelayCallAsync(string tenantId, string body, HttpMethod httpMethod);
  }
}
