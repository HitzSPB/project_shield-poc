using System;
using System.Threading.Tasks;
using TeamTwo.CloudShield.ShieldController.Services.Models;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure
{
  public interface ICustomerManagementApiClient
  {
    Task<CustomerInformation> GetCustomerInformationAsync(Guid tenantId);
    Task<CustomerInformation> CreateCustomerAsync(string customerId);
  }
}
