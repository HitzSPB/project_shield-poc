using System;

namespace TeamTwo.Customer.Management.Services.Models
{
  public class CustomerInfo
  {
    public CustomerInfo()
    { }
    public CustomerInfo(string customerId, Guid tenantId)
    {
      TenantId = tenantId;
      CustomerId = customerId;
    }
    public string CustomerId { get; set; }
    public Guid TenantId { get; set; }

  }
}
