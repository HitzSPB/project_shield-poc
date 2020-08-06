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
  }
}
