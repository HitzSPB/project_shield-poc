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
using TeamTwo.Customer.Management.Services.Models;

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
      CustomerInfo customerInfo = await _customerManagementService.GetCustomerInformationAsync(customerId);
      return new OkObjectResult(customerInfo);
    }

    [FunctionName("StoreCustomer")]
    public async Task<IActionResult> StoreCustomerAsync(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customer/management")] HttpRequest req, string customerId,
    ILogger log)
    {
      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      StoreCustomer storeCustomer = JsonConvert.DeserializeObject<StoreCustomer>(requestBody);

      CustomerInfo customerInfo = await _customerManagementService.StoreCustomerInformationAsync(storeCustomer.customerId);
      return new OkObjectResult(customerInfo);
    }
  }
}
