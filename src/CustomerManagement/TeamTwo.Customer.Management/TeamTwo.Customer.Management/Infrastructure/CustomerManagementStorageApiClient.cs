using System.Threading.Tasks;
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

    Task<object> ICustomerManagementStorageApiClient.GetCustomer(string tenantId)
    {
      throw new System.NotImplementedException();
    }

    Task<object> ICustomerManagementStorageApiClient.StoreCustomer(string custonemerId)
    {
      throw new System.NotImplementedException();
    }
  }
}
