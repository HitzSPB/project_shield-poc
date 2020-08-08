using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.Customer.Management.Apis.Models;
using TeamTwo.Customer.Management.Services;

namespace TeamTwo.Customer.Management
{
  public class CustomerManagementFunctions
  {
    private readonly ICustomerManagementService _customerManagementService;
    public CustomerManagementFunctions(ICustomerManagementService customerManagementService)
    {
      _customerManagementService = customerManagementService;
    }

    [FunctionName("GetCustomer")]
    public async Task<IActionResult> GetCustomerAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customer/management/{customerid}")] HttpRequest req, string customerId,
        ILogger log)
    {
      var a = _customerManagementService.GetCustomerInformation(customerId);
      return new OkResult();
    }

    [FunctionName("StoreCustomer")]
    public async Task<IActionResult> StoreCustomerAsync(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customer/management")] HttpRequest req, string customerId,
    ILogger log)
    {
      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      StoreCustomerDto storeCustomer = JsonConvert.DeserializeObject<StoreCustomerDto>(requestBody);

      var a = _customerManagementService.StoreCustomerInformation(storeCustomer.customerId);
      return new OkResult();
    }
  }
}
