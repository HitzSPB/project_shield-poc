using System;

namespace TeamTwo.CloudShield.ShieldController.Services.Models
{
  public class HybridConnection
  {
    public Uri HybridConnectionUrl { get; set; }
    public string ListenerPolicyName { get; set; }
    public string ListenerPolicyValue { get; set; }
  }
}
