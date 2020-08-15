using System;
using System.Threading.Tasks;
using TeamTwo.CloudShield.ShieldController.Services.Models;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure
{
  public interface IShieldApiClient
  {
    Task<HybridConnection> GetCustomerRelayAsync(Guid tenantId);
    Task<HybridConnection> CreateCustomerRelayAsync(Guid tenantId);
  }
}
