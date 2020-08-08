using System.Threading.Tasks;
using TeamTwo.Customer.Management.Services.Models;
using TeamTwo.Customer.Management.Utilities;

namespace TeamTwo.Customer.Management.Infrastructure
{
  public class CustomerManagementStorageApiClient : ICustomerManagementStorageApiClient
  {
    private readonly IApplicationSettingsService _applicationSettingsService;
    public CustomerManagementStorageApiClient(IApplicationSettingsService applicationSettingsService)
    {
      _applicationSettingsService = applicationSettingsService;
    }

    Task<CustomerInfo> ICustomerManagementStorageApiClient.GetCustomerAsync(string tenantId)
    {
      throw new System.NotImplementedException();
    }

    Task<CustomerInfo> ICustomerManagementStorageApiClient.StoreCustomerAsync(CustomerInfo customer)
    {
      throw new System.NotImplementedException();
    }
  }
}
