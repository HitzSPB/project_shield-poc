using System.Threading.Tasks;
using TeamTwo.Customer.Management.Infrastructure;

namespace TeamTwo.Customer.Management.Services
{
  public class CustomerManagementService : ICustomerManagementService
  {
    private readonly ICustomerManagementStorageApiClient _customerManagementStorageApiClient;
    public CustomerManagementService(ICustomerManagementStorageApiClient customerManagementStorageApiClient)
    {
      _customerManagementStorageApiClient = customerManagementStorageApiClient;
    }
    Task<string> ICustomerManagementService.GetCustomerInformation()
    {
      throw new System.NotImplementedException();
    }

    Task<string> ICustomerManagementService.StoreCustomerInformation()
    {
      throw new System.NotImplementedException();
    }
  }
}
