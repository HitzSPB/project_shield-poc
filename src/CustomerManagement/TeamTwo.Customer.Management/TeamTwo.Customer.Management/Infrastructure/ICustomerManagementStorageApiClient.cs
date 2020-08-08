using System.Threading.Tasks;
using Microsoft.Build.Utilities;
using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services.Models;
namespace TeamTwo.Customer.Management.Infrastructure
{
  public interface ICustomerManagementStorageApiClient
  {
    Task<CustomerInfo> StoreCustomerAsync(CustomerInfo customer);
    Task<CustomerInfo> GetCustomerAsync(string tenantId);
  }
}
