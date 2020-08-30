using System;
using System.Collections.Generic;
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
using Microsoft.Extensions.Primitives;
using TeamTwo.CloudShield.Shield.Apis;
using TeamTwo.CloudShield.Shield.Services;
using Xunit;

namespace TeamTwo.CloudShield.Shield.Test
{
  public class ProxyRelayCallApiMust
  {
    [Fact]
    public async Task ReturnCorrectStatusCodeAfterProxyingCallAsync()
    {
      // Arrange
      IProxyRelayCallService proxyRelayCallService = A.Fake<IProxyRelayCallService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ProxyRelayCallApi(proxyRelayCallService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext)
      {
        Query = new QueryCollection(new Dictionary<string, StringValues>() { { "url", "/testhost/value" } }),
        Method = HttpMethods.Post,
        Body = new MemoryStream(Encoding.UTF8.GetBytes("bodyContent"))
      };

      var contentResult = "randomContent";
      A.CallTo(() => proxyRelayCallService.ProxyRelayCallAsync(TestHelper.TenantId.ToString(), "bodyContent", new HttpMethod("post"), null, "/testhost/value")).WithAnyArguments().Returns(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(contentResult) });

      // Act
      IActionResult actual = await sut.RelayCallAsync(httpRequest, TestHelper.TenantId.ToString(), logger);

      // Assert
      Assert.Equal(HttpStatusCode.OK, (HttpStatusCode) ((ContentResult) actual).StatusCode);
      Assert.Equal(contentResult, ((ContentResult) actual).Content);
    }

    [Fact]
    public async Task ReturnBadRequestStatusCodeWhenCalledWithInvalidTenantIdAsync()
    {
      // Arrange
      IProxyRelayCallService proxyRelayCallService = A.Fake<IProxyRelayCallService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ProxyRelayCallApi(proxyRelayCallService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      // Act
      IActionResult actual = await sut.RelayCallAsync(httpRequest, "NotGuid", logger);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode) ((StatusCodeResult) actual).StatusCode);
    }
    [Fact]
    public async Task ThrowInvalidOperationWhenNoUrlIsProvidedAsync()
    {
      // Arrange
      IProxyRelayCallService proxyRelayCallService = A.Fake<IProxyRelayCallService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ProxyRelayCallApi(proxyRelayCallService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => sut.RelayCallAsync(httpRequest, TestHelper.TenantId.ToString(), logger));

    }
  }
}
