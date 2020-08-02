using Newtonsoft.Json;
using TeamTwo.CloudShield.Shield.Service.Models;

namespace TeamTwo.CloudShield.Shield.Infrastructures.Models
{
  public class HybridConnectionStorageDto
  {
    public HybridConnectionStorageDto(string id, HybridConnectionDto hybridConnection)
    {
      Id = id;
      HybridConnection = hybridConnection;
    }

    [JsonProperty("id")]
    public string Id { get; set; }
    public HybridConnectionDto HybridConnection { get; set; }
  }
}
