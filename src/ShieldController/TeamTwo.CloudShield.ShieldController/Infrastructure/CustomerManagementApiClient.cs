using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamTwo.CloudShield.ShieldController.Infrastructure.Models;
using TeamTwo.CloudShield.ShieldController.Services.Models;
using TeamTwo.CloudShield.ShieldController.Utilities;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure
{
  public class CustomerManagementApiClient : ICustomerManagementApiClient
  {
    private readonly IApplicationSettingsService _applicationsSettingsService;
    private readonly HttpClient _httpClient;

    public CustomerManagementApiClient(IApplicationSettingsService applicationsSettingsService, HttpClient httpClient)
    {
      _applicationsSettingsService = applicationsSettingsService;
      _httpClient = httpClient;
    }
    async Task<CustomerInformation> ICustomerManagementApiClient.GetCustomerInformationAsync(string customerId)
    {
      _httpClient.BaseAddress = new Uri(_applicationsSettingsService.CustomerManagementUrl);
      HttpResponseMessage response = await _httpClient.GetAsync($"customer/management/{customerId}");
      if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        return null;
      else
      {
        var responseAsJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CustomerInformation>(responseAsJson);
      }
    }
    async Task<CustomerInformation> ICustomerManagementApiClient.CreateCustomerAsync(string customerId)
    {
      _httpClient.BaseAddress = new Uri(_applicationsSettingsService.CustomerManagementUrl);
      HttpResponseMessage response = await _httpClient.PostAsJsonAsync("customer/management", new CreateRelayRequestDto() { TenantId = customerId });
      if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        return null;
      else
      {
        var responseAsJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CustomerInformation>(responseAsJson);
      }
    }
  }
}
