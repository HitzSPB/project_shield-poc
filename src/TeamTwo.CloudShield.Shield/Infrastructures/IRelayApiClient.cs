using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Build.Utilities;
using TeamTwo.CloudShield.Shield.Service.Model;

namespace TeamTwo.CloudShield.Shield.Infrastructure
{
  public interface IRelayApiClient
  {
    public Task<HttpResponseMessage> RelayCallAsync(RelayCallDto relayCallDto);
  }
}
