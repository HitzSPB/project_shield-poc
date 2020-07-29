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
	public class HealthcheckApi
	{
		[FunctionName("HealthCheck")]
		public static async Task<IActionResult> HealthCheckAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "healthcheck")]
		HttpRequest req, ILogger log)
		{
			return new OkObjectResult(null);
		}
	}
}
