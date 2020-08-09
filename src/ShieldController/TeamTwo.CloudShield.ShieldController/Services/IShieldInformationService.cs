using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamTwo.CloudShield.ShieldController.Services
{
  public interface IShieldInformationService
  {
    Task<object> GetCustomerRelayConnection(string customerId);
  }
}
