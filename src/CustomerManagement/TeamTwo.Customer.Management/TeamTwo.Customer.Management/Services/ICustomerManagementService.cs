using System.Threading.Tasks;
using Microsoft.Build.Utilities;

namespace TeamTwo.Customer.Management.Services
{
  public interface ICustomerManagementService
  {
    Task<string> GetCustomerInformation();
    Task<string> StoreCustomerInformation();
  }
}
