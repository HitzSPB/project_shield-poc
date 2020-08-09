using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure
{
  public interface IShieldApiClient
  {
    Task<object> GetCustomerRelayAsync(Guid tenantId);
    Task<object> CreateCustomerRelayAsync(Guid tenantId);
  }
}
