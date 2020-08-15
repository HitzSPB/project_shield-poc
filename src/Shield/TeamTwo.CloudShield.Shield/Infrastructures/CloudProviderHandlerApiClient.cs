using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamTwo.CloudShield.Shield.Infrastructures.Models;
using TeamTwo.CloudShield.Shield.Service.Models;
using TeamTwo.CloudShield.Shield.Utilities;

namespace TeamTwo.CloudShield.Shield.Infrastructures
{
  public class CloudProviderHandlerApiClient : ICloudProviderHandlerApiClient
  {
    private readonly IApplicationsSettingsService _applicationsSettingsService;
    private readonly HttpClient _httpClient;
    public CloudProviderHandlerApiClient(IApplicationsSettingsService applicationsSettingsService, HttpClient httpClient)
    {
      _applicationsSettingsService = applicationsSettingsService;
      _httpClient = httpClient;
    }
    async Task<HybridConnectionDto> ICloudProviderHandlerApiClient.CreateRelayHybridConnection(string tenantId)
    {
      _httpClient.BaseAddress = new Uri(_applicationsSettingsService.CloudProviderUrl, UriKind.Absolute);
      HttpResponseMessage response = await _httpClient.PostAsJsonAsync(new Uri("relay/hybridconnection", UriKind.Relative), new CloudProviderRequestBody(tenantId));
      response.EnsureSuccessStatusCode();
      var body = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<HybridConnectionDto>(body);
    }
  }
}
