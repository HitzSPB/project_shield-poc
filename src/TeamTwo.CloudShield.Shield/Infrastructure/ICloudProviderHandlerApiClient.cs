using System.Threading.Tasks;

namespace TeamTwo.CloudShield.Shield.Infrastructure
{
  public interface ICloudProviderHandlerApiClient
  {
    Task<object> CreateRelayHybridConnection(object hybridConnectionInformationDto);
  }
}
