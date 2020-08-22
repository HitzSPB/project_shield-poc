using System;

namespace TeamTwo.CloudShield.ShieldController.Apis.Models
{
  public class CustomerDetailsDto
  {
    public CustomerDetailsDto() { }
    public CustomerDetailsDto(Guid tenantId)
    {
      TenantId = tenantId;
    }
    public Guid TenantId { get; set; }
  }
}
