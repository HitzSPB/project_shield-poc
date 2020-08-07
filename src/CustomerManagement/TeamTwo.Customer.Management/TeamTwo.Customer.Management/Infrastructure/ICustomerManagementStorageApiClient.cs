using System.Threading.Tasks;
using Microsoft.Build.Utilities;

namespace TeamTwo.Customer.Management.Infrastructure
{
  public interface ICustomerManagementStorageApiClient
  {
    Task<object> StoreCustomer(string custonemerId);
    Task<object> GetCustomer(string tenantId);
  }
}
