using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Relay;
using TeamTwo.CloudShield.Shield.Service.Models;

namespace TeamTwo.CloudShield.Shield.Infrastructures
{
  public class RelayApiClient : IRelayApiClient
  {
    private readonly HttpClient _httpClient;
    public RelayApiClient(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }
    async Task<HttpResponseMessage> IRelayApiClient.RelayCallAsync(RelayCallDto relayCallDto)
    {
      await SetupHttpClientAsync(relayCallDto);
      var request = new HttpRequestMessage(relayCallDto.HttpMethod, new Uri(relayCallDto.HybridConnection.HybridConnectionUrl, $"?url=relayCallDto.Url"))
      {
        Content = new StringContent(relayCallDto.BodyContent)
      };

      HttpResponseMessage response = await _httpClient.SendAsync(request);

      return response;
    }

    private async Task SetupHttpClientAsync(RelayCallDto relayCallDto)
    {
      PolicyDto policyDto = relayCallDto.HybridConnection.PolicyDtos.First(x => x.PolicyType == PolicyClaim.Send);
      var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(policyDto.PolicyName, policyDto.PolicyKey);
      var token = (await tokenProvider.GetTokenAsync(relayCallDto.HybridConnection.HybridConnectionUrl.AbsoluteUri, TimeSpan.FromHours(1))).TokenString;
      _httpClient.DefaultRequestHeaders.Add("ServiceBusAuthorization", token);
    }
  }
}
