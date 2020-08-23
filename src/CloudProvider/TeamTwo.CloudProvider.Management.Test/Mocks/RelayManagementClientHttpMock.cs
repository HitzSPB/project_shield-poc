using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamTwo.CloudProvider.Management.Infrastructure.Models;

namespace TeamTwo.CloudProvider.Management.Test.Mocks
{
  public class RelayManagementClientHttpMock : DelegatingHandler
  {
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
    {
      var response = new HttpResponseMessage();
      var split = httpRequest.RequestUri.AbsolutePath.Split("/");
      if (httpRequest.RequestUri.OriginalString.Contains(TestHelper.GetDefaultLocalHostUri.ToString()))
      {
        if (split.Contains("hybridConnections"))
        {
          if (split.Contains("authorizationRules") && httpRequest.Method == HttpMethod.Post)
          {
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(JsonConvert.SerializeObject(new GetPolicyKeysResponse()
            { KeyName = split[split.Length - 1], PrimaryKey = TestHelper.PrimaryKey, PrimaryConnectionString = "NotInUse" }));
          }
          else if (split.Contains("authorizationRules") && httpRequest.Method == HttpMethod.Put)
          {
            response.StatusCode = HttpStatusCode.OK;
          }
          else
          {
            CreateHybridConnectionRequestBody result = JsonConvert.DeserializeObject<CreateHybridConnectionRequestBody>
              (await httpRequest.Content.ReadAsStringAsync());
            if (result.Properties.RequiresClientAuthorization == true)
            {
              response.StatusCode = HttpStatusCode.OK;
            }
            else
            {
              response.StatusCode = HttpStatusCode.BadRequest;
            }
          }
        }
      }
      else
      {
        response.StatusCode = HttpStatusCode.NotFound;
      }
      return await Task.FromResult(response);
    }
  }
}
