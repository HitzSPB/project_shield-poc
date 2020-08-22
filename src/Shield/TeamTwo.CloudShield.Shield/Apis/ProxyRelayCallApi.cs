using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using TeamTwo.CloudShield.Shield.Services;

namespace TeamTwo.CloudShield.Shield.Apis
{
  public class ProxyRelayCallApi
  {
    private readonly IProxyRelayCallService _proxyRelayCallService;
    public ProxyRelayCallApi(IProxyRelayCallService proxyRelayCallService)
    {
      _proxyRelayCallService = proxyRelayCallService;
    }

    [FunctionName("RelayCall")]
    public async Task<IActionResult> RelayCallAsync(
  [HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "patch", "delete", Route = "proxyrelay/{tenantid}")] HttpRequest req, string tenantId, ILogger log)
    {
      if (!Guid.TryParse(tenantId, out Guid tenantIdGuid))
        return new BadRequestResult();
      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      string url = req.Query["url"];
      if (string.IsNullOrWhiteSpace(requestBody)) throw new ArgumentNullException(nameof(req.Body));
      if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
      HttpResponseMessage response = await _proxyRelayCallService.ProxyRelayCallAsync(tenantId, requestBody, new HttpMethod(req.Method), req.Headers, url);

      return new ContentResult()
      {
        Content = await response.Content.ReadAsStringAsync(),
        ContentType = "text/plain",
        StatusCode = (int) response.StatusCode
      };
    }
  }
}
