using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Utilities;

namespace TeamTwo.CloudShield.Shield.Service
{
  public interface IProxyRelayCallService
  {
    Task<HttpResponseMessage> ProxyRelayCallAsync(string tenantId, string body, HttpMethod httpMethod);
  }
}
