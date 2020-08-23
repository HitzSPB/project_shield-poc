using System;
using TeamTwo.Customer.Management.Apis.Models;
using TeamTwo.Customer.Management.Services.Models;

namespace TeamTwo.Customer.Management.Test
{
  public static class TestHelper
  {
    public static Guid CustomerId = Guid.NewGuid();
    public static Guid TenantId = Guid.NewGuid();
    public static string CosmosDatabaseAccessKey { get; set; }
    public static Uri CosmosDatabaseUri { get; set; }
    public static StoreCustomer GetStoreCustomerWithTestHelperDefaultValues()
    {
      return GetStoreCustomer(CustomerId);
    }

    public static StoreCustomer GetStoreCustomer(Guid customerId)
    {
      return new StoreCustomer()
      {
        customerId = customerId
      };
    }

    public static CustomerInfo GetCustomerInfoWithTestHelperDefaultValues()
    {
      return GetCustomerInfo(CustomerId, TenantId);
    }

    public static CustomerInfo GetCustomerInfo(Guid customerId, Guid tenantId)
    {
      return new CustomerInfo(customerId, tenantId);
    }
  }
}
