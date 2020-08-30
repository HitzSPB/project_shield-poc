using System;
using System.Threading.Tasks;
using TeamTwo.CloudShield.ShieldController.Infrastructure;
using TeamTwo.CloudShield.ShieldController.Services.Models;

namespace TeamTwo.CloudShield.ShieldController.Services
{
  public class ShieldInformationService : IShieldInformationService
  {
    private readonly ICustomerManagementApiClient _customerManagementApiClient;
    private readonly IShieldApiClient _shieldApiClient;

    public ShieldInformationService(ICustomerManagementApiClient customerManagementApiClient, IShieldApiClient shieldApiClient)
    {
      _customerManagementApiClient = customerManagementApiClient;
      _shieldApiClient = shieldApiClient;
    }

    async Task<Guid> IShieldInformationService.CreateCustomerAsync(string customerId)
    {
      if (string.IsNullOrWhiteSpace(customerId)) throw new ArgumentNullException(nameof(customerId));
      return (await _customerManagementApiClient.CreateCustomerAsync(customerId)).TenantId;
    }

    async Task<HybridConnection> IShieldInformationService.GetCustomerRelayConnectionAsync(Guid tenantId)
    {
      if (string.IsNullOrWhiteSpace(tenantId.ToString())) throw new ArgumentException(nameof(tenantId));
      CustomerInformation customerInformation = await _customerManagementApiClient.GetCustomerInformationAsync(tenantId);
      if (customerInformation is null)
        return null;
      HybridConnection customerRelayInformation = await _shieldApiClient.GetCustomerRelayAsync(customerInformation.TenantId);
      if (customerRelayInformation is null)
        return await _shieldApiClient.CreateCustomerRelayAsync(customerInformation.TenantId);
      else
        return customerRelayInformation;
    }
  }
}
