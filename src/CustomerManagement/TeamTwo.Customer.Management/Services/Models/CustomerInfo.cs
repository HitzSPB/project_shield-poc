using System;

namespace TeamTwo.Customer.Management.Services.Models
{
  public class CustomerInfo
  {
    public CustomerInfo()
    { }
    public CustomerInfo(Guid customerId, Guid tenantId)
    {
      TenantId = tenantId;
      CustomerId = customerId;
    }
    public Guid CustomerId { get; set; }
    public Guid TenantId { get; set; }

  }
}
