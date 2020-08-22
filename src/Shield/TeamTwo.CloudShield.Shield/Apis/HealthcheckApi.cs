using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace TeamTwo.CloudShield.Shield.Apis
{
  public class HealthcheckApi
  {
    // Todo consider if this is needed with the options of Azure health service, application insights & Azure alerts
    [FunctionName("HealthCheck")]
    public async Task<IActionResult> HealthCheckAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "healthcheck")]
        HttpRequest req, ILogger log)
    {
      return await Task.FromResult(new OkResult());
    }
  }
}
