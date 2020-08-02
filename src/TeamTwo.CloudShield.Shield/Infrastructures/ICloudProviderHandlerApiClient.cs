using System.Threading.Tasks;

namespace TeamTwo.CloudShield.Shield.Infrastructures
{
  public interface ICloudProviderHandlerApiClient
  {
    Task<object> CreateRelayHybridConnection(object hybridConnectionInformationDto);
  }
}
