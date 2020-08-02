using System.Threading.Tasks;

namespace TeamTwo.CloudShield.Shield.Infrastructure
{
  public interface IStorageApiClient
  {
    Task<object> GetRelayFromIdAsync(string id);
    Task<object> StoreRelay(object serverStorageInformationDto);
  }
}
