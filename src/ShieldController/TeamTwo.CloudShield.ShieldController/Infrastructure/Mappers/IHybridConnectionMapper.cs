using TeamTwo.CloudShield.ShieldController.Infrastructure.Models;
using TeamTwo.CloudShield.ShieldController.Services.Models;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure.Mappers
{
  public interface IHybridConnectionMapper
  {
    HybridConnection MapToHybridConnection(ListenerDto listenerDto);
  }
}