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
	public static class RelayApi
	{
		[FunctionName("RelayCall")]
		public static async Task<IActionResult> RelayCallAsync(
			[HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "patch", "delete", Route = null)] HttpRequest req, ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			string name = req.Query["name"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			name = name ?? data?.name;

			string responseMessage = string.IsNullOrEmpty(name)
				? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
				: $"Hello, {name}. This HTTP triggered function executed successfully.";

			return new OkObjectResult(responseMessage);
		}

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

		[FunctionName("HealthCheck")]
		public static async Task<IActionResult> HealthCheckAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "healthcheck")]
		HttpRequest req, ILogger log)
		{
			return new OkObjectResult(null);
		}
	}
}
