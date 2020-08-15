using System.Threading.Tasks;
using TeamTwo.CloudShield.ShieldController.Services.Models;

namespace TeamTwo.CloudShield.ShieldController.Services
{
  public interface IShieldInformationService
  {
    Task<HybridConnection> GetCustomerRelayConnection(string customerId);
  }
}
