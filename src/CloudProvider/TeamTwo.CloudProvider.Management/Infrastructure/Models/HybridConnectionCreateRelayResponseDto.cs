using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTwo.CloudProvider.Management.Infrastructure.Models
{
  public class Properties
  {
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int listenerCount { get; set; }
    public bool requiresClientAuthorization { get; set; }
  }

  public class HybridConnectionCreateRelayResponseDto
  {
    public string id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string location { get; set; }
    public Properties properties { get; set; }
  }


}
