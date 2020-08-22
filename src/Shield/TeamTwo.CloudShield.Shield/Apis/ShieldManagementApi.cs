using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.CloudShield.Shield.Apis.Models;
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
      if (string.IsNullOrWhiteSpace(relayId)) throw new ArgumentNullException(nameof(relayId));

      HybridConnectionDto response = await _relayManagementService.GetRelayAsync(relayId);
      if (response != null)
        return new OkObjectResult(response);
      else
        return new NotFoundResult();
    }

    [FunctionName("GetRelayListnerInformationAsync")]
    public async Task<IActionResult> GetRelayListnerInformationAsync([HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "relay-management/relayinfo/{relayId}/listener")] HttpRequest req, string relayId, ILogger log)
    {
      if (string.IsNullOrWhiteSpace(relayId)) throw new ArgumentNullException(nameof(relayId));

      HybridConnectionDto hybridConnectionDto = await _relayManagementService.GetRelayAsync(relayId);
      if (hybridConnectionDto != null)
      {
        var listenerDto = new ListenerDto()
        {
          HybridConnectionUrl = hybridConnectionDto.HybridConnectionUrl,
          ListenerPolicyName = hybridConnectionDto.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyName,
          ListenerPolicyValue = hybridConnectionDto.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyKey
        };
        return new OkObjectResult(listenerDto);
      }
      else
        return new NotFoundResult();
    }

    [FunctionName("PostRelayInformation")]
    public async Task<IActionResult> PostRelayInformationAsync([HttpTrigger(AuthorizationLevel.Function, "post",
            Route = "relay-management/")] HttpRequest req, ILogger log)
    {
      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      if (requestBody is null) throw new InvalidOperationException(nameof(requestBody));

      CreateRelayStorageDto createRelayDto = JsonConvert.DeserializeObject<CreateRelayStorageDto>(requestBody);
      if (string.IsNullOrWhiteSpace(createRelayDto.TenantId)) throw new InvalidOperationException(nameof(createRelayDto));

      HybridConnectionDto hybridConnectionDto = await _relayManagementService.StoreRelayAsync(JsonConvert.DeserializeObject<CreateRelayStorageDto>(requestBody));
      if (hybridConnectionDto != null)
      {
        var listenerDto = new ListenerDto()
        {
          HybridConnectionUrl = hybridConnectionDto.HybridConnectionUrl,
          ListenerPolicyName = hybridConnectionDto.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyName,
          ListenerPolicyValue = hybridConnectionDto.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyKey
        };
        return new OkObjectResult(listenerDto);
      }
      else
        return new BadRequestResult();
    }

  }
}
