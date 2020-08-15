using TeamTwo.CloudShield.ShieldController.Infrastructure.Models;
using TeamTwo.CloudShield.ShieldController.Services.Models;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure.Mappers
{
  public class HybridConnectionMapper : IHybridConnectionMapper
  {
    public HybridConnection MapToHybridConnection(ListenerDto listenerDto)
    {
      return new HybridConnection()
      {
        HybridConnectionUrl = listenerDto.HybridConnectionUrl,
        ListenerPolicyName = listenerDto.ListenerPolicyName,
        ListenerPolicyValue = listenerDto.ListenerPolicyValue
      };
    }
  }
}
