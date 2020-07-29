using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace TeamTwo.CloudShield.Shield.Api
{
	public class ShieldManagementApi
	{
		[FunctionName("GetRelayInformation")]
		public static async Task<IActionResult> GetRelayInformationAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post",
			Route = "relay-management/getrelayinfo/{RelayId}")] HttpRequest req, string relayId, ILogger log)
		{
			return new OkObjectResult(null);
		}

		[FunctionName("PostRelayInformation")]
		public static async Task<IActionResult> PostRelayInformationAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post",
			Route = "relay-management/postrelayinfo/{RelayId}")] HttpRequest req, string relayId, ILogger log)
		{
			return new OkObjectResult(null);
		}

	}
}
