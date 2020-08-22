using System;
using System.Threading.Tasks;
using TeamTwo.Customer.Management.Services.Models;

namespace TeamTwo.Customer.Management.Services
{
  public interface ICustomerManagementService
  {
    Task<CustomerInfo> GetCustomerInformationAsync(Guid customerId);
    Task<CustomerInfo> StoreCustomerInformationAsync(string customerId);
  }
}
