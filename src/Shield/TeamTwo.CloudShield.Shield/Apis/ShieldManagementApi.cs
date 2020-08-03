using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.CloudShield.Shield.Service.Models;
using TeamTwo.CloudShield.Shield.Services;
using TeamTwo.CloudShield.Shield.Services.Models;

namespace TeamTwo.CloudShield.Shield.Apis
{
  public class ShieldManagementApi
  {
    private readonly IRelayManagementService _relayManagementService;
    public ShieldManagementApi(IRelayManagementService relayManagementService)
    {
      _relayManagementService = relayManagementService;
    }

    [FunctionName("GetRelayInformation")]
    public async Task<IActionResult> GetRelayInformationAsync([HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "relay-management/relayinfo/{RelayId}")] HttpRequest req, string relayId, ILogger log)
    {
      HybridConnectionDto response = await _relayManagementService.GetRelayAsync(relayId);
      if (response != null)
        return new OkObjectResult(response);
      else
        return new BadRequestResult();
    }

    [FunctionName("PostRelayInformation")]
    public async Task<IActionResult> PostRelayInformationAsync([HttpTrigger(AuthorizationLevel.Function, "post",
            Route = "relay-management/")] HttpRequest req, ILogger log)
    {
      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      if (requestBody is null) throw new ArgumentNullException(nameof(requestBody));

      CreateRelayStorageDto createRelayDto = JsonConvert.DeserializeObject<CreateRelayStorageDto>(requestBody);
      if (string.IsNullOrWhiteSpace(createRelayDto.TenantId)) throw new InvalidOperationException(nameof(createRelayDto));

      HybridConnectionDto response = await _relayManagementService.StoreRelayAsync(JsonConvert.DeserializeObject<CreateRelayStorageDto>(requestBody));
      if (response != null)
        return new OkObjectResult(response);
      else
        return new BadRequestResult();
    }

  }
}
