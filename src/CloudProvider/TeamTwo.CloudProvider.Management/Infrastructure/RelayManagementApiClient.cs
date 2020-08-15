using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TeamTwo.CloudProvider.Management.Infrastructure.Models;
using TeamTwo.CloudProvider.Management.Services.Models;
using TeamTwo.CloudProvider.Management.Utilities;

namespace TeamTwo.CloudProvider.Management.Infrastructure
{
  public class RelayManagementApiClient : IRelayManagementApiClient
  {
    private readonly HttpClient _httpClient;
    private readonly IApplicationSettingsService _applicationsSettings;
    public RelayManagementApiClient(IApplicationSettingsService applicationsSettings, HttpClient httpClient)
    {
      _applicationsSettings = applicationsSettings;
      _httpClient = httpClient;
      _httpClient.BaseAddress = new Uri(_applicationsSettings.AzureManagementApi, UriKind.Absolute);
    }
    
    async Task<Uri> IRelayManagementApiClient.CreateHybridConnectionAsync(string tenantId)
    {

      //_httpClient.BaseAddress = new Uri(_applicationsSettings.GetProcessEnvironmentVariable("AZURE-MANAGEMENTAPI"), UriKind.Absolute);

      var subscriptionId = _applicationsSettings.AzureSubscriptionId;
      var resourceGroupName = _applicationsSettings.ResourceGroupname;
      var relayNamespaceName = _applicationsSettings.RelayNameSpace;
      var hybridConnectionName = $"{tenantId}Hybrid";
      var relativeUrl = new Uri($"subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Relay/namespaces/{relayNamespaceName}/hybridConnections/{hybridConnectionName}?api-version=2017-04-01"
        , UriKind.Relative);
      var httpRequestMessage = new HttpRequestMessage();
      httpRequestMessage.Method = new HttpMethod(HttpMethods.Put);
      httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(new CreateHybridConnectionRequestBody(true)));
      httpRequestMessage.RequestUri = relativeUrl;
      HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);
      response.EnsureSuccessStatusCode();
      return new Uri($"https://{relayNamespaceName}.servicebus.windows.net/{hybridConnectionName}", UriKind.Absolute);
    }

    async Task<PolicyDto> IRelayManagementApiClient.CreatePolicykeyAsync(string tenantId, PolicyClaim policyClaim)
    {
      var subscriptionId = _applicationsSettings.AzureSubscriptionId;
      var resourceGroupName = _applicationsSettings.ResourceGroupname;
      var relayNamespaceName = _applicationsSettings.RelayNameSpace;
      var hybridConnectionName = $"{tenantId}Hybrid";
      var policyName = Guid.NewGuid().ToString();
      var relativeUrl = new Uri($"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Relay/namespaces/{relayNamespaceName}/hybridConnections/{hybridConnectionName}/authorizationRules/{policyName}?api-version=2017-04-01", UriKind.Relative);
      var httpRequestMessage = new HttpRequestMessage();
      httpRequestMessage.Method = new HttpMethod(HttpMethods.Put);
      httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(new CreateHybridConnectionPolicyRequest(new string[] { policyClaim.ToString() })));
      httpRequestMessage.RequestUri = relativeUrl;
      HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);
      response.EnsureSuccessStatusCode();

      var responsebody = await response.Content.ReadAsStringAsync();

      return new PolicyDto() { PolicyName = policyName, PolicyKey = await GetPolicyKeyAsync(hybridConnectionName, policyName), PolicyType = policyClaim };

    }

    private async Task<string> GetPolicyKeyAsync(string hybridConnectionName, string policyName)
    {
      var subscriptionId = _applicationsSettings.AzureSubscriptionId;
      var resourceGroupName = _applicationsSettings.ResourceGroupname;
      var relayNamespaceName = _applicationsSettings.RelayNameSpace;
      var relativeUrl = new Uri($"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Relay/namespaces/{relayNamespaceName}/hybridConnections/{hybridConnectionName}/authorizationRules/{policyName}/listKeys?api-version=2017-04-01", UriKind.Relative);

      var request = new HttpRequestMessage(HttpMethod.Post, relativeUrl);
      HttpResponseMessage response = await _httpClient.SendAsync(request);
      response.EnsureSuccessStatusCode();
      var content = await response.Content.ReadAsStringAsync();
      GetPolicyKeysResponse getPolicyKeysResponse = JsonConvert.DeserializeObject<GetPolicyKeysResponse>(content);
      return getPolicyKeysResponse.PrimaryKey;
    }
  }
}
