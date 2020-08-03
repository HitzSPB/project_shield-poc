using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.CloudProvider.Management.Services;
using TeamTwo.CloudProvider.Management.Services.Models;

namespace TeamTwo.CloudProvider.Management
{
  public class RelayHybridConnectionApi
  {
    private readonly IRelayAzureManagementService _relayAzureManagement;
    public RelayHybridConnectionApi(IRelayAzureManagementService relayAzureManagement)
    {
      _relayAzureManagement = relayAzureManagement;
    }
    [FunctionName("CreateHybridConnection")]
    public async Task<IActionResult> CreateRelayHybridConnectionAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "relay/hybridconnection")] HttpRequest req,
        ILogger log)
    {
      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      CreateRelayDto createRelayDto = JsonConvert.DeserializeObject<CreateRelayDto>(requestBody);
      HybridConnectionDto response = await _relayAzureManagement.CreateHybridConnection(createRelayDto);

      if (response != null)
        return new OkObjectResult(response);
      else
        return new BadRequestResult();
    }
  }
}
