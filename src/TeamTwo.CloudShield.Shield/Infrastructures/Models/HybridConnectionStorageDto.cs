using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TeamTwo.CloudShield.Shield.Service.Model;

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
