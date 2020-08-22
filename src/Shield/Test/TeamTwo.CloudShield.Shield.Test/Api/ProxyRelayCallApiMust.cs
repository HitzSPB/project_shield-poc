using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.CloudShield.Shield.Apis;
using TeamTwo.CloudShield.Shield.Services;
using Xunit;

namespace TeamTwo.CloudShield.Shield.Test
{
  public class ProxyRelayCallApiMust
  {
    [Fact]
    [Trait("category", "unit")]
    public async Task ReturnStatusCodeOkWhenSuccessfulAsync()
    {
      // Arrange
      IProxyRelayCallService proxyRelayCallService = A.Fake<IProxyRelayCallService>();
      ILogger log = A.Fake<ILogger>();
      A.CallTo(() => proxyRelayCallService.ProxyRelayCallAsync(null, null, null, null, null)).WithAnyArguments().Returns(new HttpResponseMessage()
      { Content = new StringContent("Unit Test"),
       StatusCode = HttpStatusCode.OK});
      var sut = new ProxyRelayCallApi(proxyRelayCallService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext)
      {
        Body = new MemoryStream(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject("test"))),
        Method = "get"
      };
      // Act
      IActionResult response = await sut.RelayCallAsync(httpRequest, Guid.NewGuid().ToString(), log);

      // Assert
      Assert.Equal((int) HttpStatusCode.OK, ((ContentResult) response).StatusCode);
    }
    [Fact]
    [Trait("category", "unit")]
    public async Task ReturnStatusCodeBadRequestWhenTenantIdIsNotAGuidOrDoesNotExistAsync()
    {
      // Arrange
      IProxyRelayCallService proxyRelayCallService = A.Fake<IProxyRelayCallService>();
      ILogger log = A.Fake<ILogger>();
      var sut = new ProxyRelayCallApi(proxyRelayCallService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      // Act
      IActionResult response = await sut.RelayCallAsync(httpRequest, "34fe", log);

      // Assert
      Assert.Equal((int) HttpStatusCode.BadRequest, ((StatusCodeResult) response).StatusCode);
    }
    [Fact]
    [Trait("category", "unit")]
    public async Task ThrowArgumentNullExceptionWhenBodyIsNullAsync()
    {
      // Arrange
      IProxyRelayCallService proxyRelayCallService = A.Fake<IProxyRelayCallService>();
      ILogger log = A.Fake<ILogger>();
      var sut = new ProxyRelayCallApi(proxyRelayCallService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.RelayCallAsync(httpRequest, Guid.NewGuid().ToString(), log));
    }

    [Fact]
    [Trait("category", "integration")]
    public async Task ReturnStatusCodeOkWhenSuccessfulExecutionAsync()
    {
      // Arrange
      IProxyRelayCallService proxyRelayCallService = A.Fake<IProxyRelayCallService>();
      ILogger log = A.Fake<ILogger>();
      A.CallTo(() => proxyRelayCallService.ProxyRelayCallAsync(null, null, null, null, null)).WithAnyArguments().Returns(new HttpResponseMessage()
      {
        Content = new StringContent("Unit Test"),
        StatusCode = HttpStatusCode.OK
      });
      var sut = new ProxyRelayCallApi(proxyRelayCallService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext)
      {
        Body = new MemoryStream(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject("test"))),
        Method = "get"
      };
      // Act
      IActionResult response = await sut.RelayCallAsync(httpRequest, Guid.NewGuid().ToString(), log);

      // Assert
      Assert.Equal((int) HttpStatusCode.OK, ((ContentResult) response).StatusCode);
    }
  }
}
