using System;
using System.Threading.Tasks;
using TeamTwo.CloudShield.ShieldController.Infrastructure;
using TeamTwo.CloudShield.ShieldController.Services.Models;
using TeamTwo.CloudShield.ShieldController.Utilities;

namespace TeamTwo.CloudShield.ShieldController.Services
{
  public class ShieldInformationService : IShieldInformationService
  {
    private readonly IApplicationSettingsService _applicationsSettingsService;
    private readonly ICustomerManagementApiClient _customerManagementApiClient;
    private readonly IShieldApiClient _shieldApiClient;

    public ShieldInformationService(IApplicationSettingsService applicationsSettingsService, ICustomerManagementApiClient customerManagementApiClient,
      IShieldApiClient shieldApiClient)
    {
      _applicationsSettingsService = applicationsSettingsService;
      _customerManagementApiClient = customerManagementApiClient;
      _shieldApiClient = shieldApiClient;
    }

    async Task<HybridConnection> IShieldInformationService.GetCustomerRelayConnection(string customerId)
    {
      if (string.IsNullOrWhiteSpace(customerId)) throw new ArgumentException(nameof(customerId));
      CustomerInformation customerInformation = await _customerManagementApiClient.GetCustomerInformationAsync(customerId);
      if (customerInformation is null)
        customerInformation = await _customerManagementApiClient.CreateCustomerAsync(customerId);
      HybridConnection customerRelayInformation = await _shieldApiClient.GetCustomerRelayAsync(customerInformation.TenantId);
      if (customerRelayInformation is null)
        return await _shieldApiClient.CreateCustomerRelayAsync(customerInformation.TenantId);
      else
        return customerRelayInformation;
    }
  }
}
