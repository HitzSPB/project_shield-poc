using System;
using System.Threading.Tasks;
using TeamTwo.CloudShield.ShieldController.Services.Models;

namespace TeamTwo.CloudShield.ShieldController.Services
{
  public interface IShieldInformationService
  {
    Task<Guid> CreateCustomerAsync(string customerId);
    Task<HybridConnection> GetCustomerRelayConnectionAsync(Guid tenantId);
  }
}
