using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
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

    [FunctionName("CustomerManagement")]
    public async Task<IActionResult> CustomerManagementAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "customer/management/{customerid}")] HttpRequest req, string customerId,
        ILogger log)
    {
      log.LogInformation("C# HTTP trigger function processed a request.");
      return new OkResult();
    }

    public override bool Equals(object obj)
    {
      return obj is CustomerManagementFunctions functions &&
             EqualityComparer<ICustomerManagementService>.Default.Equals(_customerManagementService, functions._customerManagementService);
    }
  }
}
