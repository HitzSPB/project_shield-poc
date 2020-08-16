using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamTwo.CloudShield.ShieldController.Infrastructure.Mappers;
using TeamTwo.CloudShield.ShieldController.Infrastructure.Models;
using TeamTwo.CloudShield.ShieldController.Services.Models;
using TeamTwo.CloudShield.ShieldController.Utilities;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure
{
  public class ShieldApiClient : IShieldApiClient
  {
    private readonly IApplicationSettingsService _applicationsSettingsService;
    private readonly HttpClient _httpClient;
    private readonly IHybridConnectionMapper _hybridConnectionMapper;

    public ShieldApiClient(IApplicationSettingsService applicationsSettingsService, HttpClient httpClient, IHybridConnectionMapper hybridConnectionMapper)
    {
      _applicationsSettingsService = applicationsSettingsService;
      _httpClient = httpClient;
      _hybridConnectionMapper = hybridConnectionMapper;
      if(_httpClient.BaseAddress == null)
        _httpClient.BaseAddress = new Uri(_applicationsSettingsService.ShieldUrl);
    }
    async Task<HybridConnection> IShieldApiClient.CreateCustomerRelayAsync(Guid tenantId)
    {
      var relayRequestDto = new CreateRelayRequestDto() { TenantId = tenantId.ToString() };
      HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/relay-management/", relayRequestDto);
      var body = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<HybridConnection>(body);
    }

    async Task<HybridConnection> IShieldApiClient.GetCustomerRelayAsync(Guid tenantId)
    {
      HttpResponseMessage response = await _httpClient.GetAsync($"api/relay-management/relayinfo/{tenantId}/listener");
      var body = await response.Content.ReadAsStringAsync();
      if (string.IsNullOrWhiteSpace(body))
        return null;
      return _hybridConnectionMapper.MapToHybridConnection(JsonConvert.DeserializeObject<ListenerDto>(body));
    }
  }
}
