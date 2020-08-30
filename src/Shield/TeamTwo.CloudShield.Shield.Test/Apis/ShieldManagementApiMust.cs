using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.CloudShield.Shield.Apis;
using TeamTwo.CloudShield.Shield.Apis.Models;
using TeamTwo.CloudShield.Shield.Service.Models;
using TeamTwo.CloudShield.Shield.Services;
using Xunit;

namespace TeamTwo.CloudShield.Shield.Test.Apis
{
  public class ShieldManagementApiMust
  {
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnSuccessStatusCodeWhenGetRelayIsCalledAsync()
    {
      // Arrange
      IRelayManagementService relayManagementService = A.Fake<IRelayManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ShieldManagementApi(relayManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      A.CallTo(() => relayManagementService.GetRelayAsync(TestHelper.TenantId.ToString())).WithAnyArguments().Returns(TestHelper.GetHybridConnectionDto());
      // Act
      IActionResult actual = await sut.GetRelayInformationAsync(httpRequest, "relay", logger);

      // Assert
      Assert.IsType<HybridConnectionDto>(((ObjectResult) actual).Value);
      Assert.Equal(HttpStatusCode.OK, (HttpStatusCode) ((ObjectResult) actual).StatusCode);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowArgumentNullExceptionWhenRelayIdMissingWhenGetRelayIsCalledAsync()
    {
      // Arrange
      IRelayManagementService relayManagementService = A.Fake<IRelayManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ShieldManagementApi(relayManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetRelayInformationAsync(httpRequest, "", logger));
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnNotFoundResultWhenNoHybridConnectionIsReturnedWhenGetRelayIsCalledAsync()
    {
      // Arrange
      IRelayManagementService relayManagementService = A.Fake<IRelayManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ShieldManagementApi(relayManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      A.CallTo(() => relayManagementService.GetRelayAsync(TestHelper.TenantId.ToString())).WithAnyArguments().Returns<HybridConnectionDto>(null);
      // Act
      IActionResult actual = await sut.GetRelayInformationAsync(httpRequest, "relay", logger);

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode) ((StatusCodeResult) actual).StatusCode);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnSuccessStatusCodeWhenGetRelayListnerIsCalledAsync()
    {
      // Arrange
      IRelayManagementService relayManagementService = A.Fake<IRelayManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ShieldManagementApi(relayManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      A.CallTo(() => relayManagementService.GetRelayAsync(TestHelper.TenantId.ToString())).WithAnyArguments().Returns(TestHelper.GetHybridConnectionDto());
      // Act
      IActionResult actual = await sut.GetRelayListnerInformationAsync(httpRequest, "relay", logger);

      // Assert
      Assert.IsType<ListenerDto>(((ObjectResult) actual).Value);
      Assert.Equal(HttpStatusCode.OK, (HttpStatusCode) ((ObjectResult) actual).StatusCode);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowArgumentNullExceptionWhenGetListenerIsCalledWithoutRelayAsync()
    {
      // Arrange
      IRelayManagementService relayManagementService = A.Fake<IRelayManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ShieldManagementApi(relayManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      A.CallTo(() => relayManagementService.GetRelayAsync(TestHelper.TenantId.ToString())).WithAnyArguments().Returns(TestHelper.GetHybridConnectionDto());
      // Act
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetRelayListnerInformationAsync(httpRequest, "", logger));

    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnNotFoundWhenGetRelayListenerIsCalledAndNoHybridConnectionDtoIsFoundAsync()
    {
      // Arrange
      IRelayManagementService relayManagementService = A.Fake<IRelayManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ShieldManagementApi(relayManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      A.CallTo(() => relayManagementService.GetRelayAsync(TestHelper.TenantId.ToString())).WithAnyArguments().Returns<HybridConnectionDto>(null);
      // Act
      IActionResult actual = await sut.GetRelayListnerInformationAsync(httpRequest, "relay", logger);

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode) ((StatusCodeResult) actual).StatusCode);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnSuccessStatusCodeWhenPostRelayIsCalledAsync()
    {
      // Arrange
      IRelayManagementService relayManagementService = A.Fake<IRelayManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ShieldManagementApi(relayManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext)
      {
        Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(TestHelper.GetCreateRelayStorageDto())))
      };
      A.CallTo(() => relayManagementService.StoreRelayAsync(TestHelper.GetCreateRelayStorageDto())).WithAnyArguments().Returns(TestHelper.GetHybridConnectionDto());
      // Act
      IActionResult actual = await sut.PostRelayInformationAsync(httpRequest, logger);

      // Assert
      Assert.IsType<ListenerDto>(((ObjectResult) actual).Value);
      Assert.Equal(HttpStatusCode.OK, (HttpStatusCode) ((ObjectResult) actual).StatusCode);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task PostRelayThrowInvalidOperationExceptionWhenHttpRequestContainsNoBodyAsync()
    {
      // Arrange
      IRelayManagementService relayManagementService = A.Fake<IRelayManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ShieldManagementApi(relayManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => sut.PostRelayInformationAsync(httpRequest, logger));
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnBadRequestStatusCodeWhenPostRelayIsCalledAndSomethingWentWrongAsync()
    {
      // Arrange
      IRelayManagementService relayManagementService = A.Fake<IRelayManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new ShieldManagementApi(relayManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext)
      {
        Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(TestHelper.GetCreateRelayStorageDto())))
      };
      A.CallTo(() => relayManagementService.StoreRelayAsync(TestHelper.GetCreateRelayStorageDto())).WithAnyArguments().Returns<HybridConnectionDto>(null);
      // Act
      IActionResult actual = await sut.PostRelayInformationAsync(httpRequest, logger);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode) ((StatusCodeResult) actual).StatusCode);
    }
  }
}
