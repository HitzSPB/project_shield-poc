using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TeamTwo.Customer.Management.Infrastructure.Models
{
  public class CustomerInfoStorageDto
  {
    public CustomerInfoStorageDto()
    { }
    public CustomerInfoStorageDto(string customerId, Guid tenantId)
    {
      TenantId = tenantId;
      CustomerId = customerId;
    }
    [JsonProperty("id")]
    public string CustomerId { get; set; }
    public Guid TenantId { get; set; }

  }
}
