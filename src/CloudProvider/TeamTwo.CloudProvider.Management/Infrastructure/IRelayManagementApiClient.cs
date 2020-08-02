using System.Threading.Tasks;

namespace TeamTwo.CloudProvider.Management.Infrastructure
{
  public interface IRelayManagementApiClient
  {
    Task<string> CreateHybridConnection(string tenantId);
    Task<string> CreatePolicykey(string tenantId);
  }
}
