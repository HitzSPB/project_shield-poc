using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure
{
  public class ShieldApiClient : IShieldApiClient
  {
    Task<object> IShieldApiClient.CreateCustomerRelayAsync(Guid tenantId)
    {
      throw new NotImplementedException();
    }

    Task<object> IShieldApiClient.GetCustomerRelayAsync(Guid tenantId)
    {
      throw new NotImplementedException();
    }
  }
}
