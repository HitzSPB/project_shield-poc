using System.Threading.Tasks;
using Microsoft.Build.Utilities;
using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services.Models;

namespace TeamTwo.Customer.Management.Services
{
  public interface ICustomerManagementService
  {
    Task<CustomerInfo> GetCustomerInformationAsync(string customerId);
    Task<CustomerInfo> StoreCustomerInformationAsync(string customerId);
  }
}
