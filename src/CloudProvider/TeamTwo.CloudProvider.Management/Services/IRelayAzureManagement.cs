using System.Threading.Tasks;
using TeamTwo.CloudProvider.Management.Services.Models;

namespace TeamTwo.CloudProvider.Management.Services
{
  public interface IRelayAzureManagement
  {
    Task<HybridConnectionDto> CreateHybridConnection(CreateRelayDto createRelayStorageDto);
  }
}
