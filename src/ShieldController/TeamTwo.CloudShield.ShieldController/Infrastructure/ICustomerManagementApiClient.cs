using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamTwo.CloudShield.ShieldController.Services.Models;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure
{
  public interface ICustomerManagementApiClient
  {
    Task<CustomerInformation> GetCustomerInformationAsync(string customerId);
    Task<CustomerInformation> CreateCustomerAsync(string customerId);
  }
}
