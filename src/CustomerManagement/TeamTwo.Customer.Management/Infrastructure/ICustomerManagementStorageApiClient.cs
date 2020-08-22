using System;
using System.Threading.Tasks;
using TeamTwo.Customer.Management.Services.Models;
namespace TeamTwo.Customer.Management.Infrastructure
{
  public interface ICustomerManagementStorageApiClient
  {
    Task<CustomerInfo> StoreCustomerAsync(CustomerInfo customer);
    Task<CustomerInfo> GetCustomerAsync(Guid tenantId);
  }
}
