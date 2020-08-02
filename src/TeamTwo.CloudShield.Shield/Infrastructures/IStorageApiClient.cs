using System.Threading.Tasks;
using TeamTwo.CloudShield.Shield.Infrastructures.Models;
using TeamTwo.CloudShield.Shield.Service.Model;

namespace TeamTwo.CloudShield.Shield.Infrastructure
{
  public interface IStorageApiClient
  {
    Task<HybridConnectionDto> GetRelayFromIdAsync(string tenantId);
    Task<HybridConnectionDto> StoreRelay(string tenantId, HybridConnectionDto hybridConnectionDto);
  }
}
