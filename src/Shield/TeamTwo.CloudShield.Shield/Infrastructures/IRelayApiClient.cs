using System.Net.Http;
using System.Threading.Tasks;
using TeamTwo.CloudShield.Shield.Service.Models;

namespace TeamTwo.CloudShield.Shield.Infrastructures
{
  public interface IRelayApiClient
  {
    public Task<HttpResponseMessage> RelayCallAsync(RelayCallDto relayCallDto);
  }
}
