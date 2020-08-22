using System.Collections.Generic;
using Newtonsoft.Json;

namespace TeamTwo.CloudProvider.Management.Infrastructure.Models
{
  public class CreateHybridConnectionPolicyRequest
  {
    public CreateHybridConnectionPolicyRequest(IList<string> claims)
    {
      Properties = new PropertiesInternalPolicy(claims);
    }
    [JsonProperty("properties")]
    public PropertiesInternalPolicy Properties { get; set; }
  }

  public class PropertiesInternalPolicy
  {
    public PropertiesInternalPolicy(IList<string> claims)
    {
      Claims = claims;
    }
    [JsonProperty("rights")]
    public IList<string> Claims { get; }
  }
}
