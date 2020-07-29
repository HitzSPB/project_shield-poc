using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using TeamTwo.CloudShield.Shield.Service;

namespace TeamTwo.CloudShield.Shield.Api
{
	public class ProxyRelayCallApi
	{
    //private readonly IProxyRelayCallService _proxyRelayCallService;
    //public ProxyRelayCallApi(IProxyRelayCallService proxyRelayCallService)
    public ProxyRelayCallApi()
    {
      //_proxyRelayCallService = proxyRelayCallService;
    }

		[FunctionName("RelayCall")]
		public async Task<IActionResult> RelayCallAsync(
	[HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "patch", "delete", Route = "proxyrelay/{tenantid}")] HttpRequest req, string tenantId, ILogger log)
		{
      if (!Guid.TryParse(tenantId, out Guid tenantIdGuid))
        return new BadRequestResult();
			log.LogInformation("C# HTTP trigger function processed a request.");
			var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      if (string.IsNullOrWhiteSpace(requestBody)) throw new ArgumentNullException(nameof(req.Body));

      return new OkResult();
		}
	}
}
