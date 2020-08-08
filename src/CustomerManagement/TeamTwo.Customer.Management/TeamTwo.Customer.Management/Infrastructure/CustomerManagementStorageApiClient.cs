using System.Threading.Tasks;
using TeamTwo.Customer.Management.Infrastructure.Mappers;
using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services.Models;
using TeamTwo.Customer.Management.Utilities;

namespace TeamTwo.Customer.Management.Infrastructure
{
  public class CustomerManagementStorageApiClient : ICustomerManagementStorageApiClient
  {
    private readonly IApplicationSettingsService _applicationSettingsService;
    private readonly ICustomerInfoMapper _customerInfo;
    public CustomerManagementStorageApiClient(ICustomerInfoMapper customerInfo,IApplicationSettingsService applicationSettingsService)
    {
      _applicationSettingsService = applicationSettingsService;
      _customerInfo = customerInfo;
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
