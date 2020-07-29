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
	public class ProxyRelayCallApi
	{
		[FunctionName("RelayCall")]
		public static async Task<IActionResult> RelayCallAsync(
	[HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "patch", "delete", Route = null)] HttpRequest req, ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			string name = req.Query["name"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

			return new OkObjectResult(responseMessage);
		}
	}
}
