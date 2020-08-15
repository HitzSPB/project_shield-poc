using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TeamTwo.CloudProvider.Management.Infrastructure.Models;
using TeamTwo.CloudProvider.Management.Utilities;

namespace TeamTwo.CloudProvider.Management.Infrastructure
{
  public class AzureAuthHandler : DelegatingHandler
  {
    private readonly HttpClient _httpClient;
    private readonly IApplicationSettingsService _applicationsSettings;
    public AzureAuthHandler(IApplicationSettingsService applicationsSettings, HttpClient httpClient)
    {
      _applicationsSettings = applicationsSettings;
      _httpClient = httpClient;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      Token token = await GetTokenAsync();
      request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token.access_token);
      return await base.SendAsync(request, cancellationToken);
    }

    private async Task<Token> GetTokenAsync()
    {
      var adTenantId = _applicationsSettings.AadTenantId;
      var loginUrl = new Uri($"https://login.microsoftonline.com/{adTenantId}/oauth2/token", UriKind.Absolute);
      var request = new HttpRequestMessage();
      var keyValues = new List<KeyValuePair<string, string>>();
      keyValues.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
      keyValues.Add(new KeyValuePair<string, string>("client_id", _applicationsSettings.AadClientId));
      keyValues.Add(new KeyValuePair<string, string>("client_secret", _applicationsSettings.AadClientSecret));
      keyValues.Add(new KeyValuePair<string, string>("resource", "https://management.azure.com/"));
      request.Content = new FormUrlEncodedContent(keyValues);
      request.RequestUri = loginUrl;
      request.Method = new HttpMethod(HttpMethods.Get);
      HttpResponseMessage response = await _httpClient.SendAsync(request);
      response.EnsureSuccessStatusCode();

      var responsebody = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Token>(responsebody);
    }
  }
}
