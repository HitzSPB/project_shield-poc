using System;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace TeamTwo.Customer.Management.Infrastructure.Models
{
  public class CustomerInfoEntity : TableEntity
  {
    public CustomerInfoEntity()
    {

    }
    public CustomerInfoEntity(Guid customerId, Guid tenantId) : base(tenantId.ToString(), tenantId.ToString())
    {
      TenantId = tenantId;
      CustomerId = customerId;
    }
    [JsonProperty("id")]
    public Guid CustomerId { get; set; }
    public Guid TenantId { get; set; }

  }
}
