using System.Threading.Tasks;
using TeamTwo.CloudShield.Shield.Service.Models;

namespace TeamTwo.CloudShield.Shield.Infrastructures
{
  public interface ICloudProviderHandlerApiClient
  {
    Task<HybridConnectionDto> CreateRelayHybridConnection(string tenantId);
  }
}
