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
      if (_httpClient.BaseAddress == null)
        _httpClient.BaseAddress = new Uri(_applicationsSettingsService.CustomerManagementUrl); // Should be set here as it cannot be done twice
    }
    async Task<CustomerInformation> ICustomerManagementApiClient.GetCustomerInformationAsync(Guid tenantId)
    {
      HttpResponseMessage response = await _httpClient.GetAsync($"api/customer/management/{tenantId}");
      if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        return null;
      else
      {
        response.EnsureSuccessStatusCode();
        var responseAsJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CustomerInformation>(responseAsJson);
      }
    }
    async Task<CustomerInformation> ICustomerManagementApiClient.CreateCustomerAsync(string customerId)
    {
      var customerRequestDto = JsonConvert.DeserializeObject(customerId);
      HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/customer/management", customerRequestDto);
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
