using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure.Models
{
  public class CreateCustomerRequest
  {
    public CreateCustomerRequest(string customerId)
    {
      CustomerId = customerId;
    }
    public string CustomerId { get; set; }

  }
}
