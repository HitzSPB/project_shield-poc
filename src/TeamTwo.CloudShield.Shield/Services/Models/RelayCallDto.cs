using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TeamTwo.CloudShield.Shield.Service.Model
{
  public class RelayCallDto
  {
    public RelayCallDto(HybridConnectionDto hybridConnection, string bodyContent, HttpMethod httpMethod, IHeaderDictionary httpHeaders)
    {
      HybridConnection = hybridConnection;
      BodyContent = bodyContent;
      HttpMethod = httpMethod;
      HttpHeaders = httpHeaders;
    }

    public HybridConnectionDto HybridConnection { get; set; }
    public string BodyContent { get; set; }
    public HttpMethod HttpMethod { get; set; }
    public IHeaderDictionary HttpHeaders { get; set; }
  }
}
