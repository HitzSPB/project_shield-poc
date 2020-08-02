using System.Threading.Tasks;
using TeamTwo.CloudShield.Shield.Service.Models;

namespace TeamTwo.CloudShield.Shield.Infrastructures
{
  public interface IStorageApiClient
  {
    Task<HybridConnectionDto> GetRelayFromIdAsync(string tenantId);
    Task<HybridConnectionDto> StoreRelayAsync(string tenantId, HybridConnectionDto hybridConnectionDto);
  }
}
