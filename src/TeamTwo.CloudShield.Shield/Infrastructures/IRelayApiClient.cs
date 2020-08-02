using System.Net.Http;
using System.Threading.Tasks;
using TeamTwo.CloudShield.Shield.Service.Model;

namespace TeamTwo.CloudShield.Shield.Infrastructure
{
  public interface IRelayApiClient
  {
    public Task<HttpResponseMessage> RelayCallAsync(RelayCallDto relayCallDto);
  }
}
