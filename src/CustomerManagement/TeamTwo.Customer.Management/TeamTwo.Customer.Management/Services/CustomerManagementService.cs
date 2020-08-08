using System;
using System.Threading.Tasks;
using TeamTwo.Customer.Management.Infrastructure;
using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services.Models;

namespace TeamTwo.Customer.Management.Services
{
  public class CustomerManagementService : ICustomerManagementService
  {
    private readonly ICustomerManagementStorageApiClient _customerManagementStorageApiClient;
    public CustomerManagementService(ICustomerManagementStorageApiClient customerManagementStorageApiClient)
    {
      _customerManagementStorageApiClient = customerManagementStorageApiClient;
    }
    async Task<CustomerInfo> ICustomerManagementService.GetCustomerInformationAsync(string customerId)
    {
      if (string.IsNullOrWhiteSpace(customerId)) throw new ArgumentNullException(nameof(customerId));
      return await _customerManagementStorageApiClient.GetCustomerAsync(customerId);
    }

    async Task<CustomerInfo> ICustomerManagementService.StoreCustomerInformationAsync(string customerId)
    {
      if (string.IsNullOrWhiteSpace(customerId)) throw new ArgumentNullException(nameof(customerId));

      var customer = new CustomerInfo(customerId, Guid.NewGuid());
      return await _customerManagementStorageApiClient.StoreCustomerAsync(customer);
    }
  }
}
