using Newtonsoft.Json;

namespace TeamTwo.CloudProvider.Management.Infrastructure.Models
{
  public class CreateHybridConnectionRequestBody
  {
    public CreateHybridConnectionRequestBody(bool requiresClientAuthorization)
    {
      Properties = new PropertiesInternal()
      {
        RequiresClientAuthorization = requiresClientAuthorization
      };
    }
    [JsonProperty("properties")]
    public PropertiesInternal Properties { get; set; }
  }

  public class PropertiesInternal
  {
    [JsonProperty("requiresClientAuthorization")]
    public bool RequiresClientAuthorization { get; set; }
  }
}
