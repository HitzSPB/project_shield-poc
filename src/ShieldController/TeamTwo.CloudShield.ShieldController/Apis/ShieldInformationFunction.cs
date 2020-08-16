using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.CloudShield.ShieldController.Apis.Models;
using TeamTwo.CloudShield.ShieldController.Services;
using TeamTwo.CloudShield.ShieldController.Services.Models;

namespace TeamTwo.CloudShield.ShieldController.Apis
{
  public class ShieldInformationFunction
  {
    private readonly IShieldInformationService _shieldInformationService;

    public ShieldInformationFunction(IShieldInformationService shieldInformationService)
    {
      _shieldInformationService = shieldInformationService;
    }
    [FunctionName("GetShieldInfo")]
    public async Task<IActionResult> ShieldInformatiionControllerAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "shieldInformation/controller/relay/{tenantId}")] HttpRequest req, string tenantId,
            ILogger log)
    {
      if (string.IsNullOrWhiteSpace(tenantId)) throw new ArgumentNullException(nameof(tenantId));
      if (!Guid.TryParse(tenantId, out Guid guid))
        return new BadRequestResult();
      HybridConnection hybridConnection = await _shieldInformationService.GetCustomerRelayConnectionAsync(guid);

      if (hybridConnection is null)
        return new BadRequestResult();
      else
        return new OkObjectResult(hybridConnection);

    }

    //Todo handle layers correctly and move out of this function
    [FunctionName("CreateCustomer")]
    public async Task<IActionResult> CreateCustomerAsync(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shieldinformation/controller/customer")] HttpRequest req, ILogger log)
    {
      var stream = new StreamReader(req.Body);
      var bodyContent = await stream.ReadToEndAsync();
      Guid tenantId = await _shieldInformationService.CreateCustomerAsync(bodyContent);
      if(tenantId == null)
        return new BadRequestResult();
      else
        return new OkObjectResult(new CustomerDetailsDto(tenantId));

    }
  }
}
