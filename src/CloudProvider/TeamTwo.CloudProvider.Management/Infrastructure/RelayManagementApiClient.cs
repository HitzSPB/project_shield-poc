using System;
using System.Net.Http;
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
      _httpClient.BaseAddress = new Uri(!string.IsNullOrWhiteSpace(_applicationsSettings.AzureManagementApi) ?
        _applicationsSettings.AzureManagementApi : throw new InvalidOperationException(nameof(_applicationsSettings.AzureManagementApi)), UriKind.Absolute);
    }

    async Task<Uri> IRelayManagementApiClient.CreateHybridConnectionAsync(string tenantId)
    {

      // Check for empty strings and null checks - Consider making private method to make the empty check
      if (string.IsNullOrWhiteSpace(tenantId)) throw new ArgumentNullException(tenantId);
      var subscriptionId = !string.IsNullOrWhiteSpace(_applicationsSettings.AzureSubscriptionId) ?
        _applicationsSettings.AzureSubscriptionId : throw new InvalidOperationException(nameof(_applicationsSettings.AzureSubscriptionId));
      var resourceGroupName = !string.IsNullOrWhiteSpace(_applicationsSettings.ResourceGroupname) ?
        _applicationsSettings.ResourceGroupname : throw new InvalidOperationException(nameof(_applicationsSettings.ResourceGroupname));
      var relayNamespaceName = !string.IsNullOrWhiteSpace(_applicationsSettings.RelayNameSpace) ?
        _applicationsSettings.RelayNameSpace : throw new InvalidOperationException(nameof(_applicationsSettings.RelayNameSpace));


      var hybridConnectionName = $"{tenantId}Hybrid";
      var relativeUrl = new Uri($"subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Relay/namespaces/{relayNamespaceName}/hybridConnections/{hybridConnectionName}?api-version=2017-04-01"
        , UriKind.Relative);
      var httpRequestMessage = new HttpRequestMessage
      {
        Method = new HttpMethod(HttpMethods.Put),
        Content = new StringContent(JsonConvert.SerializeObject(new CreateHybridConnectionRequestBody(true))),
        RequestUri = relativeUrl
      };
      HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);
      response.EnsureSuccessStatusCode();
      return new Uri($"https://{relayNamespaceName}.servicebus.windows.net/{hybridConnectionName}", UriKind.Absolute);
    }

    async Task<PolicyDto> IRelayManagementApiClient.CreatePolicykeyAsync(string tenantId, PolicyClaim policyClaim)
    {
      // Check for empty strings and null checks - Consider making private method to make the empty check
      if (string.IsNullOrWhiteSpace(tenantId)) throw new ArgumentNullException(tenantId);
      var subscriptionId = !string.IsNullOrWhiteSpace(_applicationsSettings.AzureSubscriptionId) ?
        _applicationsSettings.AzureSubscriptionId : throw new InvalidOperationException(nameof(_applicationsSettings.AzureSubscriptionId));
      var resourceGroupName = !string.IsNullOrWhiteSpace(_applicationsSettings.ResourceGroupname) ?
        _applicationsSettings.ResourceGroupname : throw new InvalidOperationException(nameof(_applicationsSettings.ResourceGroupname));
      var relayNamespaceName = !string.IsNullOrWhiteSpace(_applicationsSettings.RelayNameSpace) ?
        _applicationsSettings.RelayNameSpace : throw new InvalidOperationException(nameof(_applicationsSettings.RelayNameSpace));

      var hybridConnectionName = $"{tenantId}Hybrid";
      var policyName = Guid.NewGuid().ToString();
      var relativeUrlString = $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Relay/namespaces/{relayNamespaceName}/hybridConnections/{hybridConnectionName}/authorizationRules/{policyName}";
      var relativeUrl = new Uri($"{relativeUrlString}?api-version=2017-04-01", UriKind.Relative);
      var httpRequestMessage = new HttpRequestMessage
      {
        Method = new HttpMethod(HttpMethods.Put),
        Content = new StringContent(JsonConvert.SerializeObject(new CreateHybridConnectionPolicyRequest(new string[] { policyClaim.ToString() }))),
        RequestUri = relativeUrl
      };
      HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);
      response.EnsureSuccessStatusCode();

      return new PolicyDto() { PolicyName = policyName, PolicyKey = await GetPolicyKeyAsync(relativeUrlString), PolicyType = policyClaim };

    }

    private async Task<string> GetPolicyKeyAsync(string relativeUrlString)
    {
      var relativeUrl = new Uri($"{relativeUrlString}/listKeys?api-version=2017-04-01", UriKind.Relative);
      var request = new HttpRequestMessage(HttpMethod.Post, relativeUrl);
      HttpResponseMessage response = await _httpClient.SendAsync(request);
      response.EnsureSuccessStatusCode();
      var content = await response.Content.ReadAsStringAsync();
      GetPolicyKeysResponse getPolicyKeysResponse = JsonConvert.DeserializeObject<GetPolicyKeysResponse>(content);
      return getPolicyKeysResponse.PrimaryKey;
    }
  }
}
