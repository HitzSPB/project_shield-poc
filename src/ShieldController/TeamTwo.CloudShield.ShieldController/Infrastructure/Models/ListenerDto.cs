using System;

namespace TeamTwo.CloudShield.ShieldController.Infrastructure.Models
{
  public class ListenerDto
  {
    public Uri HybridConnectionUrl { get; set; }
    public string ListenerPolicyName { get; set; }
    public string ListenerPolicyValue { get; set; }
  }
}
